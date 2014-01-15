using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using WaveletStudio.Blocks;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Designer.Utils
{
    internal class CodeGenerator
    {
        private class GeneratedBlockInfo
        {
            public string VariableName { get; set; }
            public BlockBase Block { get; set; }
        }

        private StringBuilder _text;
        private List<GeneratedBlockInfo> _generatedBlocks;
        public List<BlockBase> BlockList { get; set; }
        public string ClassName { get; set; }

        public string GenerateCode()
        {
            _text = new StringBuilder(1024);
            _generatedBlocks = new List<GeneratedBlockInfo>();

            foreach (var block in BlockList.OrderBy(it => it.InputNodes.Count))
            {
                AddBlock(block);
            }
            AppendClassDeclarationCode();
            
            _text.AppendLine(Ident(3) + "//Declaring the blocks");
            foreach (var blockInfo in _generatedBlocks)
            {
                GenerateDeclarationCode(blockInfo);
            }
            _text.AppendLine();
            
            _text.AppendLine(Ident(3) + "//Connecting the blocks");
            foreach (var blockInfo in _generatedBlocks)
            {
                GenerateConnectionCode(blockInfo);
            }
            _text.AppendLine();

            _text.AppendLine(Ident(3) + "//Appending the blocks to a block list and execute all");
            _text.AppendLine(Ident(3) + "var blockList = new BlockList();");
            foreach (var blockInfo in _generatedBlocks)
            {
                _text.AppendLine(Ident(3) + "blockList.Add(" + blockInfo.VariableName + ");");
            }
            _text.AppendLine(Ident(3) + "blockList.ExecuteAll();");
            AppendClassEndCode();
            return _text.ToString();
        }

        private void AddBlock(BlockBase block)
        {
            if (_generatedBlocks.Any(it => it.Block == block))
            {
                return;
            }
            foreach (var inputNode in block.InputNodes)
            {
                if (inputNode.ConnectingNode != null && inputNode.ConnectingNode.Root != null)
                {
                    AddBlock(inputNode.ConnectingNode.Root);
                }
            }
            if (_generatedBlocks.Any(it => it.Block == block))
            {
                return;
            }
            _generatedBlocks.Add(new GeneratedBlockInfo
            {
                VariableName = GetVariableName(block),
                Block = block
            });
            foreach (var outputNode in block.OutputNodes)
            {
                if (outputNode.ConnectingNode != null && outputNode.ConnectingNode.Root != null)
                {
                    AddBlock(outputNode.ConnectingNode.Root);
                }
            }
        }

        private void AppendClassDeclarationCode()
        {
            _text.AppendLine("using WaveletStudio.Blocks;");
            _text.AppendLine("using WaveletStudio.Functions;");
            _text.AppendLine();
            _text.AppendLine("namespace WaveletStudioUserModels");
            _text.AppendLine("{");
            _text.AppendLine(Ident(1) + "public class " + ClassName);
            _text.AppendLine(Ident(1) + "{");
            _text.AppendLine(Ident(2) +     "public void RunModel()");
            _text.AppendLine(Ident(2) +     "{");
        }

        private void AppendClassEndCode()
        {
            _text.AppendLine(Ident(2) +     "}");
            _text.AppendLine(Ident(1) + "}");
            _text.AppendLine("}");
        }        

        private void GenerateDeclarationCode(GeneratedBlockInfo blockInfo)
        {
            //Variable declaration and paramaters
            var block = blockInfo.Block;
            var type = block.GetType();
            var parameterProperties = type.GetProperties().Where(property => property.GetCustomAttributes(true).OfType<Parameter>().Any()).ToList();
            var variableName = blockInfo.VariableName;
            _text.Append(Ident(3) + "var " + variableName + " = new " + type.Name);
            if (parameterProperties.Count == 0)
            {
                _text.AppendLine("();");
            }
            else
            {
                _text.AppendLine(Environment.NewLine + Ident(3) + "{");
                for (var i = 0; i < parameterProperties.Count; i++)
                {
                    var property = parameterProperties[i];
                    _text.AppendLine(Ident(4) + property.Name + " = " + GetPropertyValue(property, block) + (i < parameterProperties.Count -1 ? "," : ""));
                }
                _text.AppendLine(Ident(3) + "};");
            }
        }

        private void GenerateConnectionCode(GeneratedBlockInfo blockInfo)
        {
            //Output connections
            var block = blockInfo.Block;
            var variableName = blockInfo.VariableName;
            for (var i = 0; i < block.OutputNodes.Count; i++)
            {
                var outputNode = block.OutputNodes[i];
                var connectedTo = _generatedBlocks.Where(it => it.Block.InputNodes.Any(c => c.ConnectingNode == outputNode));
                foreach (var connectedBlock in connectedTo)
                {
                    for (var j = 0; j < connectedBlock.Block.InputNodes.Count; j++)
                    {
                        if (connectedBlock.Block.InputNodes[j].ConnectingNode == outputNode)
                        {
                            _text.AppendLine(Ident(3) + variableName + ".OutputNodes[" + i + "].ConnectTo(" + connectedBlock.VariableName + ".InputNodes[" + j + "]);");
                        }
                    }                    
                }
            }
        }

        private string GetVariableName(BlockBase block)
        {
            var baseVariableName = block.GetAssemblyClassName();
            if (!baseVariableName.ToLower().Contains("block"))
            {
                baseVariableName += "Block";
            }
            baseVariableName = char.ToLower(baseVariableName[0]) + baseVariableName.Substring(1);
            if(_generatedBlocks.All(it => it.VariableName != baseVariableName))
            {
                return baseVariableName;
            }
            var variableName = baseVariableName;
            var i = 1;
            while (_generatedBlocks.Any(it => it.VariableName == variableName))
            {
                i++;
                variableName = baseVariableName + i;
            }
            return variableName;
        }

        private string Ident(int tabCount)
        {
            const int tabSize = 4;
            return "".PadRight(tabCount*tabSize);
        }

        private string GetPropertyValue(PropertyInfo property, BlockBase block)
        {
            var value = property.GetValue(block, null);
            var propertyType = property.PropertyType;
            if (propertyType == typeof(Int32) || propertyType == typeof(Int64) || propertyType == typeof(Int16) || propertyType == typeof(UInt16) || propertyType == typeof(UInt32) || propertyType == typeof(UInt64) || propertyType == typeof(bool))
            {
                return value.ToString().ToLower();
            }
            if (propertyType.IsEnum)
            {
                var enumName = (propertyType.FullName + "." + Enum.GetName(propertyType, value)).Replace("+", ".");
                if (enumName.StartsWith("WaveletStudio.Blocks."))
                {
                    enumName = enumName.Replace("WaveletStudio.Blocks.", "");
                }
                if (enumName.StartsWith("WaveletStudio.Functions."))
                {
                    enumName = enumName.Replace("WaveletStudio.Functions.", "");
                }
                return enumName;
            }
            if (propertyType == typeof(double) || propertyType == typeof(decimal) || propertyType == typeof(Single) || propertyType == typeof(float))
            {
                return Convert.ToDecimal(value).ToString("0.0###############", CultureInfo.InvariantCulture);
            }
            return "\"" + value + "\"";
        }
    }
}
