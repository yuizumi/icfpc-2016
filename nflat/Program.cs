using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    class CommandLineException : Exception
    {
        internal CommandLineException(string message) : base(message)
        {
        }
    }

    static class Program
    {
        static int Main(string[] args)
        {
            try {
                IParsedSource source = Parser.Parse(ReadSourceFiles(args));

                AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName {Name = "Main"},
                    AssemblyBuilderAccess.ReflectionOnly);
                ModuleBuilder module = assembly.DefineDynamicModule("Main.exe");

                var ctx = new RootContext(source, module);
                ctx.Compile();
                Console.WriteLine("#pragma warning disable 0219");
                foreach (var userType in ctx.UserTypes)
                    userType.Write(Console.Out);
            } catch (NFlatLineNumberedException e) {
                Console.Error.WriteLine($"エラー ― {e.Message}");
                return 1;
            } catch (CommandLineException e) {
                Console.Error.WriteLine($"エラー ― {e.Message}");
                return 2;
            }

            return 0;
        }

        static IReadOnlyList<Token> ReadSourceFiles(string[] args)
        {
            var files = new List<string>();

            for (int i = 0; i < args.Length; i++) {
                if (args[i].StartsWith("/r:")) {
                    Assembly.LoadFile(args[i].Substring(3));
                    continue;
                }
                if (!args[i].ToLower().EndsWith(".nf")) {
                    throw new CommandLineException($"{args[i]}: 拡張子が正しくありません。");
                }
                files.Add(args[i]);
            }

            if (files.Count == 0) {
                throw new CommandLineException("ソースファイルが指定されていません。");
            }

            var tokens = new List<Token>();
            files.ForEach(file => Lexer.Parse(file, tokens));
            return tokens.AsReadOnly();
        }
    }
}
