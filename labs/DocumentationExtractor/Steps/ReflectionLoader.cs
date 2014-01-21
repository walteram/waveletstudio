using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
            LoadBlocks(members);
        }

        private void LoadBlocks(ICollection<Member> members)
        {
            foreach (var type in WaveletStudio.Utils.GetTypes(typeof (BlockBase).Namespace).Where(t => t.BaseType == typeof (BlockBase)).OrderBy(BlockBase.GetName))
            {
                if (type.CustomAttributes.Any(c => c.AttributeType == typeof(ObsoleteAttribute)))
                {
                    continue;
                }
                LoadMemberFromBlock(members, type);
            }
        }

        private void LoadMemberFromBlock(ICollection<Member> members, Type type)
        {
            var block = (BlockBase) Activator.CreateInstance(type);
            var blockName = type.Name;
            var member = members.FirstOrDefault(m => m.Name == blockName && m.Type == "Block");
            if (member == null)
            {
                member = new Member
                {
                    Name = blockName,
                    Type = "Block",
                    FriendlyName = ApplicationUtils.GetResourceString(block.Name),
                    Category = BlockBase.GetProcessingTypeName(block.ProcessingType)
                };
                members.Add(member);
            }
            if (block.HasParameters())
            {
                LoadBlockParameters(block, member);
            }
            SaveBlockImage(block, type, blockName, block.GetAssemblyClassName());
        }

        private static void LoadBlockParameters(ICustomTypeDescriptor block, Member member)
        {
            var properties = block.GetProperties();
            member.ParameterFullNames = new Dictionary<string, string>();
            foreach (GlobalizedPropertyDescriptor property in properties)
            {
                member.ParameterFullNames.Add(property.Name, property.PropertyType.IsEnum ? property.PropertyType.FullName : "");
            }
        }

        public void SaveBlockImage(BlockBase block, Type type, string blockName, string assemblyClassName)
        {
            var imagePath = Path.Combine(_docPath, "images", "blocks", blockName + ".png");            
            var diagramBlock = ApplicationUtils.CreateDiagramBlock(block, false);            
            var img = diagramBlock.GetImage();
            img.Save(imagePath, ImageFormat.Png);                     
        }        
    }
}
