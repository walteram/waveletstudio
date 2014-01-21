using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DocumentationExtractor.Steps
{
    internal class XmlMemberLoaderStep : IStep
    {
        private readonly string _xmlPath;

        public XmlMemberLoaderStep(string xmlPath)
        {
            _xmlPath = xmlPath;     
        }

        public void Run(List<Member> members)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_xmlPath);            
            foreach (XmlNode xmlNode in xmlDocument.ChildNodes[1].ChildNodes[1].ChildNodes)
            {
                if (xmlNode.Attributes == null)
                {
                    continue;
                }
                var name = xmlNode.Attributes["name"].Value;
                name = name.Substring(name.LastIndexOf(".", StringComparison.InvariantCulture) + 1);
                
                var member = members.FirstOrDefault(it => it.Name == name);
                if (member == null)
                {
                    continue;
                }

                var summaryNode = xmlNode.ChildNodes[0];
                if (summaryNode.ChildNodes.Count == 0 || summaryNode.ChildNodes[0] is XmlText)
                {
                    member.Description = summaryNode.InnerText.TrimAll();
                }
                else
                {
                    ProcessSumaryNode(summaryNode, member);
                }
                if (!string.IsNullOrWhiteSpace(member.ExampleCode) && string.IsNullOrWhiteSpace(member.ExampleDescription))
                {
                    member.ExampleDescription = "The following example shows an usage in C#.";
                }
                if(member.ParameterFullNames != null)
                {
                    foreach (var parameter in member.ParameterFullNames)
                    {
                        LoadParameterDescription(xmlDocument, member, xmlNode.Attributes["name"].Value.Substring(2), parameter.Key, parameter.Value);
                    }
                }
            }
        }

        private static void LoadParameterDescription(XmlNode xmlDocument, Member member, string memberFullName, string parameterName, string parameterType)
        {
            var propertyName = "P:" + memberFullName + "." + parameterName;
            foreach (XmlNode xmlNode in xmlDocument.ChildNodes[1].ChildNodes[1].ChildNodes)
            {
                if (xmlNode.Attributes == null || xmlNode.Attributes["name"].Value != propertyName)
                {
                    continue;
                }

                member.Parameters = member.Parameters.AppendLine(Environment.NewLine);
                member.Parameters = member.Parameters.AppendLine(Environment.NewLine);
                var i = 0;
                member.Parameters = member.Parameters.Append(parameterName + ": ");
                foreach (XmlNode childNode in xmlNode.ChildNodes[0].ChildNodes)
                {
                    if (i > 0)
                    {
                        member.Parameters += Environment.NewLine;
                    }
                    i++;
                    member.Parameters = member.Parameters.Append(childNode.InnerText.TrimAll());   
                }
                break;
            }

            if (string.IsNullOrEmpty(parameterType))
            {                
                return;
            }

            var typeStartName = "F:" + parameterType.Replace('+','.') + ".";
            foreach (XmlNode xmlNode in xmlDocument.ChildNodes[1].ChildNodes[1].ChildNodes)
            {
                if (xmlNode.Attributes == null || !xmlNode.Attributes["name"].Value.StartsWith(typeStartName))
                {
                    continue;
                }
                var name = xmlNode.Attributes["name"].Value.Substring(xmlNode.Attributes["name"].Value.LastIndexOf('.')+1);

                member.Parameters = member.Parameters.AppendLine(" > " + name + " - " + xmlNode.InnerText.TrimAll());                
            }            
        }

        private static void ProcessSumaryNode(XmlNode summaryNode, Member member)
        {
            foreach (XmlNode summaryChildNode in summaryNode.ChildNodes)
            {
                var value = summaryChildNode.InnerText.TrimAll();
                var tagName = summaryChildNode.Name.ToLower();
                if (tagName == "para")
                {
                    ProcessParaNode(member, value);
                }
                else if (tagName == "code")
                {
                    member.Description += "\"}" + Environment.NewLine + "{code:c#}" + ProcessCode(value) + Environment.NewLine + "{code:c#}" + Environment.NewLine + "{\"";
                }
                else if (tagName == "example")
                {
                    ProcessExampleNode(member, summaryChildNode, value);
                }
                else
                {
                    member.Description = member.Description.AppendLine(value);
                }
            }
        }

        private static void ProcessExampleNode(Member member, XmlNode summaryChildNode, string value)
        {
            if (summaryChildNode.ChildNodes.Count == 0 || summaryChildNode.ChildNodes[0] is XmlText)
            {
                member.ExampleDescription = member.ExampleDescription.AppendLine(value);
            }
            else
            {
                ProcessSummaryChildNodes(member, summaryChildNode.ChildNodes);
            }
        }

        private static void ProcessSummaryChildNodes(Member member, XmlNodeList summaryChildNodes)
        {
            foreach (XmlNode exampleChildNode in summaryChildNodes)
            {
                var value = exampleChildNode.InnerText.TrimAll();
                if (exampleChildNode.Name.ToLower() == "code")
                {
                    member.ExampleCode = ProcessCode(value);
                }
                else
                {
                    member.ExampleDescription = member.ExampleDescription.AppendLine(value);
                }
            }
        }

        private static string ProcessCode(string value)
        {
            var brackCount = 0;
            var codeLines = value.Split('\n');
            var text = "";
            foreach (var codeLine in codeLines)
            {
                var line = codeLine.TrimAll();
                if (brackCount > 0)
                {
                    if (line.StartsWith("}"))
                    {
                        brackCount--;
                    }
                    line = "".PadLeft(brackCount*4) + line;
                }
                text = text + Environment.NewLine + line;
                if (line.TrimAll().StartsWith("{"))
                {
                    brackCount++;
                }
            }
            return text;
        }

        private static void ProcessParaNode(Member member, string paraNodeValue)
        {
            if (paraNodeValue.ToLower().StartsWith("inputs:"))
            {
                member.Inputs = member.Inputs.AppendLineSection(paraNodeValue);
            }
            else if (paraNodeValue.ToLower().StartsWith("outputs:"))
            {
                member.Outputs = member.Outputs.AppendLineSection(paraNodeValue);
            }
            else if (paraNodeValue.ToLower().StartsWith("image:"))
            {
                member.Image = member.Image.AppendLineSection(paraNodeValue);
            }
            else if (paraNodeValue.ToLower().StartsWith("inoutgraph:"))
            {
                member.InOutGraph = member.InOutGraph.AppendLineSection(paraNodeValue);
            }
            else if (paraNodeValue.ToLower().StartsWith("docurl:"))
            {
                member.DocUrl = member.DocUrl.AppendLineSection(paraNodeValue);
            }
            else
            {
                member.Description = member.Description.AppendLine(paraNodeValue);
            }
        }        
    }
}
