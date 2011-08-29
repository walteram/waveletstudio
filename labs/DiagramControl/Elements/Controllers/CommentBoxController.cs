namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for CommentBoxElement
	/// </summary>
	internal class CommentBoxController: RectangleController, ILabelController
	{
		public CommentBoxController(BaseElement element): base(element) {}

		public void SetLabelPosition()
		{
			var label = ((ILabelElement) El).Label;
			label.Location = El.Location;
			label.Size = El.Size;
		}
	}
}
