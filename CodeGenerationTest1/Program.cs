using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            //new DesignTimeCodeGeneratorTest().TestMethod();
            Console.Write(new ServiceModelRuntimeGenerator("Hello").TransformText());
            Console.Write(new ServiceClassRuntimeGenerator("Hello").TransformText());

            Console.ReadLine();
        }


    }

    partial class RuntimeTestTemplate
    {

        string testinput;
        public RuntimeTestTemplate(string input)
        {
            testinput = input;
        }

    }
    partial class ServiceModelRuntimeGenerator
    {

        string testinput;
        public ServiceModelRuntimeGenerator(string input)
        {
            testinput = input;
        }

    }
    partial class ServiceClassRuntimeGenerator
    {

        string testinput;
        public ServiceClassRuntimeGenerator(string input)
        {
            testinput = input;
        }

    }
}
