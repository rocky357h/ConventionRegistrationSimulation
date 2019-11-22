//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: IContainer.cs
//  Description: Contains the IContainer Interface.
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
    /// Interface for a Container.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainer<T>
    {
        // Remove all objects from the container.
        void Clear();
        // Returns true if container is empty.
        bool IsEmpty();
        // Returns the number of entries in the container.
        int Count { get; set; }
    }
}
