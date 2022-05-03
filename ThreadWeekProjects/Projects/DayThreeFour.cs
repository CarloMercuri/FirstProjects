using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadWeekProjects.Projects
{
    public class DayThreeFour
    {



     

        // Variables

        // op 1

        private object firstLock = new object();
        private int sharedNumber = 0;

        // op 2

        private object writerLock = new object();
        private int writeCount = 0;
        
        private string hashOutput = ""; // filled later
        private string starOutput = ""; // filled later

        private bool writing = true;
        

        // op 3

        // 
        // Opgave 1
        //

        public void IncreaseDecrease()
        {
            Thread increase = new Thread(new ThreadStart(IncreaseThread));
            Thread decrease = new Thread(new ThreadStart(DecreaseThread));

            increase.Start();
            decrease.Start();


        }

        private void IncreaseThread()
        {
            while (true)
            {
                Monitor.Enter(firstLock);

                sharedNumber += 2;

                Monitor.Exit(firstLock);

                Console.WriteLine($"Value incremented by IncreaseThread. New value: {sharedNumber}");

                Thread.Sleep(1000);
            }
        }

        private void DecreaseThread()
        {
            while (true)
            {
                Monitor.Enter(firstLock);

                sharedNumber -= 1;

                Monitor.Exit(firstLock);

                Console.WriteLine($"Value decreased by DecreaseThread. New value: {sharedNumber}");

                Thread.Sleep(1000);
            }
        }

        // 
        // Opgave 2
        //

        public void StarHash()
        {
            Console.WriteLine("Press any key to terminate.");
            writing = true;

            for (int i = 0; i < 60; i++)
            {
                hashOutput += '#';
                starOutput += '*';
            }

            Thread hashThread = new Thread(new ThreadStart(WriteHash));
            Thread starThread = new Thread(new ThreadStart(WriteStar));

            hashThread.Start();
            starThread.Start();

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Terminating...");

            writing = false;

            hashThread.Join();
            starThread.Join();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("StarHash terminated.");
        }

        private void WriteHash()
        {
            while(writing)
            {
                Monitor.Enter(writerLock);


                writeCount += 60;
                Console.WriteLine(hashOutput + $"    Count: {writeCount}");

                Monitor.Exit(writerLock);

                Thread.Sleep(1000);
            }
        }

        private void WriteStar()
        {
            while (writing)
            {
                Monitor.Enter(writerLock);


                writeCount += 60;
                Console.WriteLine(starOutput + $"    Count: {writeCount}");

                Monitor.Exit(writerLock);

                Thread.Sleep(1000);
            }
        }
    }
}
