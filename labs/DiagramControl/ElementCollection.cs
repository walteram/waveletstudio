using System;
using System.Collections;
using System.Drawing;
using DiagramNet.Elements;

namespace DiagramNet
{
	[Serializable]
	public class ElementCollection : ReadOnlyCollectionBase
	{
		private Point _location = new Point(MaxIntSize, MaxIntSize);
		private Size _size = new Size(0, 0);
		private bool _enabledCalc = true;	
		
		private bool _needCalc = true;

		public const int MaxIntSize = 100;

		#region Collection Members
		
		internal ElementCollection()
		{ }

		public BaseElement this[int item]
		{
			get
			{
				return (BaseElement) InnerList[item];
			}
		}

		internal virtual int Add(BaseElement element)
		{
			_needCalc = true;

			return InnerList.Add(element);
		}

		public bool Contains(BaseElement element)
		{
			return InnerList.Contains(element);
		}

		public int IndexOf(BaseElement element)
		{
			return InnerList.IndexOf(element);
		}

		internal void Insert(int index, BaseElement element)
		{
			_needCalc = true;
						
			InnerList.Insert(index, element);
		}
		
		internal void Remove(BaseElement element)
		{
			InnerList.Remove(element);

			_needCalc = true;

		}

		internal void Clear()
		{
			InnerList.Clear();
			_needCalc = true;
		}

		internal void ChangeIndex(int i, int y)
		{
			var tmp = InnerList[y];
			InnerList[y] = InnerList[i];
			InnerList[i] = tmp;
		}

		#region Implementation of IEnumerator
		public class BaseElementEnumarator : IEnumerator 
		{

			private readonly IEnumerator _baseEnumarator;
			private readonly IEnumerable _tmp;

			BaseElementEnumarator(IEnumerable mapping)
			{
				_tmp = mapping;
				_baseEnumarator = _tmp.GetEnumerator();
			}

			

			void IEnumerator.Reset()
			{
				_baseEnumarator.Reset();
			}
			bool IEnumerator.MoveNext()
			{
				return _baseEnumarator.MoveNext();
			}
			
			object IEnumerator.Current
			{
				get
				{
					return _baseEnumarator.Current;
				}
			}

			public void Reset()
			{
				_baseEnumarator.Reset();
			}
			public bool MoveNext()
			{
				return _baseEnumarator.MoveNext();
			}
			
			public BaseElement Current
			{
				get
				{
					return (BaseElement) _baseEnumarator.Current;
				}
			}
		}
		#endregion

		#endregion
	
		public BaseElement[] GetArray()
		{
			var els = new BaseElement[InnerList.Count];
			for (var i = 0; i <= InnerList.Count - 1; i++)
			{
				els[i] = (BaseElement) InnerList[i];
			}
			return els;
		}

		#region Window Methods and Properties

		internal bool EnabledCalc
		{
			get
			{
				return _enabledCalc;
			}
			set
			{
				_enabledCalc = value;

				if (_enabledCalc)
				{
					_needCalc = true;
				}
			}
		}

		internal Point WindowLocation
		{
			get
			{	
				CalcWindow();
				return _location;
			}
		}

		internal Size WindowSize
		{
			get
			{
				CalcWindow();
				return _size;
			}
		}

		internal void CalcWindow(bool forceCalc)
		{
			if (forceCalc)
				_needCalc = true;
			CalcWindow();
		}

		internal void CalcWindow()
		{
			if (!_enabledCalc) return;

			if (!_needCalc) return;

			_location.X = MaxIntSize;
			_location.Y = MaxIntSize;
			_size.Width = 0;
			_size.Height = 0;
			foreach (BaseElement element in this)
			{
				CalcWindowLocation(element);
			}
			
			foreach (BaseElement element in this)
			{
				CalcWindowSize(element);
			}

			_needCalc = false;
		}

		internal void CalcWindowLocation(BaseElement element)
		{
			if (!_enabledCalc) return;

			var elementLocation = element.Location;

			if (elementLocation.X < _location.X)
				_location.X = elementLocation.X;
		
			if (elementLocation.Y < _location.Y)
				_location.Y = elementLocation.Y;
		}

		internal void CalcWindowSize(BaseElement element)
		{
			if (!_enabledCalc) return;

		    var elementLocation = element.Location;
			var elementSize = element.Size;

			var val = (elementLocation.X + elementSize.Width) - _location.X;
			if (val > _size.Width)
				_size.Width = val;

			val = (elementLocation.Y + elementSize.Height) - _location.Y;
			if (val > _size.Height)
				_size.Height = val;

		}
		#endregion
	}
}
