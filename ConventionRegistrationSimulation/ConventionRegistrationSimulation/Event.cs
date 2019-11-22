//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: Event.cs
//  Description: Contains the type and all information for an Event for the
//               Simulation.
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

namespace Convention_Registration_Simulation
{
    /// <summary>
    /// Type of Event for a Registrant.
    /// </summary>
    enum EVENTTYPE { ENTER, LEAVE };

    /// <summary>
    /// Contains all the information for an Event for the Simulation.
    /// </summary>
    class Event : IComparable
    {
        public EVENTTYPE Type { get; set; }
        public DateTime Time { get; set; }
        public Registrant registrant { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        public Event()
        {
            Type = EVENTTYPE.ENTER;
            Time = DateTime.Now;
            registrant = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="time">The time of the event.</param>
        /// <param name="registrant">The registrant for the event.</param>
        public Event(EVENTTYPE type, DateTime time, Registrant person)
        {
            Type = type;
            Time = time;
            registrant = person;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CompareTo(Object obj)
        {
            if (!(obj is Event))
                throw new ArgumentException("The argument is not an Event object");

            Event e = (Event)obj;
            return e.Time.CompareTo(Time);
        }
    }
}
