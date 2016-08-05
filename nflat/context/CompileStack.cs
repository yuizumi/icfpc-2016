using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class CompileStack : NFStack
    {
        private List<IValue> mValues;

        internal CompileStack(ICompileContext owner)
        {
            mValues = new List<IValue>();
            Owner = owner;
        }

        protected override List<IValue> Values
        {
            get {
                ErrorHelper.Ensure(mValues != null);
                return mValues;
            }
        }

        internal ICompileContext Owner { get; }

        internal void MarkAsUnused()
        {
            mValues = null;
        }

        internal void ForceEvaluate() => Localize(x => x.IsStable);
        internal void ForceLocalize() => Localize(x => x is Local);

        private void Localize(Func<IValue, bool> preserve)
        {
            for (int i = 0; i < Count; i++) {
                IValue value = Values[i];
                if (preserve(value))
                    continue;
                var local = Owner.Output.MakeVariable(value.Type);
                Owner.Output.RawEmit($"{local.Code} = {value.Get(value.Type).Code};");
                Values[i] = new Local(local);
            }
        }

        internal void CopyFrom(IEnumerable<IValue> values)
        {
            mValues = values.ToList();
        }

        internal void AdjustTo(CompileStack that)
        {
            if (that.mValues == null) {
                return;
            }
            if (this.mValues == null) {
                mValues = that.mValues.ToList();
                return;
            }
            if (this.Count != that.Count) {
                throw Error.InconsistentStack(this.Owner, that.Owner);
            }

            for (int i = 0; i < mValues.Count; i++) {
                var thisValue = this.mValues[i];
                var thatValue = that.mValues[i];
                if (thisValue == thatValue) {
                    continue;
                }
                if (!(thatValue is Local)) {
                    throw Error.InconsistentStack(this.Owner, that.Owner);
                }
                if (!thisValue.Has(thatValue.Type)) {
                    throw Error.InconsistentStack(this.Owner, that.Owner);
                }
                Owner.Output.RawEmit(thatValue.Get(thatValue.Type).Code + " = " +
                                     thisValue.Get(thisValue.Type).Code + ";");
                mValues[i] = thatValue;
            }
        }
    }
}
