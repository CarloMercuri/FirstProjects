using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadWeekProjects.Projects
{
    public class DayOneExercises
    {
        // Opgave 0 

        private char ch = '*';

        public void ZeroEx()
        {
            Thread threadOne = new Thread(new ThreadStart(WorkThreadFunction));
            threadOne.Name = "First Test Thread";

            Thread threadTwo = new Thread(new ThreadStart(WorkThreadFunction));
            threadTwo.Name = "Second Test Thread";

            threadOne.Start();
            threadTwo.Start();
        }

        private void WorkThreadFunction()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Current thread name: {Thread.CurrentThread.Name}, iteration {i}");
            }
        }

        // Opgave 1 - 2

        public void FirstSecondEx()
        {
            Thread thread1 = new Thread(new ThreadStart(ThreadExecuteNormal));    
            Thread thread2 = new Thread(new ThreadStart(ThreadExecuteAdvanced));
            thread1.Start();
            thread2.Start();
        }

        private void ThreadExecuteNormal()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"C#-trådning er nemt!");
                Thread.Sleep(1000);
            }
        }

        private void ThreadExecuteAdvanced()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Også med flere tråde …");
                Thread.Sleep(1000);
            }
        }

        // It's fine to use thread.sleep in this case, as it does exactly what you want it to do.
        // Issues with Thread.Sleep is that first of all, it locks the whole thread, so it might not be
        // what you want. Also, if absolute precision is your goal, Thread.sleep is not guaranteed to 
        // resume processing EXACTLY after the specified milliseconds, as it comes down to when the OS wakes it up



        // Opgave 3

        public void TemperatureAlarm()
        {
            Thread alarmThread = new Thread(new ThreadStart(GenerateTemperatures));
            alarmThread.Start();

            while (alarmThread.IsAlive)
            {
                Console.WriteLine("MAIN THREAD: Alarm thread still running!");
                Thread.Sleep(10000);                
            }

            Console.WriteLine("MAIN THREAD: Alarm thread terminated!");
        }

        public void GenerateTemperatures()
        {
            int alarmsCount = 0;

            Random rand = new Random();

            while(alarmsCount < 3)
            {
                Thread.Sleep(2000);

                int temperature = rand.Next(-20, 101);

                Console.WriteLine($"Temperature generated: {temperature}. {(temperature < 0 ? "ALARM!!!" : "")}");

                if(temperature < 0)
                {
                    alarmsCount++;
                }                
            }

            Console.WriteLine("Thread stopping automatically.");
        }

        //
        // Opgave 4
        //

        public void InputOutput()
        {
            Console.WriteLine("Input S to stop.");
            Console.WriteLine();
            Console.WriteLine();

            char userInput = '*';
            Thread printThread = new Thread(new ThreadStart(PrintScreenThread));
            printThread.Start();

            while(userInput != 's')
            {
                string inputString = Console.ReadLine();                

                // Regardless of the input lenght, it's always gonna take the first char
                userInput = inputString[0];

                ch = userInput;
            }

            Console.WriteLine("Char S input. The end.");

        }

        private void PrintScreenThread()
        {
            while(ch != 's')
            {
                Console.Write(ch);
                Thread.Sleep(50);
            }
            
        }
    }
}
