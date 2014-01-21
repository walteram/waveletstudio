using System.Collections.Generic;

namespace DocumentationExtractor.Steps
{
    internal class StepComposite : List<IStep>, IStep
    {
        public void Run(List<Member> members)
        {
            foreach (var step in this)
            {
                step.Run(members);
            }
        }
    }
}
