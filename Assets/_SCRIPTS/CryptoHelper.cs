using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public static class CryptoHelper
{
    private const int SaltSize = 16; // 128 bits
    private const int IvSize = 16;   // 128 bits
    private const int KeySize = 256; // 256 bits
    private const int DerivationIterations = 1000;

    public static string Encrypt(string plainText, string passPhrase)
    {
        byte[] salt = GenerateRandomBytes(SaltSize);
        byte[] iv = GenerateRandomBytes(IvSize);

        var key = new Rfc2898DeriveBytes(passPhrase, salt, DerivationIterations).GetBytes(KeySize / 8);

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    var encrypted = msEncrypt.ToArray();
                    // Return the formatted encrypted data
                    return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(iv)}:{Convert.ToBase64String(encrypted)}";
                }
            }
        }
    }

    public static string Decrypt(string cipherText, string passPhrase)
    {
        //Debug.Log("CryptoHelperData: Message = " + cipherText + " Pass = " + passPhrase);
        string[] parts = cipherText.Split(':');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid encrypted text passed. Expected format: [salt]:[IV]:[data]");
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] iv = Convert.FromBase64String(parts[1]);
        byte[] encrypted = Convert.FromBase64String(parts[2]);

        var key = new Rfc2898DeriveBytes(passPhrase, salt, DerivationIterations).GetBytes(KeySize / 8);

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            {
                using (var msDecrypt = new MemoryStream(encrypted))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }


    private static byte[] GenerateRandomBytes(int size)
    {
        var randomBytes = new byte[size];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        return randomBytes;
    }
}

