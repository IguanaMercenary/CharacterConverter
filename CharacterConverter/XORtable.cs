using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterConverter {
    /// <summary>
    /// A class that exists to perform bitwise XOR on two numbers.
    /// Don't feel like re-writing the function everytime it's needed.
    /// Author: M.O
    /// Date: 2017/08/03
    /// </summary>
    public class XORtable {

        /// <summary>
        /// Performs bitwise XOR on the two inputed numbers
        /// Author: M.O
        /// Date: 2017/08/03
        /// </summary>
        /// <param name="x">The first number to be XOR'd</param>
        /// <param name="y">The second number to be XOR'd</param>
        /// <returns>The result of the XOR</returns>
        public int bitXOR(int x, int y) {
            int z = x ^ y;
            return z;
        }
    }
}
