using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            SymmetricAlgorithm aes = new AesCryptoServiceProvider();
            aes.KeySize = 256; // This is with 256 bits

            //This is incomplete. Need to create a powershell command let and then use it

            
        }
    }
}
