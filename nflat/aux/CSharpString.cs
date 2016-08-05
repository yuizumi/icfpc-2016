using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace NFlat.Micro
{
    internal static class CSharpString
    {
        internal static string Primitive(object obj)
        {
            return GetString(new CodePrimitiveExpression(obj));
        }

        internal static string Type(Type type)
        {
            return GetString(new CodeTypeReferenceExpression(type));
        }

        private static string GetString(CodeExpression expr)
        {
            using (var cs = CodeDomProvider.CreateProvider("CSharp")) {
                var sw = new StringWriter();
                cs.GenerateCodeFromExpression(expr, sw, new CodeGeneratorOptions());
                return sw.ToString();
            }
        }
    }
}
