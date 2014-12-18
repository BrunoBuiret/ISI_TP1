using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace TP1
{
    class EncryptorDecryptor
    {
        protected static Regex keyRegex;

        static EncryptorDecryptor()
        {
            keyRegex = new Regex(@"^[a-zA-Z]+$");
        }

        public String encrypt(String key, String text)
        {
            if(this.isKeyValid(key) && text.Length > 0)
            {
                // Lower the key string to avoid problems
                key = key.ToLower();
                text = text.Trim();

                // Initialize the text matrix
                int matrixHeight = (int) Math.Ceiling((decimal) text.Length / key.Length);

                char[,] textMatrix = new char[key.Length, matrixHeight];
                int x, y;

                for(y = 0; y < matrixHeight; y++)
                {
                    for(x = 0; x < key.Length; x++)
                    {
                        textMatrix[x, y] = '\0';
                    }
                }

                // Populate the text matrix
                x = y = 0;

                foreach (char letter in text)
                {
                    textMatrix[x, y] = letter;
                    x++;

                    if(x == key.Length)
                    {
                        x = 0;
                        y++;
                    }
                }

                // Transform the key into letters position
                int[] asciiKey = new int[key.Length];

                for(x = 0; x < key.Length; x++)
                {
                    asciiKey[x] = ((int) key[x]) - ((int) 'a');
                }

                // Determine the indexes to read
                int[] columnIndexes = new int[key.Length];
                int z;
                ArrayList ignoredColumns = new ArrayList();

                for(x = 0; x < key.Length; x++)
                {
                    // y holds the minimum's position

                    // Determine the column to start with
                    for(y = 0; ignoredColumns.Contains(y); y++);

                    // Determine the minimum's position
                    for(z = y + 1; z < key.Length; z++)
                    {
                        if(!ignoredColumns.Contains(z) && asciiKey[z] < asciiKey[y])
                        {
                            y = z;
                        }
                    }

                    // Add the index of the minimum and now ignore it.
                    columnIndexes[y] = x;
                    ignoredColumns.Add(y);
                }

                // Build the encrypted string
                StringBuilder encryptedText = new StringBuilder();
                ignoredColumns.Clear();

                for(x = 0; x < key.Length; x++)
                {
                    // y holds the minimum's position

                    // Determine the column to start with
                    for(y = 0; ignoredColumns.Contains(y); y++);

                    // Determine the minimum's position
                    for(z = y + 1; z < key.Length; z++)
                    {
                        if (!ignoredColumns.Contains(z) && asciiKey[z] < asciiKey[y])
                        {
                            y = z;
                        }
                    }

                    // Add the column to the encrypted text and ignore it now
                    for(z = 0; z < matrixHeight; z++)
                    {
                        if(textMatrix[y, z] != '\0')
                        {
                            encryptedText.Append(textMatrix[y, z]);
                        }
                    }

                    ignoredColumns.Add(y);
                }

                return encryptedText.ToString();    
            }

            return text;
        }

        public String decrypt(String key, String text)
        {
            if (this.isKeyValid(key) && text.Length > 0)
            {
                // Lower the key string to avoid problems
                key = key.ToLower();

                // Initialize the text matrix
                int matrixHeight = (int) Math.Ceiling((decimal) text.Length / key.Length);

                char[,] textMatrix = new char[key.Length, matrixHeight];
                int x, y;

                for (y = 0; y < matrixHeight; y++)
                {
                    for (x = 0; x < key.Length; x++)
                    {
                        textMatrix[x, y] = '\0';
                    }
                }

                // Transform the key into letters position
                int[] asciiKey = new int[key.Length];

                for(x = 0; x < key.Length; x++)
                {
                    asciiKey[x] = ((int) key[x]) - ((int) 'a');
                }

                // Determine the indexes to read
                int[] columnIndexes = new int[key.Length];
                int z;
                ArrayList ignoredColumns = new ArrayList();

                for (x = 0; x < key.Length; x++)
                {
                    // y holds the minimum's position

                    // Determine the column to start with
                    for (y = 0; ignoredColumns.Contains(y); y++);

                    // Determine the minimum's position
                    for (z = y + 1; z < key.Length; z++)
                    {
                        if (!ignoredColumns.Contains(z) && asciiKey[z] < asciiKey[y])
                        {
                            y = z;
                        }
                    }

                    // Add the index of the minimum and now ignore it.
                    columnIndexes[y] = x;
                    ignoredColumns.Add(y);
                }

                // Before populating the matrix, determine if it'll be complete or not
                if(text.Length < matrixHeight * key.Length)
                {
                    // If not, we need to fill in the blanks to reconstruct correctly
                    int blanksNumber = matrixHeight * key.Length - text.Length;

                    // Swap the keys and values of the column indexes to retrieve the order
                    int[] swappedColumnIndexes = new int[key.Length];

                    for(x = 0; x < key.Length; x++)
                    {
                        swappedColumnIndexes[columnIndexes[x]] = x;
                    }

                    for(x = 0; x < key.Length; x++)
                    {
                        if (swappedColumnIndexes[x] >= key.Length - blanksNumber)
                        {
                            text = text.Insert(x * matrixHeight + (matrixHeight - 1), "\0");
                        }
                    }
                }

                // Populate the text matrix
                for(y = 0; y < matrixHeight; y++)
                {
                    for(x = 0; x < key.Length; x++)
                    {
                        textMatrix[x, y] = columnIndexes[x] * matrixHeight + y < text.Length ? text[columnIndexes[x] * matrixHeight + y] : '\0';
                    }
                }

                // Build the encrypted string
                StringBuilder decryptedText = new StringBuilder();

                for(y = 0; y < matrixHeight; y++)
                {
                    for(x = 0; x < key.Length; x++)
                    {
                        if (textMatrix[x, y] != '\0')
                        {
                            decryptedText.Append(textMatrix[x, y]);
                        }
                    }
                }

                return decryptedText.ToString();
            }

            return text;
        }

        private Boolean isKeyValid(String key)
        {
            return key != "" && key.Length >= 2 && EncryptorDecryptor.keyRegex.IsMatch(key);
        }
    }
}