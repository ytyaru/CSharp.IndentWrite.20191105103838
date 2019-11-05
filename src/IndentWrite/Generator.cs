using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace IndentWrite
{
    class Generator
    {
        // in/out/ref付きのものはFunc,Actionにないためdelegate定義必須
        // https://teratail.com/questions/66340
        private delegate void CreateCodeDelegate(in IndentedTextWriter tw);
        public void Run()
        {
            Generate(CreateCode);
        }
        // ファイル出力
        private void Generate(CreateCodeDelegate createCode)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            string sourceFile = @"Hello.cs";
            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                createCode(tw);
                tw.Close();
            }
        }
        // コード生成
        public void CreateCode(in IndentedTextWriter tw)
        {
            tw.WriteLine("using System;");
            tw.WriteLine();
            tw.WriteLine("namespace MyNamespace");
            tw.WriteLine("{");
            tw.Indent++;
            tw.WriteLine("public sealed class MyClass");
            tw.WriteLine("{");
            tw.Indent++;
            tw.WriteLine("static void Main(string[] args)");
            tw.WriteLine("{");
            tw.Indent++;
            tw.WriteLine(@"Console.WriteLine(""Hello world"");");
            tw.Indent--;
            tw.WriteLine("}");
            tw.Indent--;
            tw.WriteLine("}");
            tw.Indent--;
            tw.WriteLine("}");
        }
    }
}
