using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliType : NFType, IIndexer
    {
        private readonly IIndexer mIndexer;

        internal CliType(TypePool owner, Type type)
        {
            Owner = owner;
            Type = type;

            BaseType = (type.BaseType == null) ? null : owner.Get(type.BaseType);

            foreach (var group in type.GetMembers().GroupBy(m => m.Name))
                Members.Add(Identifier.Of(group.Key), MakeMember(group));
            var indexers = type.GetDefaultMembers().OfType<PropertyInfo>()
                .Where(p => p.GetIndexParameters().Length != 0);
            if (type.IsArray) { mIndexer = new CliArrayAccess(type); }
            if (indexers.Any()) { mIndexer = CliIndexerGroup.Create(indexers); }
        }

        internal override TypePool Owner { get; }
        internal override Type Type { get; }
        internal override NFType BaseType { get; }

        internal override string Text => $"|T:{Type}|";

        public CSharpExpr GetForIndex(IValue instance, CliArguments args)
        {
            if (mIndexer == null) throw Error.NotIndexer(instance);
            return mIndexer.GetForIndex(instance, args);
        }

        private ITypeMember MakeMember(IEnumerable<MemberInfo> members)
        {
            if (Type.IsGenericTypeDefinition) {
                return new CliGeneric(members.First());
            }
            switch (members.First().MemberType) {
                case MemberTypes.Constructor:
                    return CliMethodGroup.Create(members.Cast<ConstructorInfo>());
                case MemberTypes.Event:
                    goto default;  // Not supported yet.
                case MemberTypes.Field:
                    return new CliFieldAccess(members.Single() as FieldInfo);
                case MemberTypes.Method:
                    return CliMethodGroup.Create(members.Cast<MethodInfo>());
                case MemberTypes.NestedType:
                    return Owner.Get(members.Single() as Type);
                case MemberTypes.Property:
                    var property = members.Cast<PropertyInfo>().SingleOrDefault(
                        p => p.GetIndexParameters().Length == 0);
                    if (property != null) { return new CliPropertyAccess(property); }
                    goto default;  // Indexers supported separately.
                case MemberTypes.TypeInfo:
                    return Owner.Get(members.Single() as Type);
                default:
                    return new CliUnknown(members.ToList().AsReadOnly());
            }
        }

        public override string ToString() => $"CliType({Type})";
    }
}
