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
using WaveletStudio.Designer.Utils;
using WaveletStudio.Functions;
using ZedGraph;

namespace DocumentationExtractor.Dynamic
{
    public class CLS_AbsoluteValueBlock_213344584
    {
        public static void RunExampleCode()
        {
            var signal1 = new ImportFromTextBlock { Text = "1, 3, 0, 7, 4, 8, 3, 1" };
            var signal2 = new ImportFromTextBlock { Text = "8, 4, 10, 2,  -5, 3, 7, -1" };
            var block = new ConvolutionBlock
            {
                FFTMode = ManagedFFTModeEnum.UseLookupTable,
                ReturnOnlyValid = false
            };

            signal1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signal2.OutputNodes[0].ConnectTo(block.InputNodes[1]);

            var blockList = new BlockList { signal1, signal2, block };
            blockList.ExecuteAll();
            Console.WriteLine(block.OutputNodes[0].Object.ToString(0));

            //Console Output:
            //8 28 22 88 61 138 126 93 48 37 32 56 16 4 -1
            DocumentationExtractor.Steps.GenerateInOutGraph.GetInputOutputGraphs(block, @"C:\Projetos\WaveletStudio\trunk\res\docs\documentation\images\inoutgraphs\AbsoluteValueBlock.png");
        }
    }
}