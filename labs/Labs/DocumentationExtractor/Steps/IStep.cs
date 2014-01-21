using System.Collections.Generic;

namespace DocumentationExtractor.Steps
{
    internal interface IStep
    {
        void Run(List<Member> members);
    }
}