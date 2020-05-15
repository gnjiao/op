using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System.ToolKit.Helpers
{
    /// <summary>
    /// 表示加密帮助类。
    /// </summary>
    public static class SecurityHelper
    {
        static SecurityHelper()
        {
            Vector = Encoding.UTF8.GetBytes("System.Toolkit");
        }

        /// <summary>
        ///     默认加密向量
        /// </summary>
        private static readonly byte[] Vector;

       

       
        public static byte[] Encrypt(string plainText, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);
            return Encrypt(plainText, key, Vector);
        }

        public static string Decrypt(byte[] cipherText, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);
            return Decrypt(cipherText, key, Vector);
        }
        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        /// <param name="encypStr">明文</param>
        /// <returns>密文</returns>
        public static string TextToMd5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] inputBye;
            byte[] outputBye;
            inputBye = System.Text.Encoding.ASCII.GetBytes(encypStr);
            outputBye = md5.ComputeHash(inputBye);
            retStr = Convert.ToBase64String(outputBye);
            return (retStr);
        }
        /// <summary>
        ///     AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">密钥向量</param>
        /// <returns>解密后的字符串</returns>
        public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // 检查参数. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");
            byte[] encrypted;
            // 使用指定的键和IV创建Rijndael对象. 
            using (var rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // 创建一个解密器来执行流转换.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // 创建用于加密的流. 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //将所有数据写入流.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // 从内存流返回加密的字节. 
            return encrypted;
        }

        /// <summary>
        ///     AES解密
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <param name="key">密钥</param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // 检查参数. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");

            // 声明用于保存解密文本的字符串. 
            string plaintext;

            // 使用指定的键和IV创建Rijndael对象. 
            using (var rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                //创建一个解密器来执行流转换.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // 创建用于解密的流. 
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            //从解密流中读取解密后的字节并将它们放在字符串中.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
