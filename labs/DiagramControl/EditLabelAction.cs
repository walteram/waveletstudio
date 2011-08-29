using System.Drawing;
using System.Windows.Forms;
using DiagramNet.Elements;
using DiagramNet.Elements.Controllers;

namespace DiagramNet
{
	/// <summary>
	/// This class control the label edition of the element.
	/// </summary>
	internal class EditLabelAction
	{
		private BaseElement _siteLabelElement;
		private LabelElement _labelElement;
		private TextBox _labelTextBox;
		private LabelEditDirection _direction;
		private Point _center;
		private const int TextBoxBorder = 3;

	    public void StartEdit(BaseElement el, TextBox textBox)
		{
			if (!(el is ILabelElement)) return;

			if (((ILabelElement) el).Label.ReadOnly) return;

			_siteLabelElement = el;
			_labelElement = ((ILabelElement) _siteLabelElement).Label;
			_labelTextBox = textBox;
			if (_siteLabelElement is BaseLinkElement)
				_direction = LabelEditDirection.Both;
			else
				_direction = LabelEditDirection.UpDown;
			
			SetTextBoxLocation(_siteLabelElement, _labelTextBox);

			_labelTextBox.AutoSize = true;
			_labelTextBox.Show();
			_labelTextBox.Text = _labelElement.Text;
			_labelTextBox.Font = _labelElement.Font;
			_labelTextBox.WordWrap = _labelElement.Wrap;
			
			_labelElement.Invalidate();
			
			switch(_labelElement.Alignment)
			{
				case StringAlignment.Near:
					_labelTextBox.TextAlign = HorizontalAlignment.Left;
					break;
				case StringAlignment.Center:
					_labelTextBox.TextAlign = HorizontalAlignment.Center;
					break;
				case StringAlignment.Far:
					_labelTextBox.TextAlign = HorizontalAlignment.Right;
					break;
			}	

			_labelTextBox.KeyPress += LabelTextBoxKeyPress;
			_labelTextBox.Focus();
			_center.X = textBox.Location.X + (textBox.Size.Width / 2);
			_center.Y = textBox.Location.Y + (textBox.Size.Height / 2);
		}

		public void EndEdit()
		{
			if (_siteLabelElement == null) return;
			
			_labelTextBox.KeyPress -= LabelTextBoxKeyPress;

			var lblCtrl = ControllerHelper.GetLabelController(_siteLabelElement);
			_labelElement.Size = MeasureTextSize();
			_labelElement.Text = _labelTextBox.Text;
			_labelTextBox.Hide();
			if (lblCtrl != null)
			{
				lblCtrl.SetLabelPosition();
			}
			else
			{
				_labelElement.PositionBySite(_siteLabelElement);
			}
			_labelElement.Invalidate();
			_siteLabelElement = null;
			_labelElement = null;
			_labelTextBox= null;
		}

		public static void SetTextBoxLocation(BaseElement el, TextBox tb)
		{
			if (!(el is ILabelElement)) return;

			var lab = ((ILabelElement) el).Label;

			el.Invalidate();
			lab.Invalidate();

			if (lab.Text.Length > 0)
			{
				tb.Location = lab.Location;
				tb.Size = lab.Size;
			}
			else
			{
				const string tmpText = "XXXXXXX";
				var sizeTmp = DiagramUtil.MeasureString(tmpText, lab.Font, lab.Size.Width, lab.Format);
				
				if (el is BaseLinkElement)
				{
					tb.Size = sizeTmp;
					tb.Location = new Point(el.Location.X + (el.Size.Width / 2) - (sizeTmp.Width / 2),
						el.Location.Y + (el.Size.Height / 2) - (sizeTmp.Height / 2));
				}
				else
				{
					sizeTmp.Width = el.Size.Width;
					tb.Size = sizeTmp;
					tb.Location = new Point(el.Location.X,
						el.Location.Y + (el.Size.Height / 2) - (sizeTmp.Height / 2));
				}
			}

			SetTextBoxBorder(tb);
		}

		private static void SetTextBoxBorder(TextBox tb)
		{
			Rectangle tbBox = new Rectangle(tb.Location, tb.Size);
			tbBox.Inflate(TextBoxBorder, TextBoxBorder);
			tb.Location = tbBox.Location;
			tb.Size = tbBox.Size;		
		}

		private Size MeasureTextSize()
		{
			var text = _labelTextBox.Text;
			var sizeTmp = Size.Empty;
			if (_direction == LabelEditDirection.UpDown)
				sizeTmp = DiagramUtil.MeasureString(text, _labelElement.Font, _labelTextBox.Size.Width, _labelElement.Format);
			else if (_direction == LabelEditDirection.Both)
				sizeTmp = DiagramUtil.MeasureString(text, _labelElement.Font);

			sizeTmp.Height += 30;

			return sizeTmp;
		}

		void LabelTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (_labelTextBox.Text.Length == 0) return;

			var size = _labelTextBox.Size;
			var sizeTmp = MeasureTextSize();

			if (_direction == LabelEditDirection.UpDown)
				size.Height = sizeTmp.Height;
			else if (_direction == LabelEditDirection.Both)
				size = sizeTmp;

			_labelTextBox.Size = size;
			_labelTextBox.Location = new Point(_center.X - (size.Width / 2), _center.Y - (size.Height / 2));
		}
	}
}

