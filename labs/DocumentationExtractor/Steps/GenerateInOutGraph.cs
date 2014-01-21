using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using WaveletStudio;
using WaveletStudio.Blocks;
using WaveletStudio.Designer.Resources;
using ZedGraph;

namespace DocumentationExtractor.Steps
{
    public class GenerateInOutGraph : IStep
    {
        private readonly string _docPath;

        public GenerateInOutGraph(string docPath)
        {
            _docPath = docPath;
        }

        public void Run(List<Member> members)
        {
            foreach (var member in members)
            {
                if (string.IsNullOrWhiteSpace(member.ExampleCode))
                {
                    continue;
                }
                var compiledType = LoadCompiledExampleType(member);
                var method = compiledType.GetMethod("RunExampleCode");
                method.Invoke(null, null);
            }
        }

        private Type LoadCompiledExampleType(Member member)
        {
            using (var provider = new CSharpCodeProvider())
            {
                var parameters = new CompilerParameters { GenerateInMemory = true };
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add(Path.Combine(Environment.CurrentDirectory, "DocumentationExtractor.exe"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Environment.CurrentDirectory, "WaveletStudio.Designer.exe"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Environment.CurrentDirectory, "WaveletStudio.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Environment.CurrentDirectory, "ZedGraph.dll"));
                string fullClassName;
                var imagePath = Path.Combine(_docPath, "images", "inoutgraphs", member.Name + ".png");
                var results = provider.CompileAssemblyFromSource(parameters, GetCSharpCode(member, imagePath, out fullClassName));
                if (results.Errors.HasErrors)
                {
                    throw new Exception(results.Errors[0].ErrorText);
                }
                var type = results.CompiledAssembly.GetType(fullClassName);
                return type;
            }                                  
        }

        private static string[] GetCSharpCode(Member member, string imagePath, out string fullClassName)
        {
            const string namespaceName = "DocumentationExtractor.Dynamic";
            var className = "CLS_" + member.Name;
            if (className.Length > 30)
            {
                className = className.Substring(0, 30);
            }
            className = className + "_" + new Random().Next();
            fullClassName = namespaceName + "." + className;
            var ret = new[]
            {
                @"  using System;
                    using System.CodeDom.Compiler;
                    using System.Collections.Generic;
                    using System.Drawing;
                    using System.Drawing.Imaging;
                    using System.IO;
                    using System.Linq;
                    using Microsoft.CSharp;
                    using WaveletStudio;
                    using WaveletStudio.Blocks;
                    using WaveletStudio.Designer.Resources;
                    using WaveletStudio.Designer.Utils;
                    using WaveletStudio.Functions;
                    using ZedGraph;

                    namespace " + namespaceName + @"
                    {
                        public class " + className + @"
                        {
                            public static void RunExampleCode()
                            {
                                "+
                                member.ExampleCode +
                                Environment.NewLine +
                                typeof(GenerateInOutGraph).FullName + @".GetInputOutputGraphs(block, @"""+ imagePath +@""");
                            }
                        }
                    }".Trim()
            };
            return ret;
        }

        public static void GetInputOutputGraphs(BlockBase block, string filePath)
        {
            var images = new List<Image>();

            images.AddRange(GetNodeImages(block, true, block.InputNodes.Select(n => n.ConnectingNode as BlockOutputNode), n => n.ConnectingNode.ShortName));
            images.AddRange(GetNodeImages(block, true, block.OutputNodes, n => n.ShortName));

            MergeImages(images, filePath);
        }

        private static IEnumerable<Image> GetNodeImages(BlockBase block, bool joinSignals, IEnumerable<BlockOutputNode> nodes, Func<BlockOutputNode, string> getNameFunc)
        {
            foreach (var node in nodes)
            {
                if (node == null || node.Object == null)
                {
                    continue;
                }
                var signalName = getNameFunc(node);
                if (joinSignals)
                {
                    var image = SaveGraph(block, node.Object, signalName);
                    yield return image;
                }
                else
                {
                    foreach (var signal in node.Object)
                    {
                        var image = SaveGraph(block, new[] { signal }, signalName);
                        yield return image;
                    }   
                }                
            }
        }
        
        private static void MergeImages(IReadOnlyList<Image> images, string filePath)
        {
            var width = images.Count > 1 ? images[0].Width * 2 + 2 : images[0].Width;
            var rows = images.Count / 2;
            if (images.Count % 2 == 1)
            {
                rows++;
            }
            var height = rows * images[0].Height + rows * 2;

            var finalImage = new Bitmap(width, height);
            var graph = Graphics.FromImage(finalImage);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, finalImage.Width, finalImage.Height);

            var x = 0;
            var y = 0;
            for (var i = 0; i < images.Count; i++)
            {
                graph.DrawImage(images[i], new Point(x, y));
                if (i%2 == 0)
                {
                    x += images[i].Width + 2;
                }
                else
                {
                    x = 0;
                    y += images[i].Height + 2;
                }
            }

            finalImage.Save(filePath, ImageFormat.Png);
        }
        
        private static Image SaveGraph(BlockBase block, IEnumerable<Signal> signals, string title)
        {
            var minX = double.MaxValue;
            var maxX = double.MinValue;
            var minY = double.MaxValue;
            var maxY = double.MinValue;
            var pane = new GraphPane(new RectangleF(0, 0, 324, 243), title, "", "");
            var colors = new[] {Color.Red, Color.Blue, Color.DarkGreen, Color.SaddleBrown, Color.Magenta};
            var colorIndex = 0;
            var i = 0;
            foreach (var signal in signals)
            {
                var samples = signal.GetSamplesPair().ToList();

                var yAxys = new PointPairList();
                yAxys.AddRange(samples.Select(it => new PointPair(it[1], it[0])));

                if (title == "Abs" || 
                    block.Name == "IFFT" && title == "In" ||
                    block.GetType().Name == "RelationalOperatorBlock" && title == "Out")
                {
                    pane.AddBar(title, yAxys, Color.Blue);
                }
                else
                {
                    pane.AddCurve(title, yAxys, colors[colorIndex], SymbolType.None);   
                }
                
                colorIndex++;
                if (colorIndex >= colors.Length)
                {
                    colorIndex = 0;
                }

                if (signal.CustomPlot != null && signal.CustomPlot.Length > 0 && signal.CustomPlot.Length == 2)
                {
                    var minValue = signal.Samples.Min()*1.1;
                    var maxValue = signal.Samples.Max()*1.1;

                    var area = new PointPairList
                    {
                        {signal.CustomPlot[0], minValue},
                        {signal.CustomPlot[0], maxValue},
                        {signal.CustomPlot[0], maxValue},
                        {signal.CustomPlot[1], maxValue},
                        {signal.CustomPlot[1], maxValue},
                        {signal.CustomPlot[1], minValue},
                        {signal.CustomPlot[1], minValue},
                        {signal.CustomPlot[0], minValue}
                    };
                    pane.AddCurve(DesignerResources.PreviousSize, area, Color.Orange, SymbolType.None);
                }
                var localMinY = signal.Samples.Min();
                var localMaxY = signal.Samples.Max();
                var localMinX = samples.Min(it => it[1]);
                var localMaxX = samples.Max(it => it[1]);
                if (localMinY < minY)
                {
                    minY = localMinY;
                }
                if (localMaxY > maxY)
                {
                    maxY = localMaxY;
                }
                if (localMinX < minX)
                {
                    minX = localMinX;
                }
                if (localMaxX > maxX)
                {
                    maxX = localMaxX;
                }
                i++;
            }

            pane.Legend.IsVisible = false;
            pane.XAxis.Title.IsVisible = false;
            pane.XAxis.Scale.Min = minX;
            pane.XAxis.Scale.Max = maxX;
            pane.YAxis.Title.IsVisible = false;
            pane.YAxis.Scale.Min = minY;
            pane.YAxis.Scale.Max = maxY;
            pane.AxisChange();
            return pane.GetImage();
        }
    }
}
