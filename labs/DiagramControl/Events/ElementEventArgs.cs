using System;
using DiagramNet.Elements;

namespace DiagramNet.Events
{
    public class ElementEventArgs: EventArgs
    {
        private readonly BaseElement _element;
        private readonly BaseElement _previousElement;

        public ElementEventArgs(BaseElement el)
        {
            _element = el;
        }

        public ElementEventArgs(BaseElement el, BaseElement previousEl)
        {
            _element = el;
            _previousElement = previousEl;
        }

        public BaseElement Element
        {
            get
            {
                return _element;
            }
        }

        public BaseElement PreviousElement
        {
            get
            {
                return _previousElement;
            }
        }

        public override string ToString()
        {
            return "el: " + _element.GetHashCode();
        }


    }
}
