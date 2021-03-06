using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace App2
{
    public interface IConverter
    {
        public StreamWriter Convert(string input, string writePath);
    }
}
