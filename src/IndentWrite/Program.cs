using System;
using System.IO;
//using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
//using System.Reflection; // TypeAttributes

namespace IndentWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            new Generator().Run();
        }
    }
}
