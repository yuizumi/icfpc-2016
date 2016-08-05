using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    static class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1) {
                Console.Error.WriteLine("引数の数が違います。");
                return 1;
            }

            try {
                IParsedSource source;
                using (var reader = new StreamReader(args[0]))
                    source = Parser.Parse(Lexer.Parse(reader));

                AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName {Name = "Main"},
                    AssemblyBuilderAccess.ReflectionOnly);
                ModuleBuilder module = assembly.DefineDynamicModule("Main.exe");
                var ctx = new RootContext(source, module);
                ctx.Compile();
                foreach (var userType in ctx.UserTypes) userType.Write(Console.Out);
            } catch (NFlatLineNumberedException e) {
                Console.Error.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }
    }
}
