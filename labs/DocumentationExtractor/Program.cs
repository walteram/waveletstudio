using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DocumentationExtractor.Steps;

namespace DocumentationExtractor
{
    class Program
    {
        private static List<Member> _members;

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Console.WriteLine(@"Usage: DocumentationExtractor.exe ""c:\WaveletStudio.xml"" ""c:\output""");
            if (args.Length < 2)
            {
                throw new ArgumentOutOfRangeException();
            }
            _members = new List<Member>(128);
            Console.WriteLine(@"Generating documentation...");

            var xmlPath = args[0];
            var outputPath = args[1];

            var steps = new StepComposite
            {
                new ReflectionLoader(outputPath),
                new XmlMemberLoaderStep(xmlPath),
                new GenerateInOutGraph(outputPath),
                new CreateCodePlexMarkup(outputPath),
            };
            steps.Run(_members);
        }
    }
}
