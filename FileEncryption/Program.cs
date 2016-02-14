using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryption
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            EncryptDecryptAFile();
        }

        private static void EncryptDecryptAFile()
        {
            Console.WriteLine("You want to encrypt the file or decrypt the file (E/D) ?");
            var option = Console.ReadLine();
            string sourceFile, targetFile, key;
            if (option.Equals("E"))
            {
                Console.WriteLine("Enter the source file location");
                sourceFile = Console.ReadLine();

                Console.WriteLine("Enter a key for encryption...");
                key = Console.ReadLine();

                Console.WriteLine("Destination file will be at the same location as that of the source file...");
                Console.WriteLine(
                    "Encrypting the file, please be patient...file location will open automatically after it is encrypted...");
                targetFile = Path.GetDirectoryName(sourceFile) + "\\" + Path.GetFileNameWithoutExtension(sourceFile) +
                             "_Encrypted" + Path.GetExtension(sourceFile);
                FileCryptography.EncryptFile(sourceFile, targetFile, key);
                Process.Start(Path.GetDirectoryName(sourceFile));
            }
            else if (option.Equals("D"))
            {
                Console.WriteLine("Enter the source file location");
                sourceFile = Console.ReadLine();

                Console.WriteLine("Enter the code for decryption...its the first 8 characters of your spouses common pwd");
                key = Console.ReadLine();

                Console.WriteLine("Destination file will be at the same location as that of the source file...");
                Console.WriteLine(
                    "Decrypting the file, please be patient...file location will open automatically after it is encrypted...");
                targetFile = Path.GetDirectoryName(sourceFile) + "\\" + Path.GetFileNameWithoutExtension(sourceFile) +
                             "_Decrypted" + Path.GetExtension(sourceFile);
                FileCryptography.DecryptFile(sourceFile, targetFile, key);
                Process.Start(Path.GetDirectoryName(sourceFile));
            }
            else
            {
                Console.WriteLine("Sorry, invalid choice");
            }
            Console.WriteLine("Done...Press any key to exit or 'Y' to continue ");
            var doopy = Console.ReadLine();
            if (doopy.Equals("Y"))
            {
                EncryptDecryptAFile();
            }
        }
    }

    internal static class FileCryptography
    {
        public static void EncryptFile(string inputFile, string outputFile, string password)
        {

            try
            {
                //string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                                                   RMCrypto.CreateEncryptor(key, key),
                                                   CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte) data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption failed! {0}", ex);
            }
        }

        public static void DecryptFile(string inputFile, string outputFile, string password)
        {

            {
                //string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                                                   RMCrypto.CreateDecryptor(key, key),
                                                   CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte) data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }
    }
}
