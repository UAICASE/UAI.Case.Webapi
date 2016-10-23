using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UAI.Case.Exceptions;
using UAI.Case.Exceptions.Security;

namespace UAI.Case.Security
{
    public static class Cryptography
    {
        private const string CRYPTO_KEY = "3A8B7284FBFF53A8B72853A8";
        private const string SIMMETRIC_CRYPTO_KEY = "caseuai";
        public static string MD5Hash(string text)
        {
            MD5 md5 = MD5.Create();

            //compute hash from the bytes of text
            

            //get hash result after compute it
            byte[] result = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string Encrypt(string key, string valueToEncript, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(valueToEncript);

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                //MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                MD5 hashmd5 = MD5.Create();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Dispose();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDES tdes = TripleDES.Create();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Dispose();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string key, string cipheredValue, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipheredValue);


            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5 hashmd5 = MD5.Create();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Dispose();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDES tdes = TripleDES.Create();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Dispose();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }



        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

       
        public static string SimmetricEncrypt(string text)
       
        {
            SymmetricAlgorithm algorithm = TripleDES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string SimmetricDecrypt(string text)
          
        {
            SymmetricAlgorithm algorithm = TripleDES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }


        public static string SimmetricEncryptObject(object decryptedObject)
        {
            var decrypted = JsonConvert.SerializeObject(decryptedObject);
            var encrypted = Cryptography.SimmetricEncrypt(decrypted);
            return encrypted;
        }


        private static string ProcessSimmetric(string Input, string Key)
        {
            if (Input.Length != Key.Length)
            {
                throw new ArgumentException("Key is not the same length as the input string");
            }
            ASCIIEncoding Encoding = new ASCIIEncoding();
            byte[] InputArray = Encoding.GetBytes(Input);
            byte[] KeyArray = Encoding.GetBytes(Key);
            byte[] OutputArray = new byte[InputArray.Length];
            for (int x = 0; x < InputArray.Length; ++x)
            {
                OutputArray[x] = (byte)(InputArray[x] ^ Key[x]);
            }
            return Encoding.GetString(OutputArray);
        }

        public static T SimmetricDecryptObject<T>(string encriptedObject)
        {
            try
            {
                var decryptedJsonToken = Cryptography.SimmetricDecrypt(encriptedObject);
                T decrypted = JsonConvert.DeserializeObject<T>(decryptedJsonToken);
                return decrypted;
            }
            catch (Exception)
            {

                throw new InvalidTokenException("Token inválido!");
            }
        }

        public static string EncryptObject(object decryptedObject)
        {
            var securityToken = JsonConvert.SerializeObject(decryptedObject);
            var encryptedtoken = Cryptography.Encrypt(CRYPTO_KEY, securityToken, false);
            return encryptedtoken;
        }

        public static T DecryptObject<T>(string encriptedObject)
        {
            try
            {
                var decryptedJsonToken = Cryptography.Decrypt(CRYPTO_KEY, encriptedObject, false);
                T token = JsonConvert.DeserializeObject<T>(decryptedJsonToken);
                return token;
            }
            catch (Exception)
            {

                throw new InvalidTokenException("Token inválido!");
            }
        }
    }
}
