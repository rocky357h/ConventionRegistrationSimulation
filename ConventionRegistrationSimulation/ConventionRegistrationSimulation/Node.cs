//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: Node.cs
//  Description: Contains the Node class.
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
    /// Contains information for a Node.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> where T : IComparable
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="link">The link.</param>
        public Node(T value, Node<T> link)
        {
            Item = value;
            Next = link;
        }
    }
}
