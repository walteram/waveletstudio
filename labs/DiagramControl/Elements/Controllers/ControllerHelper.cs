namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class helps with Controllers.
	/// </summary>
	internal class ControllerHelper
	{
		private ControllerHelper()
		{
			
		}

		public static IMoveController GetMoveController(BaseElement el)
		{
		    if (el is IControllable)
			{
				var ctrl = ((IControllable) el).GetController();
				if (ctrl is IMoveController)
					return (IMoveController) ctrl;
			}
		    return null;
		}

	    public static IResizeController GetResizeController(BaseElement el)
	    {
	        if (el is IControllable)
			{
				var ctrl = ((IControllable) el).GetController();
				return GetResizeController(ctrl);
			}
	        return null;
	    }

	    public static IResizeController GetResizeController(IController ctrl)
		{
			return ctrl as IResizeController;
		}

		public static ILabelController GetLabelController(BaseElement el)
		{
		    if ((el is IControllable) && (el is ILabelElement))
			{
				var ctrl = ((IControllable) el).GetController();
				return GetLabelController(ctrl);
			}
		    return null;
		}

	    public static ILabelController GetLabelController(IController ctrl)
		{
			return ctrl as ILabelController;
		}
	}
}
