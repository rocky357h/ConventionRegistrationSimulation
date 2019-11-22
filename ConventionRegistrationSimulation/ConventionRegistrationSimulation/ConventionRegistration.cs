//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: ConventionRegistration.cs
//  Description: Manages the Simulation process for the driver.
//  Course: CSCI 2210-001 - Data Structures
//  Author: Aaron Henderson, hendersonaw@etsu.edu
//  Created: Saturday, November 17, 2019
//  Copyright: Aaron Henderson, 2019
//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Convention_Registration_Simulation;
using System.Threading;
using ConventionRegistrationSimulation;

namespace Convention_Registration_Simulation
{
    /// <summary>
    /// Manages the Simulation process for the driver.
    /// </summary>
    public class ConventionRegistration
    {
        public static int NumRegistrants = SimulationDriver.NumRegistrants;
        public static double NumHoursOpen = SimulationDriver.NumHoursOpen;
        public static int NumWindows = SimulationDriver.NumWindows;
        public static double ExpectedDuration = SimulationDriver.ExpectedDuration;

        private static Random r = new Random(); // Uniform random number generator.
        private static PriorityQueue<Event> PQ
            = new PriorityQueue<Event>();       // Priority Queue of Events.
        private static DateTime timeWeOpen
            = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.Today.Day, 10, 0, 0);  // Time at which we open today.
        public static int maxPresent { get; set; }    // The largest number present during the simulation.
        public static int arrivals { get; set; }       // Count of arrival events processed.
        public static int departures { get; set; }     // Count of departure events processed.
        private static TimeSpan shortest,       // The shortest stay.
                                longest,        // The longest stay.
                                totalTime;      // The sum of all stays for use in finding the average stay.
        static TimeSpan start;
        static TimeSpan interval;      // Length of time in line.
        private static List<Queue<Registrant>> ListOfRegistrationLines = new List<Queue<Registrant>>(NumWindows);

        /// <summary>
        /// Generates, runs, and shows the statistics of the simulation.
        /// </summary>
        public static void Run()
        {
            GenerateQueues();
            GenerateRegistrantEvents();
            DoSimulation();
            ShowStatistics();
        }

        /// <summary>
        /// Draws the lines for the Registration Windows.
        /// </summary>
        public static void DrawLines()
        {
            List<List<Registrant>> ListOfQueues = new List<List<Registrant>>();
            ClearDisplayOfQueues(6, LengthOfCurrentLongestLine());
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(0, 6);
            for(int i=0; i < NumWindows; i++)
            {
                ListOfQueues.Add(new List<Registrant>(ListOfRegistrationLines[i].ToArray()));
            }

            for(int i=0; i < ListOfQueues.Count; i++)
            {
                while (ListOfQueues[i].Count < LengthOfCurrentLongestLine())
                {
                    ListOfQueues[i].Add(new Registrant());
                }
            }

            for(int i=0; i < LengthOfCurrentLongestLine(); i++)
            {
                Console.Write("\t");
                for(int j=0; j < NumWindows; j++)
                {
                    if(ListOfQueues[j][i].ID != -1)
                    {
                        Console.Write("\t" + ListOfQueues[j][i].ID.ToString("D4"));
                    }
                    else
                    {
                        Console.Write("\t    ");
                    }
                }
                Console.WriteLine();
            }

            DrawStatistics();
        }

        /// <summary>
        /// Draws the Statistics for the Simulation.
        /// </summary>
        private static void DrawStatistics()
        {
            // Draw "So far:" bar
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSo far:\t-------------------------------------------------------------");

            // Draw Current Statistics
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Longest Queue Encountered So Far:\t{maxPresent}");
            Console.Write($"Events Processed So Far: {arrivals + departures}");
            Console.Write($"\tArrivals: {arrivals}");
            Console.WriteLine($"\tDepartures: {departures}");
            Console.WriteLine();
        }

