using System;
using System.IO;
//using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
//using System.Reflection; // TypeAttributes

namespace IndentWrite
{
    class Generator
    {
        // in/out/ref付きのものはFunc,Actionにないためdelegate定義必須
        // https://teratail.com/questions/66340
        private delegate void CreateCodeDelegate(in IndentedTextWriter tw);
        public void Run()
        {
//            Generate(CreateCode());
//            Generate(this.CreateCode);
            Generate(CreateCode);
        }
        // ファイル出力
//        private static void Generate(CodeCompileUnit compileunit)
//        private void Generate(Action<IndentedTextWriter> createCode)
        private void Generate(CreateCodeDelegate createCode)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            string sourceFile = @"Hello.cs";
            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                createCode(tw);
//                provider.GenerateCodeFromCompileUnit(compileunit, tw,
//                    new CodeGeneratorOptions());
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
        /*
        // コード生成
        private static CodeCompileUnit CreateCode()
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("MyNamespace");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            CodeTypeDeclaration targetClass = new CodeTypeDeclaration("MyClass");
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public | TypeAttributes.Sealed;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);
            return targetUnit;
        }
        */
    }
}
