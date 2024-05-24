using ReiCalcLib;

namespace ReiCalcConsole
{
    internal class Program
    {
        private static ReiCalc calculator;

        static void Main(string[] args)
        {
            calculator = new ReiCalc();

            Console.WriteLine("ReiCalc Console - enter \"exit\" to exit.");
            string input = "";
            while (true)
            {
                Console.WriteLine();
                Console.Write("Expression: ");
                input = Console.ReadLine();

                if (input == "exit") break;

                if (input == "")
                {
                    // Blank expression, use a templated one
                    input = "3 + 26 - -489420 * 4 + 1";
                    // Expected result: 1957710
                }

                double result = calculator.Calculate(input);
                Console.WriteLine($"Result: {result}");
            }
        }
    }
}
