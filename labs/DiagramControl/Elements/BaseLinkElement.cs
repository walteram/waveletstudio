using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace DiagramNet.Elements
{
	/// <summary>
	/// This is the base for all link element.
	/// </summary>
	[Serializable]
	public abstract class BaseLinkElement: BaseElement
	{
		protected ConnectorElement Connector1Value;
		protected ConnectorElement Connector2Value;
		protected LineCap StartCapValue;
		protected LineCap EndCapValue;
		protected bool NeedCalcLinkValue = true;

		internal BaseLinkElement(ConnectorElement conn1, ConnectorElement conn2)
		{
			BorderWidthValue = 1;
			BorderColorValue = Color.Black;

			Connector1Value = conn1;
			Connector2Value = conn2;

			Connector1Value.AddLink(this);
			Connector2Value.AddLink(this);
		}

		[Browsable(false)]
		public ConnectorElement Connector1
		{
			get
			{
				return Connector1Value;
			}
			set
			{
				if (value == null)
					return;
				
				Connector1Value.RemoveLink(this);
				Connector1Value = value;
				NeedCalcLinkValue = true;
				Connector1Value.AddLink(this);
				OnConnectorChanged(new EventArgs());
			}
		}

		[Browsable(false)]
		public ConnectorElement Connector2
		{
			get
			{
				return Connector2Value;
			}
			set
			{
				if (value == null)
					return;
				
				Connector2Value.RemoveLink(this);
				Connector2Value = value;
				NeedCalcLinkValue = true;
				Connector2Value.AddLink(this);
				OnConnectorChanged(new EventArgs());
			}
		}

		[Browsable(false)]
		internal bool NeedCalcLink
		{
			get
			{
				return NeedCalcLinkValue;
			}
			set
			{
				NeedCalcLinkValue = value;
			}
		}

		public abstract override Point Location
		{	
			get;
		}

		public abstract override Size Size
		{
			get;
		}

		public abstract LineElement[] Lines
		{
			get;
		}

		[Browsable(false)]
		public abstract Point Point1
		{
			get;
		}

		[Browsable(false)]
		public abstract Point Point2
		{
			get;
		}

		public virtual LineCap StartCap
		{
			get
			{
				return StartCapValue;
			}
			set
			{
				StartCapValue = value;
			}
		}

		public virtual LineCap EndCap
		{
			get
			{
				return EndCapValue;
			}
			set
			{
				EndCapValue = value;
			}
		}

		internal abstract void CalcLink();

	    #region Events
		[field: NonSerialized]
		public event EventHandler ConnectorChanged;

		protected virtual void OnConnectorChanged(EventArgs e)
		{
			if (ConnectorChanged != null)
				ConnectorChanged(this, e);
		}
		#endregion

	}
}


