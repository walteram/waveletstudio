using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class RelationalOperatorBlockTest
    {
        [TestMethod]
        public void TestRelationalOperatorBlockTest()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            var signalBlock1 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var signalBlock2 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, Phase = 0.5, SamplingRate = 1, IgnoreLastSample = true };
            var block = new RelationalOperatorBlock { Operation = WaveMath.RelationalOperatorEnum.GreaterThan };
            block.Execute();
            signalBlock1.Execute();
            signalBlock2.Execute();
            
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);
            Assert.AreEqual("GreaterThan", block.GetAssemblyClassName());

            block.Operand = RelationalOperatorBlock.OperandEnum.Signal;
            block.Execute();
            Assert.AreEqual("0 1 0 1 0", block.OutputNodes[0].Object.ToString(0));

            block.Operand = RelationalOperatorBlock.OperandEnum.PreviousSample;
            block.Operation = WaveMath.RelationalOperatorEnum.LessThan;
            block.Execute();
            Assert.AreEqual("0 0 1 0 1", block.OutputNodes[0].Object.ToString(0));

            block.Operand = RelationalOperatorBlock.OperandEnum.NextSample;
            block.Operation = WaveMath.RelationalOperatorEnum.LessThan;
            block.Execute();
            Assert.AreEqual("1 0 1 0 0", block.OutputNodes[0].Object.ToString(0));

            block.Operand = RelationalOperatorBlock.OperandEnum.StaticValue;
            block.Operation = WaveMath.RelationalOperatorEnum.GreaterOrEqualsThan;
            block.ScalarValue = 1;
            block.Execute();
            Assert.AreEqual("0 1 0 1 0", block.OutputNodes[0].Object.ToString(0));
           
            var block2 = (RelationalOperatorBlock)block.Clone();
            block2.Operation = WaveMath.RelationalOperatorEnum.GreaterOrEqualsThan;
            block.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block2.InputNodes[1]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("0 1 0 1 0", block2.OutputNodes[0].Object[0].ToString(0));

            block.Cascade = false;
            block2 = (RelationalOperatorBlock)block.Clone();
            block.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            block.Operand = RelationalOperatorBlock.OperandEnum.Signal;
            signalBlock1.Execute();
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0));

            signalBlock1 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 3, SamplingRate = 1, IgnoreLastSample = true };
            signalBlock2 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            signalBlock2.OutputNodes[0].Object.Add(new Signal(new double[] { 1, 2, 3, 4 }));
            block.Execute();
            Assert.AreEqual("1 1 1 0 0", block.OutputNodes[0].Object[0].ToString(0));

            signalBlock2.OutputNodes[0].Object.Clear();
            block.Execute();
            Assert.AreEqual(1, block.OutputNodes[0].Object.Count);
            Assert.AreEqual("1 1 1", block.OutputNodes[0].Object[0].ToString(0));

            block2.InputNodes[1].ConnectingNode = null;
            block2.Operand = RelationalOperatorBlock.OperandEnum.Signal;
            block2.Execute();
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);

            block2 = (RelationalOperatorBlock)block.Clone();
            block2.Operand = RelationalOperatorBlock.OperandEnum.Signal;
            block.ConnectTo(block2);
            signalBlock1.ConnectTo(block2);
            block2.Operand = RelationalOperatorBlock.OperandEnum.Signal;
            block2.Execute();
            Assert.AreEqual("1 1 1", block2.OutputNodes[0].Object[0].ToString(0));

            ((BlockOutputNode)block2.InputNodes[0].ConnectingNode).Object.Add(signalBlock1.OutputNodes[0].Object[0].Clone());
            block2.Execute();
            Assert.AreEqual("1 1 1", block2.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 1 1", block2.OutputNodes[0].Object[1].ToString(0));
        }
    }
}
