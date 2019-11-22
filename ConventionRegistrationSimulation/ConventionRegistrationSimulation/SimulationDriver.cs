//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: Simulation.cs
//  Description: Contains and manages the Simulation.
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
using MenuClassDemo;
using UtilityNamespace;
using Utils;
using Convention_Registration_Simulation;

namespace ConventionRegistrationSimulation
{
    /// <summary>
    /// Contains and manages the Simulation.
    /// </summary>
    class SimulationDriver
    {
        // Initial Simulation Values
        public static int NumRegistrants = 1000;     // Number of Registrants.
        public static int NumHoursOpen = 10;
        public static int NumWindows = 6;
        public static double ExpectedDuration = 4.5;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            
            // Initialize Simulation Menu
            Menu menu = new Menu("Simulation Menu");
            menu = menu + "Set the number of Registrants";
            menu = menu + "Set the number of hours of operation";
            menu = menu + "Set the number of windows";
            menu = menu + "Set the expected checkout duration";
            menu = menu + "Run the simulation";
            menu = menu + "End the program";

            Choices choice = (Choices) menu.GetChoice();
            while (choice != Choices.QUIT)
            {
                switch (choice)
                {
                    case Choices.REGISTRANTS:
                        Console.Write("How many Registrants are expected to be served in a day? ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        if(Int32.TryParse(Console.ReadLine(), out int people))
                        {
                            NumRegistrants = people;
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid number of Registrants!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Utility.PressAnyKey();
                        }
                        break;

                    case Choices.HOURS:
                        Console.Write("How many hours will registration be open? ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        if(Int32.TryParse(Console.ReadLine(), out int hours))
                        {
                            NumHoursOpen = hours;
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid number of Hours!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Utility.PressAnyKey();
                        }
                        break;

                    case Choices.WINDOWS:
                        Console.Write("How many registration lines are to be simulated? ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (Int32.TryParse(Console.ReadLine(), out int windows))
                        {
                            NumWindows = windows;
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid number of Windows!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Utility.PressAnyKey();
                        }
                        break;

                    case Choices.CHECKOUT:
                        Console.WriteLine("What is the expected service time for a Registrant in minutes? ");
                        Console.WriteLine("Example: Enter 5.5 for 5 and a half minutes (5 minutes, 30 seconds).");
                        if (Double.TryParse(Console.ReadLine(), out double time))
                        {
                            ExpectedDuration = time;
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid expected duration!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Utility.PressAnyKey();
                        }
                        break;

                    case Choices.RUN:
                        if(AnySettingNotInitialized())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Settings not Initialized Correctly!");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Utility.PressAnyKey();
                            break;
                        }

                        ConventionRegistration.Run();

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Utility.PressAnyKey();
                        break;

                } // end of switch

                choice = (Choices) menu.GetChoice();
            } // end of while
        }

        /// <summary>
        /// Determines if any Simulation setting is not initialized.
        /// </summary>
        /// <returns></returns>
        public static bool AnySettingNotInitialized()
        {
            if (NumRegistrants <= 0 || NumHoursOpen <= 0 || NumWindows <= 0 || ExpectedDuration <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
