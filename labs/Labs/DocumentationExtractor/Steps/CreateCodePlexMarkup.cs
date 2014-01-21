using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocumentationExtractor.Steps
{
    internal class CreateCodePlexMarkup : IStep
    {
        private readonly string _docPath;

        public CreateCodePlexMarkup(string docPath)
        {
            _docPath = docPath;
        }

        public void Run(List<Member> members)
        {
            foreach (var member in members)
            {
                var text = GetText(member);
                var filename = Path.Combine(_docPath, member.Name + ".txt");
                File.WriteAllText(filename, text);
            }
        }

        private string GetText(Member member)
        {
            var text = new StringBuilder(256);
            text.Append("! ");
            if (!string.IsNullOrEmpty(member.Image))
            {
                text.Append(">[image:" + member.Image + "]");
            }
            text.AppendLine("*" + member.Type + ": " + (member.FriendlyName ?? member.Name) + "*");
            text.AppendLine();
            text.AppendLine("{\"" + member.Description + "\"}");
            text.AppendLine();
            AppendSection("Parameters", member.Parameters, text);
            AppendSection("Inputs", member.Inputs, text);
            AppendSection("Outputs", member.Outputs, text);            
            if (!string.IsNullOrEmpty(member.ExampleCode) && !string.IsNullOrEmpty(member.ExampleDescription))
            {
                text.AppendLine("!! *Example:*");
                text.AppendLine();
                text.AppendLine("{\"" + member.ExampleDescription + "\"}");
                text.AppendLine();
                text.AppendLine("{code:c#}");
                text.AppendLine(member.ExampleCode.TrimAll());
                text.AppendLine("{code:c#}");
                text.AppendLine();
                if (!string.IsNullOrEmpty(member.InOutGraph))
                {
                    text.AppendLine("The above example generates the following set of inputs and outputs:");
                    text.Append("[image:" + member.InOutGraph + "]");
                    text.AppendLine();
                }
            }
            text.AppendLine();
            return text.ToString();
        }

        private static void AppendSection(string sectionName, string value, StringBuilder text)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            text.AppendLine("!! *" + sectionName + ":*");
            text.AppendLine();
            AppendKeyValue(value, text);
            text.AppendLine();
        }

        private static void AppendKeyValue(string value, StringBuilder text)
        {
            foreach (var line in value.Split('\n'))
            {
                var index1 = line.IndexOf(':');
                var index2 = line.IndexOf(' ');
                if (index1 == -1 || index2 < index1)
                {
                    text.AppendLine(line.TrimAll());
                }
                else
                {
                    text.AppendLine("*" + line.Substring(0, index1) + ":* {\"" + line.Substring(index1 + 1).TrimAll() + "\"}");
                }
            }
        }
    }
}
