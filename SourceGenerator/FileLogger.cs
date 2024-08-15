﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SourceGenerator
{
    public static class FileLogger
    {
        static FileLogger()
        {
            var now = DateTime.Now;

            _fileName = Path.Combine(@"c:\Users\Acer\", $"{now:dd.MM.yyyyy_HHmmss}.log");
        }

        private static string _fileName;

        public static void WriteLn(string text)
        {
            File.AppendAllLines(_fileName, new List<string> { text});
        }
    }
}
