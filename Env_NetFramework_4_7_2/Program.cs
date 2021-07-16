using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Env_NetFramework_4_7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the Target Framework
            string envName = ((TargetFrameworkAttribute)Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false)
                .SingleOrDefault()).FrameworkName;

            FPE.Environment = envName;

            FPE.Run();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
