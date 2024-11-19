namespace XMLAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    internal class Program
    {
        private static async Task Main(string[] arguments)
        {
            foreach (var argument in arguments)
            {
                Console.WriteLine($"{nameof(argument)}:{argument}");

                if (File.Exists(argument))
                {
                    Console.WriteLine($"File {Path.GetFileName(argument)} is found");

                    var fileFullname = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(argument));
                    await OpenXml(fileFullname);
                }
                else
                {
                    Console.WriteLine("File is not found");
                }
            }

            Console.ReadKey();
        }

        private static async Task OpenXml(string fileFullname)
        {
            var fileText = await File.ReadAllTextAsync(fileFullname);

            try
            {
                var xDocument = XDocument.Parse(fileText, LoadOptions.None);

                AnalyzeXml(xDocument);
            }
            catch (Exception exception) //todo
            {
                Console.WriteLine($"File {Path.GetFileName(fileFullname)} does not contain xml or contains errors");
            }
        }

        private static void AnalyzeXml(XDocument xDocument)
        {
            var elements = GetElements(xDocument.Root);
            
            Console.WriteLine($"Elements count:{elements.Length}");

            OutputElementsToConsole(elements);

            OutputUniqueElementsToConsole(elements);

            OutputAllAttributesToConsole(elements);
        }

        private static void OutputAllAttributesToConsole(XElement[] elements)
        {
            Console.WriteLine("Looping through attributes of elements");
            foreach (var xElement in elements)
            {
                if (xElement.Attributes().Any())
                {
                    Console.WriteLine($"\t{xElement.Name} contains attributes:");
                    foreach (var xAttribute in xElement.Attributes())
                    {
                        Console.WriteLine($"\t\t{xAttribute.Name}={xAttribute.Value}");
                    }
                }
            }
        }

        private static void OutputElementsToConsole(XElement[] elements)
        {
            Console.WriteLine("List of all elements");
            foreach (var xElement in elements)
            {
                Console.WriteLine($"\t{xElement.Name}");
            }
        }

        private static void OutputUniqueElementsToConsole(XElement[] elements)
        {
            Console.WriteLine("List of unique elements and their quantity");
            foreach (var xElement in elements.Select(x => x.Name.ToString())
                                             .Distinct())
            {
                Console.WriteLine($"\t{xElement,-20}\t{elements.Count(x => x.Name == xElement)}");
            }
        }

        private static XElement[] GetElements(XElement xElement)
        {
            var xElements = new List<XElement> { xElement };

            foreach (var element in xElement.Elements())
            {
                xElements.AddRange(GetElements(element));
            }

            return xElements.ToArray();
        }
    }
}