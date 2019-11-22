//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 4 - Convention Registration Simulation
//  File Name: Utility.cs
//  Description: Contains methods intended to be used for multiple projects.
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

namespace Utils
{
    /// <summary>
    /// Static class containing methods for multiple projects.
    /// </summary>
    static class Utility
    {
        #region Methods
        /// <summary>
        /// Converts "\r\n" to a newline character and returns the result.
        /// </summary>
        /// <param name="work">The string to be trimmed.</param>
        /// <returns></returns>
        public static string Clean(ref string work)
        {
            work = work.Trim(" \t".ToCharArray());
            work = work.Replace(Environment.NewLine, "\n");
            return work;
        }

        /// <summary>
        /// Clears the console screen by skipping 40 lines.
        /// </summary>
        public static void ClearScreen()
        {
            Skip(40);
        }

        /// <summary>
        /// Formats the text.
        /// </summary>
        /// <param name="txt">The text to be formatted.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <param name="rightMargin">The right margin.</param>
        /// <returns></returns>
        public static string FormatText(string txt, int leftMargin = 0, int rightMargin = 80)
        {
            List<string> ListOfTokens = Tokenize(txt, " ");
            string str = String.Empty;
            int LengthOfRow = 0;
            for (int i = 0; i < ListOfTokens.Count; i++)
            {
                if (LengthOfRow + ListOfTokens[i].Length < rightMargin)
                {
                    if (ListOfTokens[i] == "." || ListOfTokens[i] == "?" || ListOfTokens[i] == "," || ListOfTokens[i] == "!")
                    {
                        str += $"{ListOfTokens[i]}";
                        LengthOfRow += ListOfTokens[i].Length;
                    }
                    else
                    {
                        str += $" {ListOfTokens[i]}";
                        LengthOfRow += ListOfTokens[i].Length + 1;
                    }
                }
                else if (ListOfTokens[i] == "." || ListOfTokens[i] == "?" || ListOfTokens[i] == "," || ListOfTokens[i] == "!")
                {
                    str += $"{ListOfTokens[i]}\n";
                    LengthOfRow = 0;
                }
                else
                {
                    str += $"\n{ListOfTokens[i]}";
                    LengthOfRow = ListOfTokens[i].Length;
                }
            }
            return str;
        }

        /// <summary>
        /// Tells the user goodbye at the end of the program. 
        /// </summary>
        /// <param name="msg">The goodbye message.</param>
        /// <returns></returns>
        public static void GoodbyeMessage(string msg = "Goodbye and thank you for using this program.")
        {
            Console.WriteLine(msg);
            PressAnyKey();
        }

        /// <summary>
        /// Determines the maximum length of txt. 
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <returns></returns>
        public static int MaxLength(List<string> txt)
        {
            return txt.Capacity;
        }

        /// <summary>
        /// Waits for user to press enter before continuing the program.
        /// </summary>
        /// <param name="strVerb">The string displayed to the user.</param>
        public static void PressAnyKey(string strVerb = "Continue...")
        {
            Console.WriteLine(strVerb);
            Console.ReadLine();
        }

        /// <summary>
        /// Skips n number of lines in the console. 
        /// </summary>
        /// <param name="n">The number of lines to skip.</param>
        public static void Skip(int n = 1)
        {
            for (int i = 0; i < n; i++)
                Console.Write("\n");
        }

        /// <summary>
        /// Tokenizes the specified original string.
        /// </summary>
        /// <param name="original">The original string.</param>
        /// <param name="delimeters">The delimeters string.</param>
        /// <returns></returns>
        public static List<string> Tokenize(string original, string delimeters)
        {
            List<string> tokens = new List<string>();
            string work = original;
            work = Clean(ref work);

            int col;
            string token;

            while (!string.IsNullOrEmpty(work))
            {
                col = work.IndexOfAny(delimeters.ToCharArray());
                if (col == 0)
                    col = 1;
                if (col == -1)
                    col = work.Length;
                token = work.Substring(0, col);
                tokens.Add(token);
                work = work.Substring(col);
                work = work.Trim(" \t".ToCharArray());
            }
            return tokens;
        }

        /// <summary>
        /// Welcomes the user with a message.
        /// </summary>
        /// <param name="msg">The welcome message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="author">The author.</param>
        public static void WelcomeMessage(string msg, string caption = "Computer Science 2210-001", string author = "Aaron Henderson")
        {
            Console.WriteLine($"{caption} --- {author} \n{msg}");
        }
        #endregion
    }

}
