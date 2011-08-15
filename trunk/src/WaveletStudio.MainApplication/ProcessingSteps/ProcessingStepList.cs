using System.Collections.Generic;

namespace WaveletStudio.MainApplication.ProcessingSteps
{
    public class ProcessingStepList : List<ProcessingStepBase>
    {
        public Signal Process()
        {
            for (var i = 0; i < Count; i++)
            {
                var item = this[i];
                var previousItem = i != 0 ? this[i - 1] : null;
                item.Process(previousItem);
                if (i == Count-1)
                {
                    return item.Signal;
                }
            }
            return null;
        }
    }
}
