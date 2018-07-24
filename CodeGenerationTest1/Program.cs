using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.IO;

namespace CodeGenerationTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new ServiceModelRuntimeGenerator("Hello").TransformText();
            var c2 = new ServiceClassRuntimeGenerator("Hello").TransformText();
            var c3 = new AppHostRuntimeGenerator("Hello").TransformText();
            var c4 = new SelfHostRuntimeGenerator("Hello").TransformText();
            Console.Write(c1);
            Console.Write(c2);
            Console.Write(c3);
            Console.Write(c4);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("ServiceStack.dll");
            parameters.ReferencedAssemblies.Add("ServiceStack.Client.dll");
            parameters.ReferencedAssemblies.Add("ServiceStack.Common.dll");
            parameters.ReferencedAssemblies.Add("ServiceStack.Interfaces.dll");
            parameters.ReferencedAssemblies.Add("ServiceStack.Text.dll");            


            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;

            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = true;

            var installDir = Path.Combine(Environment.CurrentDirectory,"TestPublish");
            Directory.CreateDirectory(installDir);

            parameters.OutputAssembly = Path.Combine(installDir, "CodeGeneratedService.exe");
            
            var filesToCopy = new[] { "ServiceStack.dll", "ServiceStack.Client.dll", "ServiceStack.Common.dll", "ServiceStack.Interfaces.dll", "ServiceStack.Text.dll" };

            foreach (var file in filesToCopy)
            {
                var serviceStackDllLoc = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\" + file;
                var destinationLoc = installDir + @"\" + file;
                File.Copy(serviceStackDllLoc, destinationLoc, true);
            }

            CompilerResults results = provider.CompileAssemblyFromSource(
                parameters,
                c1,
                c2,
                c3,
                c4);

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }

            Console.WriteLine(results.PathToAssembly);
            Console.WriteLine("An attempt to save the exe was made.");

            Console.ReadLine();
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
    partial class AppHostRuntimeGenerator
    {

        string testinput;
        public AppHostRuntimeGenerator(string input)
        {
            testinput = input;
        }

    }

    partial class SelfHostRuntimeGenerator
    {

        string testinput;
        public SelfHostRuntimeGenerator(string input)
        {
            testinput = input;
        }

    }
}
