using System.Collections.Generic;

namespace DocumentationExtractor
{
    public class Member
    {
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public string Description { get; set; }

        public string Inputs { get; set; }

        public string Outputs { get; set; }

        public string Parameters { get; set; }

        public Dictionary<string, string> ParameterFullNames { get; set; }

        public string ExampleDescription { get; set; }

        public string ExampleCode { get; set; }

        public string Image { get; set; }

        public string InOutGraph { get; set; }

        public string Category { get; set; }
        
        public string Type { get; set; }
        
        public string Title { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}