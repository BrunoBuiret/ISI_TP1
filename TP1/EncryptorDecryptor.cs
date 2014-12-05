using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    class EncryptorDecryptor
    {
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
            return key != "";
        }
    }
}
