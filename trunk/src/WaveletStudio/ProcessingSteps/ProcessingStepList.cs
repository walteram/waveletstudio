using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveletStudio.ProcessingSteps
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

        public new void Insert(int index, ProcessingStepBase item)
        {
            if (index >= Count)
                Add(item);
            else
                base.Insert(index, item);
            RecreateIndexes();
        }

        public new int RemoveAll(Predicate<ProcessingStepBase> match)
        {
            var retVal = base.RemoveAll(match);
            RecreateIndexes();
            return retVal;
        }

        private void RecreateIndexes()
        {
            for (var i = 0; i < Count; i++)
            {
                this[i].Index = i;
            }
        }

        public ProcessingStepBase GetStep(int key)
        {
            return this.FirstOrDefault(it => it.Key == key);
        }
    }
}
