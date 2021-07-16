using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace Env_NetCore_3_1
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
        }
    }
}
