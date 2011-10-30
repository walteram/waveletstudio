using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class BlocksConnectionTest
    {
        [TestMethod]
        public void TestConnection()
        {
            var signalBlock = new GenerateSignalBlock{TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true};
            var sumBlock = new ScalarOperationBlock { Operation = WaveMath.OperationEnum.Sum, Value = 1.5 };
            var multBlock = new ScalarOperationBlock { Operation = WaveMath.OperationEnum.Multiply, Value = 2 };

            var signalOutputNode = signalBlock.OutputNodes[0];
            var sumInputNode = sumBlock.InputNodes[0];
            var sumOutputNode = sumBlock.OutputNodes[0];
            var multInputNode = multBlock.InputNodes[0];
            var multOutputNode = multBlock.OutputNodes[0];
            
            signalOutputNode.ConnectTo(ref sumInputNode);
            sumOutputNode.ConnectTo(ref multInputNode);

            signalBlock.Execute();

            var outputText = multOutputNode.Object.ToString(0);

            Assert.AreEqual("3 5 3 5 3", outputText);
        }

        [TestMethod]
        [DeploymentItem("example.csv")]
        public void TestCloneWithLinks()
        {
            AssertBlock(new ConvolutionBlock());
            AssertBlock(new DownSampleBlock());
            AssertBlock(new ExportToCSVBlock());
            AssertBlock(new FFTBlock());
            AssertBlock(new GenerateSignalBlock());
            AssertBlock(new IFFTBlock());
            AssertBlock(new ScalarOperationBlock());
            AssertBlock(new RampFunctionBlock());
            AssertBlock(new SampleBasedOperationBlock());
            AssertBlock(new SignalExtensionBlock());
            AssertBlock(new ImportFromCSVBlock());
            AssertBlock(new ImportFromTextBlock{Text = "1234"});
            AssertBlock(new DWTBlock());
            AssertBlock(new UpSampleBlock());
            AssertBlock(new WaveletBlock());
            AssertBlock(new IDWTBlock());
            AssertBlock(new AbsoluteValueBlock());
            AssertBlock(new UniqueBlock());
            AssertBlock(new InvertBlock());
            AssertBlock(new ShiftBlock());
            AssertBlock(new InterpolationBlock());
            AssertBlock(new RepeatBlock());
        }

        private static void AssertBlock(BlockBase block)
        {
            var blockList = new BlockList { block };
            foreach (var inputNode in block.InputNodes)
            {
                var signalBlock = new GenerateSignalBlock
                                      {
                                          TemplateName = "Binary",
                                          Start = 0,
                                          Finish = 4,
                                          SamplingRate = 1,
                                          IgnoreLastSample = true
                                      };
                signalBlock.OutputNodes[0].ConnectTo(inputNode);
                blockList.Add(signalBlock);
            }            
            blockList.ExecuteAll();
            var clone = block.CloneWithLinks();
            foreach (var outputNode in clone.OutputNodes)
            {
                Assert.IsNotNull(outputNode.Object[0].Samples);
            }

            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);
        }
    }
}
