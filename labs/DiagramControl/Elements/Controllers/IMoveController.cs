using System.Drawing;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// If a class controller implements this interface, it can move the element.
	/// </summary>
	internal interface IMoveController : IController
	{
		bool IsMoving {get;}

		void Start(Point posStart);
		void Move(Point posCurrent);
        bool WillMove(Point posCurrent);
		void End();

		bool CanMove {get;}
	}
}
