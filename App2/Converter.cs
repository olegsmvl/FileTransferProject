using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App2
{
    public class Converter : IConverter
    {
        public StreamWriter Convert(string input, string writePath)
        {
            return new StreamWriter(writePath, false, Encoding.Unicode);
        }
    }
}
