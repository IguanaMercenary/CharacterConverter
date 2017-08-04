using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace CharacterConverter {
    /// <summary>
    /// Apparently any byte greater than hexadecimal 0x7F is decoded as the Unicode question mark ("?").
    /// </summary>
    class Program {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // Read a line of input
            string input = Console.ReadLine();
            string ls;
            StringBuilder sb = new StringBuilder();
            XORtable xor = new XORtable();
            Random rand = new Random();
            int[] asciiNumArray = new int[input.Length]; // Array of ints from characters converted to ASCII
            int[] randNumArray = new int[input.Length]; // Array of random numbers
            int counter = 0; // Counter for what position the number column is at
            
            /*
             * Loops through each char in input and converts it to an int and 
             * appends it to a StringBuilder
             */
            foreach (char ch in input) {
                int ascii = ch;
                sb.Append(Convert.ToInt32(ch) + ", ");
                asciiNumArray[counter] = ascii;
                counter++;
            }

            /*
             * Adds a random number to the random number array.
             */
            for (int i = 0; i < randNumArray.Length; i++) {
                randNumArray[i] = rand.Next(0, 255);
                Console.WriteLine("Random Number: " + randNumArray[i]);
            }

            ls = Convert.ToString(sb); // Puts the stringBuilder into a string
            ls = ls.Remove(ls.Length - 2); // Remove the last ','

            Console.Write(ls);
            Console.WriteLine();

            int[] XORArray = new int[input.Length]; // Array of ints from two other arrays XOR'd together
            for (int i = 0; i < XORArray.Length; i++) {
                XORArray[i] = xor.bitXOR(asciiNumArray[i], randNumArray[i]);
                Console.WriteLine(String.Format("{0,-5} {1, -5} {2,-5} Result: {3,6:X}", asciiNumArray[i], "^", randNumArray[i], XORArray[i]));    
            }

            for (int i = 0; i < XORArray.Length; i++) {
                char c = Convert.ToChar(XORArray[i]);
                Console.Write(c);
            }

            KeyGenerator key = new KeyGenerator();
            key.storeKeyMatrix();
            key.keySchedule();
            Console.ReadLine();
        }
    }
}