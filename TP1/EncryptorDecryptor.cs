using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TP1
{
    class EncryptorDecryptor
    {
        static Regex keyRegex;

        static EncryptorDecryptor()
        {
            EncryptorDecryptor.keyRegex = new Regex(@"\[a-zA-Z]+\");
        }

        public String encrypt(String key, String text)
        {
            if(this.isKeyValid(key))
            {
                return text;    
            }

            return text;
        }

        public String decrypt(String key, String text)
        {
            if (this.isKeyValid(key))
            {
                return text;
            }

            return text;
        }

        private Boolean isKeyValid(String key)
        {
            return key != "" && key.Length >= 2 && EncryptorDecryptor.keyRegex.IsMatch(key);
        }
    }
}
