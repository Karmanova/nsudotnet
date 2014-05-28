using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project1.model
{
    interface CryptoInterface
    {
        void Encrypt(Stream streamIn, Stream streamOut, Stream streamKey);
        void Decrypt(Stream streamIn, Stream streamOut, StreamReader streamKey);
    }
}
