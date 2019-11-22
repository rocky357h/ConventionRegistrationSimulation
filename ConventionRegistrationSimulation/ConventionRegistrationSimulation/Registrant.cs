//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: Registrant.cs
//  Description: Contains information for each Registrant for the Simulation.
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
    /// Contains information for each Registrant for the Simulation.
    /// </summary>
    class Registrant
    {
        public int ID { get; set; }
        public TimeSpan Arrival { get; set; }
        public TimeSpan Departure { get; set; }
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Registrant"/> class.
        /// </summary>
        public Registrant()
        {
            ID = -1;
            Arrival = new TimeSpan(0, 0, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Registrant"/> class.
        /// </summary>
        /// <param name="id">The identification number.</param>
        /// <param name="arrival">The arrival time.</param>
        /// <param name="departure">The departure time.</param>
        public Registrant(int id, TimeSpan arrival)
        {
            ID = id;
            Arrival = arrival;
        }
    }
}
