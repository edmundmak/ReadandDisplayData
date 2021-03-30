using System;
using Microsoft.Extensions.DependencyInjection;
using ReadData;

namespace ReadandDisplayData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please supply the file name. For example BDG_Input.txt");
            string inputString = Console.ReadLine();
            // instantiate startup
            // all the constructor logic would happen
            var startup = new Startup();
            // request an instance of type ISomeService
            // from the ServicePipeline built
            // returns an object of type SomeService
            readAndConvertToJson(startup, inputString);
        }
        private static void readAndConvertToJson(Startup startup, string inputString)
        {
            var service = startup.Provider.GetRequiredService<IReadData>();
            Console.WriteLine(service.ConvertToJsonFromTextFile(inputString));
        }
    }
}
