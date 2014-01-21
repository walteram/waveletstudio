using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using DiagramNet.Elements;
using WaveletStudio.Blocks;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Designer.Utils;

namespace DocumentationExtractor.Steps
{
    internal class ReflectionLoader : IStep
    {
        private readonly string _docPath;

        public ReflectionLoader(string docPath)
        {
            _docPath = docPath;
        }

        public void Run(List<Member> members)
        {
            foreach (var type in WaveletStudio.Utils.GetTypes(typeof(BlockBase).Namespace).Where(t => t.BaseType == typeof(BlockBase)).OrderBy(BlockBase.GetName))
            {
                var block = (BlockBase) Activator.CreateInstance(type);
                var blockName = type.Name;
                var add = false;
                var member = members.FirstOrDefault(it => it.Name == blockName);
                if (member == null)
                {
                    add = true;
                    member = new Member { Name = blockName };
                }
                member.Type = "Block";
                member.FriendlyName = ApplicationUtils.GetResourceString(block.Name);
                member.Category = BlockBase.GetProcessingTypeName(block.ProcessingType);
                if (block.HasParameters())
                {
                    var properties = block.GetProperties();
                    member.ParameterFullNames = new Dictionary<string, string>();
                    foreach (GlobalizedPropertyDescriptor property in properties)
                    {
                        member.ParameterFullNames.Add(property.Name, property.PropertyType.IsEnum ? property.PropertyType.FullName : "");                        
                    }                    
                }               

                SaveImage(block, type, blockName, block.GetAssemblyClassName());
                if (add)
                {
                    members.Add(member);
                }
            }
        }

        public void SaveImage(BlockBase block, Type type, string blockName, string assemblyClassName)
        {
            var imagePath = Path.Combine(_docPath, "images", "blocks", blockName + ".png");            
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + block.GetAssemblyClassName() + "Mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"), true);
            
            var img = diagramBlock.GetImage();
            img.Save(imagePath, ImageFormat.Png);                     
        }
    }
}
