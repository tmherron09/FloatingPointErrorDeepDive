using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Env_Net_5_0
{
    /// <summary>
    /// A static class Containing the various examples where I tested if it would have a Floating Point Rounding Error.
    /// </summary>
    public static class FPE
    {
        public static string Environment = "Environment Not Set";

        static float startX = -2.304f;
        static float xOffset = .768f;
        static int x = 3;

        public static void Run()
        {
            Console.WriteLine($"Current Environment: {Environment}\n");

            Intro();
            VariableStoredValues();
            HardCoded();
            StoredInNewVariables();
            SplitAssignmentVariables();
            SplitAssignmentHardCoded();

        }
        static void Intro()
        {
            
            Console.WriteLine("Comparing runtime enviroments: floating point rounding error.");
            Console.WriteLine("**Reminder** Just a footnote for myself. While it is good to know floating point rules, this example is mostly being repo-ed so I remember to reverse engineer the compiled code to trace back how the different precisions or being written or computed. Another unnecessary but fun side project with learning assembly and reverse-engineering for later.");
            EndSection();


            Console.WriteLine("Until I can dig deeper, two possible factors (or combination of both) I would attribute this discrepancy.");
            Console.WriteLine("1.) The c# framework. Unity uses Mono, a .Net Implementation, opposed to the native .Net framework.");
            Console.WriteLine("2.) x86 vs x64 Depending on the build/runtime environment (chipset could also factor in possibly in weird scenarios outside of this, maybe) the generated code's precision can vary.");
            Console.WriteLine("\nThe result we are looking for below is 0. Thus anything that is not 0, had a rounding error.");
            Console.WriteLine("**Note the original environment was Unity. Comparing on various online C# Repl again gave different results. This solution will include several projects targeting different versions of .Net");
            EndSection();

            Console.WriteLine("Original Scenario, while setting a floating point value of a Vector3 that would make up a grid. Certain values, 7x7, in this case, would not calculate the center tile of the grid at 0,0 while all other tiles had correct positions. This offset was a HUGE 1.192093E-07, basically 0 but not. Ultimately this error had no discernable effect on the actual product as it could not be visible and wouldn't cause compounded rounding errors later on.\n--Values--");
            Console.WriteLine("Start x: -2.304\nOffset of x: 0.768\nx value in a 7x7 grid: 3");
            EndSection();
        }
        static void EndSection()
        {
            int width = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(new string('-', width));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\n");
        }
        static void VariableStoredValues()
        {
            Console.WriteLine("In this example, when run under Unity, the result was incorrect. The value 1.192093E-07 was returned. Result was calculated from variables in a single assignment.\n");

            float result = startX + (xOffset * x);
            DisplayResult(result);
            EndSection();

        }
        static void HardCoded()
        {
            Console.WriteLine("In this example, I plugged the values directly into the assignment. This appeared correct in Unity. However, results varied with online c# Repl, giving the incorrect rounding Unity had in the other examples.");

            float result = -2.304f + (.768f * 3);
            DisplayResult(result);
            EndSection();
        }
        static void StoredInNewVariables()
        {
            Console.WriteLine("In this example, I was testing if the single assignment might have caused casting issues, or that I was pulling the values off a struct or just let's test. Doubtful but it was another scenario to test. All the variables were saved into new variables. This had effect.");

            float newStartX = startX;
            float newXOffset = xOffset;
            int newX = x;
            float result = newStartX + (newXOffset * newX);
            DisplayResult(result);
            EndSection();
        }
        static void SplitAssignmentVariables()
        {
            Console.WriteLine("In this example, the assignment is split. Multiplying the offset by x was done seperate from adding it to the starting x. This appeared to solve the rounding issue. More information would be needed to ensure it wouldn't encounter a rounding issue at some point for the larger use case. (1/2) Using Variables *variables were originally references from other objects.");

            float partOne = xOffset * x;
            float result = startX + partOne;
            DisplayResult(result);
            EndSection();
        }
        static void SplitAssignmentHardCoded()
        {
            Console.WriteLine("In this example, the assignment is split. Multiplying the offset by x was done seperate from adding it to the starting x. This appeared to solve the rounding issue. More information would be needed to ensure it wouldn't encounter a rounding issue at some point for the larger use case. (1/2) Using hard coded values.");

            float partOne = .768f * 3;
            float result = -2.304f + partOne;
            DisplayResult(result);
            EndSection();
        }
        static void DisplayResult(float result)
        {
            // Three tests incase evaluating float == 0 has errors. 
            // Could cause other errors, come back to this in debug.
            if (result != 0 || result > 0 || result < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This environment was unable to correctly round: {result}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"This environment correctly rounded: {result}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
