using System;
using ThreadWeekProjects.Projects;
using ThreadWeekProjects.Projects.Philosophers;

namespace ThreadWeekProjects
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\r/////////  First Day  ////////\n\r");

                Console.WriteLine("1 - Opgave 1 - 2");
                Console.WriteLine("2 - Opgave 3");
                Console.WriteLine("3 - Opgave 4");                
                Console.WriteLine();

                int choice = ConsoleTools.GetUserInputInteger("Make a selection:");

                RunAssignment(choice);

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Assignment ended. Press any key to return to the main menu.");

                Console.ReadKey();

            }

            DayThreeFour ex = new DayThreeFour();
            ex.StarHash();

            Console.ReadKey();
        }

        private static void RunAssignment(int selection)
        {
            Console.Clear();

            // De er i rækkefølge

            

            switch (selection)
            {
                              

                case 1: 
                    DayOneExercises d1_1 = new DayOneExercises();
                    d1_1.FirstSecondEx();
                    break;

                case 2: DayOneExercises d1_2 = new DayOneExercises();
                    d1_2.TemperatureAlarm();
                    break;

                case 3: DayOneExercises d1_3 = new DayOneExercises();
                    d1_3.InputOutput();
                    break;

            }
        }
    }
}
