//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: PriorityQueue.cs
//  Description: Contains the PriorityQueue (PQ) data structure.
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
    /// Contains the PriorityQueue data structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Convention_Registration_Simulation.IPriorityQueue{T}" />
    class PriorityQueue<T> : IPriorityQueue<T>
        where T : IComparable
    {
        private Node<T> top;
        public int Count { get; set; }

        /// <summary>
        /// Clears the PQ.
        /// </summary>
        public void Clear()
        {
            top = null;     // Nodes will be garbage collected.
            Count = 0;      // Count is 0 since PQ is empty.
        }

        /// <summary>
        /// Removes first item in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Cannot remove from empty queue.</exception>
        public void Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from empty queue.");
            else
            {
                Node<T> oldNode = top;
                top = top.Next;
                Count--;
                oldNode = null;     // Removed node can be garbage collected.
            }
        }

        /// <summary>
        /// Inserts item based on its priority.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        public void Enqueue(T item)
        {
            if (Count == 0)
            {
                top = new Node<T>(item, null);
            }
            else
            {
                Node<T> current = top;
                Node<T> previous = null;

                // Search for the first node in the linked list structure 
                // that is smaller in priorty than item.
                while(current != null && current.Item.CompareTo(item) >= 0)
                {
                    previous = current;
                    current = current.Next;
                }

                // Have found the place to insert the new node
                Node<T> newNode = new Node<T>(item, current);

                // If there is a previous node, set it to link to the new node
                if (previous != null)
                    previous.Next = newNode;
                else
                    top = newNode;
            }
            Count++;    // Add 1 to the number of nodes in PQ.
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Retrieve the top item in the PQ.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Cannot obtain top of empty priority queue.</exception>
        public T Peek()
        {
            if(!IsEmpty())
            {
                return top.Item;
            }
            else
            {
                throw new InvalidOperationException("Cannot obtain top of empty priority queue.");
            }
        }
    }
}
