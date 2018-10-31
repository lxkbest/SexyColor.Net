using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace SexyColor.Infrastructure
{
    public class SymmetricEncrypt
    {
        private SymmetricEncryptType _mbytEncryptionType;
        private SymmetricAlgorithm _mCSP;
        private string _mstrEncryptedString;
        private string _mstrOriginalString;

        public SymmetricEncrypt()
        {
            this._mbytEncryptionType = SymmetricEncryptType.DES;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType)
        {
            this._mbytEncryptionType = encryptionType;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType, string originalString)
        {
            this._mbytEncryptionType = encryptionType;
            this._mstrOriginalString = originalString;
            this.SetEncryptor();
        }

        public string Decrypt()
        {
            ICryptoTransform transform = this._mCSP.CreateDecryptor(this._mCSP.Key, this._mCSP.IV);
            byte[] buffer = Convert.FromBase64String(this._mstrEncryptedString);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream2.Dispose();
            this._mstrOriginalString = Encoding.Unicode.GetString(stream.ToArray());
            return this._mstrOriginalString;
        }

        public string Decrypt(string encryptedString)
        {
            this._mstrEncryptedString = encryptedString;
            return this.Decrypt();
        }

        public string Decrypt(string encryptedString, SymmetricEncryptType encryptionType)
        {
            this._mstrEncryptedString = encryptedString;
            this._mbytEncryptionType = encryptionType;
            return this.Decrypt();
        }

        public string Encrypt()
        {
            ICryptoTransform transform = this._mCSP.CreateEncryptor(this._mCSP.Key, this._mCSP.IV);
            byte[] bytes = Encoding.Unicode.GetBytes(this._mstrOriginalString);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream2.Dispose();
            this._mstrEncryptedString = Convert.ToBase64String(stream.ToArray());
            return this._mstrEncryptedString;
        }

        public string Encrypt(string originalString)
        {
            this._mstrOriginalString = originalString;
            return this.Encrypt();
        }

        public string Encrypt(string originalString, SymmetricEncryptType encryptionType)
        {
            this._mstrOriginalString = originalString;
            this._mbytEncryptionType = encryptionType;
            return this.Encrypt();
        }

        public string GenerateIV()
        {
            this._mCSP.GenerateIV();
            return Convert.ToBase64String(this._mCSP.IV);
        }

        public string GenerateKey()
        {
            this._mCSP.GenerateKey();
            return Convert.ToBase64String(this._mCSP.Key);
        }

        private void SetEncryptor()
        {
            switch (this._mbytEncryptionType)
            {
                case SymmetricEncryptType.AES:
                    this._mCSP = new AesCng();
                     Aes.Create();
                    break;
                
 
                case SymmetricEncryptType.TripleDES:
                    this._mCSP = new TripleDESCng();
                    break;
            }
            this._mCSP.GenerateKey();
            this._mCSP.GenerateIV();
        }

        public SymmetricAlgorithm CryptoProvider
        {
            get
            {
                return this._mCSP;
            }
            set
            {
                this._mCSP = value;
            }
        }

        public string EncryptedString
        {
            get
            {
                return this._mstrEncryptedString;
            }
            set
            {
                this._mstrEncryptedString = value;
            }
        }

        public SymmetricEncryptType EncryptionType
        {
            get
            {
                return this._mbytEncryptionType;
            }
            set
            {
                if (this._mbytEncryptionType != value)
                {
                    this._mbytEncryptionType = value;
                    this._mstrOriginalString = string.Empty;
                    this._mstrEncryptedString = string.Empty;
                    this.SetEncryptor();
                }
            }
        }

        public byte[] IV
        {
            get
            {
                return this._mCSP.IV;
            }
            set
            {
                this._mCSP.IV = value;
            }
        }

        public string IVString
        {
            get
            {
                return Convert.ToBase64String(this._mCSP.IV);
            }
            set
            {
                this._mCSP.IV = Convert.FromBase64String(value);
            }
        }

        public byte[] key
        {
            get
            {
                return this._mCSP.Key;
            }
            set
            {
                this._mCSP.Key = value;
            }
        }

        public string KeyString
        {
            get
            {
                return Convert.ToBase64String(this._mCSP.Key);
            }
            set
            {
                this._mCSP.Key = Convert.FromBase64String(value);
            }
        }

        public string OriginalString
        {
            get
            {
                return this._mstrOriginalString;
            }
            set
            {
                this._mstrOriginalString = value;
            }
        }
    }
}
