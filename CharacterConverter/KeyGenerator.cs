using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CharacterConverter {
    /// <summary>
    /// Generates the cypher key for the AES and performs the key expansion.
    /// Author: M.O
    /// Date: 2017/08/03
    /// Updated by: M.O
    /// Date: 2017/08/04
    /// </summary>
    class KeyGenerator {
        private const int MATRIX_ROWS = 4;
        private const int MATRIX_COLUMNS = 4;

        Random rand = new Random();
        private int[,] cypherKey = new int[MATRIX_ROWS, MATRIX_COLUMNS];
        private int[,] roundKeyOne = new int[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyTwo = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyThree = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyFour = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyFive = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeySix = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeySeven = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyNine = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyEight = new byte[MATRIX_ROWS, MATRIX_COLUMNS];
        private byte[,] roundKeyTen = new byte[MATRIX_ROWS, MATRIX_COLUMNS];







        /// <summary>
        /// Generates random numbers and puts them in to the matrix.
        /// Author: M.O
        /// Date: 2017/08/03
        /// </summary>
        public void storeKeyMatrix() {
            for (int i = 0; i < MATRIX_ROWS; i++) {
                for (int j = 0; j < MATRIX_COLUMNS; j++) {
                    cypherKey[i, j] = rand.Next(0, 128);
                }
            }
        }

        /// <summary>
        /// Performs RotWord on the cipher key provided. 
        /// Returns the cipher key with the top block switched with the bottom one
        /// Author: M.O
        /// Date: 2017/08/04
        /// </summary>
        /// /// <param name="tempKeyMatrix"></param>
        public int[,] rotWord(int[,] tempKeyMatrix) {
            var temp = tempKeyMatrix.GetValue(0, 3);
            tempKeyMatrix[0, 3] = (int) tempKeyMatrix.GetValue(3, 3);
            tempKeyMatrix[3, 3] = (int) temp;
            return tempKeyMatrix;
        }

        /// <summary>
        /// Performs the round constant like in Rijndael -- "exponentiation of 2 to a user-specified value."
        /// Author: M.O
        /// Date: 2017/08/04
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public int RoundConstant(int temp, int round) {
            //The rcon. ?? 
            UInt16[] rcon = new UInt16[256]
    {
    0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a,
    0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39,
    0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a,
    0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8,
    0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef,
    0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc,
    0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b,
    0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3,
    0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94,
    0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
    0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35,
    0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f,
    0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04,
    0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63,
    0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd,
    0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d
    };
            XORtable xor = new XORtable();
            int avTemp = xor.bitXOR(temp, (int)rcon.GetValue(round));
            return avTemp;
        }

        /// <summary>
        /// Performs XOR on the column of the cypher key that has been rotated (all prior to Rcon), 
        /// with the initial cypher key's first column.
        /// </summary>
        public void roundOneRoundOneKeyColumn() {
            XORtable xor = new XORtable();
            for (int i = 0; i < MATRIX_ROWS; i++) {
                roundKeyOne[i,0] = xor.bitXOR(cypherKey[i, 0], roundKeyOne[i, 0]);
            }
        }

        /// <summary>
        /// Performs the sub-byte substitution - a non-linear substitution step where each byte is replaced 
        /// with another according to a lookup table.
        /// </summary>
        /// <param name="tempKeyMatrix"></param>
        public int subByteTransform(int switchNumber) {
            //Represents a Rijndael S-Box
            UInt16[] sbox = new UInt16[256]
 {
    0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
    0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
    0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
    0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
    0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
    0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
    0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
    0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
    0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
    0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
    0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
    0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
    0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
    0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
    0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
    0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16
 };
            // I want to go through the S-Box to the index specified by switchNumber and return the value at that index
            int temp;
            return temp = (int)sbox.GetValue(switchNumber);
        }

        /// <summary>
        /// Expands the generated keys 10 times to hide their initial value. 
        /// Should be recursive so that it calls itself 10 times and generates 10 different keys?
        /// </summary>
        public void keySchedule() {
            byte[,] roundConstant = new byte[MATRIX_ROWS, MATRIX_COLUMNS];

            for (int i = 0; i < MATRIX_ROWS; i++) {
                for (int j = 0; j < MATRIX_COLUMNS; j++) {
                    roundConstant[i, j] = (byte)rand.Next(0, 2);
                }
            }

            //swap the top byte of the last row of the last column 
            //with the bottom byte of the last row of the last column
            var temp = cypherKey.GetValue(0, 3);
            Console.WriteLine("[0, 3] = {0}", cypherKey.GetValue(0, 3)); //for testing purposes
            Console.WriteLine("[3, 3] = {0}", cypherKey.GetValue(3, 3));
            cypherKey[0, 3] = (byte)cypherKey.GetValue(3, 3);
            cypherKey[3, 3] = (byte)temp;

            Console.WriteLine("[0, 3] = {0}", cypherKey.GetValue(0, 3));
            Console.WriteLine("[3, 3] = {0}", cypherKey.GetValue(3, 3));
        }

        /*
        byte[,] substitution(byte[,] key, string original) {
            byte[] substitutionBox = new byte[4];
            // Create a new instance of the RijndaelManaged
            // class.  This generates a new key and initialization 
            // vector (IV).
            using (RijndaelManaged myRijndael = new RijndaelManaged()) {

                //string original = "Here is some data to encrypt!";

                myRijndael.GenerateKey();
                myRijndael.GenerateIV();
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

                // Decrypt the bytes to a string.
                //string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                //Display the original data and the decrypted data.
                Console.WriteLine("Original:   {0}", original);
                //Console.WriteLine("Round Trip: {0}", roundtrip);
            }
            return key;
        }
        */

        /*
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV) {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged()) {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream()) {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        */

        /*
        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV) {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged()) {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText)) {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
        */
    }
}
