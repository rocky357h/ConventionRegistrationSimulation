//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: IPriorityQueue.cs
//  Description: Contains the IPriorityQueue Interface.
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
    /// Interface for a PriorityQueue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Convention_Registration_Simulation.PriorityQueue.IContainer{T}" />
    public interface IPriorityQueue<T> : IContainer<T>
                            where T : IComparable
    {
        /// <summary>
        /// Inserts item based on its priority.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        void Enqueue(T item);

        /// <summary>
        /// Removes first item in the queue.
        /// </summary>
        void Dequeue();

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        T Peek();
    }
}
