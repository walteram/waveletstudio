using System;
using System.Windows.Forms;
using System.Diagnostics;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;

namespace Qios.DevSuite.DemoZone.Misc
{
	/// <summary>
	/// Summary description for QRibbonEventListener.
	/// </summary>
	public class QCompositeEventListener
	{
		IQCompositeItemEventPublisher m_oPublisher;

		public QCompositeEventListener(IQCompositeItemEventPublisher publisher)
		{
			m_oPublisher = publisher;
			m_oPublisher.PaintItem += new QCompositePaintStageEventHandler(Publisher_PaintItem);
			m_oPublisher.ItemActivating +=new QCompositeCancelEventHandler(Publisher_ItemActivating);
			m_oPublisher.ItemActivated +=new QCompositeEventHandler(Publisher_ItemActivated);
			m_oPublisher.ItemSelected +=new QCompositeEventHandler(Publisher_ItemSelected);
            
			m_oPublisher.ItemExpanded +=new QCompositeExpandedEventHandler(Publisher_ItemExpanded);
			m_oPublisher.ItemExpanding +=new QCompositeExpandingCancelEventHandler(Publisher_ItemExpanding);
			m_oPublisher.ItemCollapsed +=new QCompositeEventHandler(Publisher_ItemCollapsed);
			m_oPublisher.ItemCollapsing +=new QCompositeCancelEventHandler(Publisher_ItemCollapsing);
		}

		private void Publisher_PaintItem(object sender, QCompositePaintStageEventArgs e)
		{
			if (e.Stage == QPartPaintStage.ForegroundPainted)
			{				
				//e.Context.Graphics.DrawString("B", Control.DefaultFont, System.Drawing.Brushes.Red, e.Item.CalculatedProperties.Bounds.X,e.Item.CalculatedProperties.Bounds.Y);
			}

		}

		private void Publisher_ItemActivating(object sender, QCompositeCancelEventArgs e)
		{
			this.WriteEvent("ItemActivating", e);
		}

		private void Publisher_ItemActivated(object sender, QCompositeEventArgs e)
		{
			this.WriteEvent("ItemActivated", e);
		}

		private void Publisher_ItemSelected(object sender, QCompositeEventArgs e)
		{
			this.WriteEvent("ItemSelected", e);
		}
	
		private void Publisher_ItemExpanded(object sender, QCompositeExpandedEventArgs e)
		{
			this.WriteEvent("ItemExpanded", e);
		}

		private void Publisher_ItemExpanding(object sender, QCompositeExpandingCancelEventArgs e)
		{
			this.WriteEvent("ItemExpanding", e);
		}

		private void Publisher_ItemCollapsed(object sender, QCompositeEventArgs e)
		{
			this.WriteEvent("ItemCollapsed", e);
		}

		private void Publisher_ItemCollapsing(object sender, QCompositeCancelEventArgs e)
		{
			this.WriteEvent("ItemCollapsing", e);
		}

		private void Publisher_CompositeKeyPress(object sender, QCompositeKeyboardCancelEventArgs e)
		{
			this.WriteEvent("CompositeKeyPress", e);
		}

		private int m_iTrace = 0;

		private void WriteEvent(string eventName, QCompositeEventArgs e)
		{
			QCompositeMenuItem tmp_oItem = e.Item as QCompositeMenuItem;
			string tmp_sItemType = (e.Item != null) ? e.Item.GetType().Name : "<NULL>";
			string tmp_sItemTitle = (tmp_oItem != null) ? tmp_oItem.Title : "<no title>";

			Trace.WriteLine(
				String.Format("{0} - {1}. ItemType = {2}, ItemTitle = {3}, ActivationType = {4}",
				m_iTrace++, eventName, tmp_sItemType, tmp_sItemTitle, e.ActivationType.ToString()));

		}
	}
}
