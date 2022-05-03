using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadWeekProjects.Projects.Philosophers
{
    public class Philosopher
    {

        public PhilosopherGUI gui;

        private int updateTimer = 100; // ms

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        public PhilosopherStatus Status { get; set; }

        private int eatingTime;

        public int EatingTime
        {
            get { return eatingTime; }
            set { eatingTime = value; }
        }

        private int eatingTimeRemaining;

        public int EatingTimeRemaining
        {
            get { return eatingTimeRemaining; }
            set { eatingTimeRemaining = value; }
        }

        private Fork fork1;

        public Fork Fork1
        {
            get { return fork1; }
            set { fork1 = value; }
        }

        private Fork fork2;

        public Fork Fork2
        {
            get { return fork2; }
            set { fork2 = value; }
        }

        private int timeToDie;

        public int TimeToDie
        {
            get { return timeToDie; }
            set { timeToDie = value; }
        }

        private int timeWithoutFood;

        public int TimeWithoutFood
        {
            get { return timeWithoutFood; }
            set { timeWithoutFood = value; }
        }

        private int meditationTime;

        public int MeditationTime
        {
            get { return meditationTime; }
            set { meditationTime = value; }
        }


        private int meditationTimeRemaining;

        public int MeditationTimeRemaining
        {
            get { return meditationTimeRemaining; }
            set { meditationTimeRemaining = value; }
        }


        public Philosopher(PhilosopherGUI _gui)
        {
            gui = _gui;
        }

        public void StartProcedure()
        {
            this.Status = PhilosopherStatus.Idle;
            Thread mainThread = new Thread(new ThreadStart(MainLoop));
            mainThread.Start();
        }

        private void MainLoop()
        {
            while (this.Status != PhilosopherStatus.Dead)
            {
                this.Status = PhilosopherStatus.Idle;

                if (Monitor.TryEnter(fork1.Lock, 10000) && Monitor.TryEnter(fork2.Lock, 10000))
                {
                    try
                    {
                        this.Status = PhilosopherStatus.Eating;
                        eatingTimeRemaining = eatingTime;
                        timeWithoutFood = 0;

                        // eat with updates every second
                        while (eatingTimeRemaining > 0)
                        {
                            Thread.Sleep(updateTimer);

                            eatingTimeRemaining -= updateTimer;
                        }

                        this.Status = PhilosopherStatus.Idle;
                        eatingTimeRemaining = 0;

                        Monitor.Pulse(fork1.Lock);
                        Monitor.Pulse(fork2.Lock);

                        Monitor.Exit(fork1.Lock);
                        Monitor.Exit(fork2.Lock);

                        Meditate();
                    }
                    catch (Exception ex)
                    {
                        gui.PrintException(ex.Message);
                        throw;
                    }
                    
                }
                else
                {
                    timeWithoutFood += 10;
                    Meditate();
                }
            }

            
        }
        private void Meditate()
        {
            meditationTimeRemaining = meditationTime;
            this.Status = PhilosopherStatus.Meditating;

            while (meditationTimeRemaining > 0)
            {
                if(timeWithoutFood > TimeToDie)
                {
                    this.Status = PhilosopherStatus.Dead;
                    meditationTimeRemaining = 0; // this will exit the loop, and then die.
                }

                Thread.Sleep(updateTimer);

                meditationTimeRemaining -= updateTimer;
                timeWithoutFood += updateTimer;
            }            
        }

    }
}
