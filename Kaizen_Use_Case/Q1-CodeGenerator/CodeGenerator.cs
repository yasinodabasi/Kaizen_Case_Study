using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen_Use_Case {
    public class CodeGenerator {
        // Character set to be used for generating codes
        private static readonly char[] _characters = "ACDEFGHKLMNPRTXYZ234579".ToCharArray();
        // Length of the generated code
        private static readonly int _codeLength = 8;
        // Set to keep track of generated codes and ensure uniqueness
        private static readonly HashSet<string> _generatedCodes = new HashSet<string>();
        // Random number generator for secure random numbers
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();


        public string GenerateCode() {
            string code;
            do {
                char[] codeChars = new char[_codeLength];

                // Generate the first 7 characters randomly
                for (int i = 0; i < _codeLength - 1; i++) {
                    codeChars[i] = _characters[GetRandomInt(_characters.Length)];
                }

                // Calculate the check character
                codeChars[_codeLength - 1] = CalculateCheckCharacter(codeChars);

                code = new string(codeChars);
            } while (!CheckCode(code) || !_generatedCodes.Add(code)); // Ensure the code is valid and unique

            return code;
        }

        private char CalculateCheckCharacter(char[] codeChars) {
            // More complex check character calculation algorithm
            int hash = 0;
            for (int i = 0; i < _codeLength - 1; i++) {
                // Calculate hash using a multiplier and current character
                hash = (hash * 31 + codeChars[i]) % _characters.Length;
            }
            return _characters[hash];
        }

        public bool CheckCode(string code) {
            if (code.Length != _codeLength) return false;

            // Check if every character in the code is valid
            if (code.Any(c => !_characters.Contains(c))) return false;

            // Extract the first 7 characters from the code
            char[] codeChars = code.Substring(0, _codeLength - 1).ToCharArray();

            // Calculate the expected check character and compare
            char expectedCheckCharacter = CalculateCheckCharacter(codeChars);
            return code[_codeLength - 1] == expectedCheckCharacter;
        }

        private int GetRandomInt(int max) {
            byte[] randomNumber = new byte[4];
            rng.GetBytes(randomNumber); // Generate secure random bytes
                                        // Convert bytes to an integer and get a non-negative result within the specified range
            return Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % max;
        }
    }
}