        /// <summary>
        /// Draws the Registration Queues.
        /// </summary>
        private static void DrawRegistrationWindows()
        {
            Console.Write("\t");
            for (int i = 0; i < ListOfRegistrationLines.Count; i++)
            {
                Console.Write($"\t W {i}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Draws the Title for the Registration Window.
        /// </summary>
        private static void DrawHeading()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t\tRegistration Windows");
            Console.WriteLine("\t\t\t\t\t\t--------------------");
            Console.WriteLine();
            DrawRegistrationWindows();
        }

        /// <summary>
        /// Clears the Display Window of the Registrant Queues.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void ClearDisplayOfQueues(int start, int end)
        {
            Console.SetCursorPosition(start, 0);
            for(int i=0; i < end; i++)
            {
                Console.SetCursorPosition(0, start + 1);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        /// <summary>
        /// Determines the Current Longest Queue in the ListOfRegistraionLines.
        /// </summary>
        /// <returns></returns>
        private static int LengthOfCurrentLongestLine()
        {
            int longest = 0;
            for (int i = 0; i < ListOfRegistrationLines.Count; i++)
            {
                if (ListOfRegistrationLines[i].Count > longest)
                    longest = ListOfRegistrationLines[i].Count;
            }

            return longest;
        }

        /// <summary>
        /// Generates the registrant events.
        /// </summary>
        public static void GenerateRegistrantEvents()
        {
            shortest = new TimeSpan(0, 100000, 0);  // Shortest stay.
            longest = new TimeSpan(0, 0, 0);        // Longest stay.
            totalTime = new TimeSpan(0, 0, 0);      // Total of all stays for finding average.

            NumRegistrants = Poisson(NumRegistrants);

            for (int registrantID = 1; registrantID <= NumRegistrants; registrantID++)
            {
                Registrant person = new Registrant(registrantID, new TimeSpan(0, 0, (int)start.TotalSeconds));

                // Random start time based on the number of minutes in the hours we are open.
                start = new TimeSpan(0, 0, r.Next((int)(NumHoursOpen * 60 * 60)));

                /* Random (neg. exp.) interval with a minimum of 1.5 minutes; expected time = 4.5 minutes
                interval = new TimeSpan(0, 0, (int)((1.5 + NegExp(3.0)) * 60));
                totalTime += interval;

                if (shortest > interval)     // Remember the shortest stay
                    shortest = interval;

                if (longest < interval)       // Remember the longest stay
                    longest = interval;
                */

                // Enqueue the arrival event for this person.
                PQ.Enqueue(new Event(EVENTTYPE.ENTER, timeWeOpen.Add(start), person));

                /* Enqueue the departure event for this person.
                person.Departure = new TimeSpan(0, 0, (int) (start.TotalSeconds + interval.TotalSeconds));
                PQ.Enqueue(new Event(EVENTTYPE.LEAVE, timeWeOpen.Add(start + interval), person));
                */
            }
        }

        /// <summary>
        /// Calculates the average time registrants spend in line.
        /// </summary>
        /// <returns></returns>
        public static void DisplayAvgTime()
        {
            // Calculate and display the average time registrants spend in line
            int seconds = (int)(totalTime.TotalSeconds / NumRegistrants);   // Average for all registrants.
            TimeSpan avgTime = new TimeSpan(0, 0, seconds);

            Console.WriteLine($"The average time registrants spent in line was {avgTime}");
        }

        /// <summary>
        /// Does the simulation.
        /// </summary>
        public static void DoSimulation()
        {
            maxPresent = 0;
            Queue<Registrant> shortestQueue;

            DrawHeading();

            while (PQ.Count > 0)        // While there are more events...
            {
                DrawLines();        // Draw Registration Windows.

                if (PQ.Peek().Type == EVENTTYPE.ENTER)   // If top event is Arrival...
                {
                    shortestQueue = CurrentShortestLine();
                    if(shortestQueue.Count == 0)
                    {
                        double interval = (1.5 * 60) + NegExp(ExpectedDuration * 60) - (1.5 * 60);  // 1.5 = minimum checkout time.
                        PQ.Peek().registrant.Interval = TimeSpan.FromSeconds(interval);
                        PQ.Peek().registrant.Departure = PQ.Peek().registrant.Arrival + TimeSpan.FromSeconds(interval);

                    }
                    shortestQueue.Enqueue(PQ.Peek().registrant);
                    PQ.Dequeue();
                    arrivals++;
                    if (LengthOfCurrentLongestLine() > maxPresent)
                    {
                        maxPresent = LengthOfCurrentLongestLine();
                    }
                }
                else
                {
                    for(int i=0; i < NumWindows; i++)
                    {
                        if (ListOfRegistrationLines[i].Count > 0 && PQ.Peek().registrant.ID == ListOfRegistrationLines[i].Peek().ID)
                        {
                            TimeSpan previousRegistrant = ListOfRegistrationLines[i].Peek().Departure;
                            ListOfRegistrationLines[i].Dequeue();
                            departures++;

                            if(ListOfRegistrationLines[i].Count > 0)
                            {
                                double interval = (1.5 * 60) + NegExp(ExpectedDuration * 60) - (1.5 * 60);  // 1.5 = minimum checkout time.
                                PQ.Peek().registrant.Interval = TimeSpan.FromSeconds(interval);
                                PQ.Peek().registrant.Departure = PQ.Peek().registrant.Arrival + TimeSpan.FromSeconds(interval);

                                if (ListOfRegistrationLines[i].Peek().Interval < shortest || shortest == new TimeSpan(0, 0, 0))
                                {
                                    shortest = ListOfRegistrationLines[i].Peek().Interval;
                                }
                                if(ListOfRegistrationLines[i].Peek().Interval > longest)
                                {
                                    longest = ListOfRegistrationLines[i].Peek().Interval;
                                }

                                totalTime = totalTime.Add(ListOfRegistrationLines[i].Peek().Interval);
                                ListOfRegistrationLines[i].Peek().Departure = ListOfRegistrationLines[i].Peek().Interval + previousRegistrant;
                                PQ.Enqueue(new Event(EVENTTYPE.LEAVE, timeWeOpen.Add(ListOfRegistrationLines[i].Peek().Departure), ListOfRegistrationLines[i].Peek()));
                            }
                        }
                    }
                    PQ.Dequeue();       // Remove top event.
                }

                Thread.Sleep(100);  // Pause for a moment.
            }
        }

        /// <summary>
        /// Shows the statistics for a completed Simulation.
        /// </summary>
        public static void ShowStatistics()
        {
            DisplayAvgTime();
            DisplayMinMaxTimes();
        }

        /// <summary>
        /// Displays the minimum and maximum registration times.
        /// </summary>
        public static void DisplayMinMaxTimes()
        {
            Console.WriteLine($"The minimum registration time was {shortest} and the maximum was {longest}.");
        }

        /// <summary>
        /// Displays the maximum registrants present at one time.
        /// </summary>
        public static void DisplayMaxPresent()
        {
            Console.WriteLine($"The maximum number in line to register at any time was {maxPresent}.");
        }

        /// <summary>
        /// Generates the Queue lines for Registration.
        /// </summary>
        private static void GenerateQueues()
        {
            for (int i = 0; i < ListOfRegistrationLines.Capacity; i++)
                ListOfRegistrationLines.Add(new Queue<Registrant>());
        }

        /// <summary>
        /// Returns the Current Shortest Queue in ListOfRegistrationLines.
        /// </summary>
        /// <returns></returns>
        private static Queue<Registrant> CurrentShortestLine()
        {
            int shortest = 0;

            for(int i=1; i < ListOfRegistrationLines.Count; i++)
            {
                if (ListOfRegistrationLines[i].Count < ListOfRegistrationLines[shortest].Count)
                    shortest = i;
            }

            return ListOfRegistrationLines[shortest];
        }

        /// <summary>
        /// Returns the Negative Exponential value of Expected Value.
        /// </summary>
        /// <param name="ExpectedValue">The expected value.</param>
        /// <returns></returns>
        private static double NegExp(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(r.NextDouble(), Math.E);
        }

        /// <summary>
        /// Returns the Poisson value of Expected Value.
        /// </summary>
        /// <param name="ExpectedValue"></param>
        /// <returns></returns>
        private static int Poisson (double ExpectedValue)
        {
            double dLimit = -ExpectedValue;
            double dSum = Math.Log(r.NextDouble());

            int Count;
            for (Count = 0; dSum > dLimit; Count++)
                dSum += Math.Log(r.NextDouble());

            return Count;
        }

        /// <summary>
        /// Updates the values for the Registration Windows graphic.
        /// </summary>
        private static void UpdateValues()
        {
            Console.WriteLine($"Longest Queue Encountered So Far:\t{maxPresent}");

        }
    }
}
