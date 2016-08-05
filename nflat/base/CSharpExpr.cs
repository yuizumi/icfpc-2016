using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace NFlat.Micro
{
    internal struct CSharpExpr
    {
        internal CSharpExpr(string code, Type type)
        {
            Code = code;
            Type = type;
        }

        internal string Code { get; }
        internal Type Type { get; }

        public override string ToString() => Code;
    }
}
