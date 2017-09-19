using System;

namespace EFSM.Generator.Model
{
    internal abstract class GeneratedBase<TModel>
        where TModel : class
    {
        protected GeneratedBase(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;
        }

        public TModel Model { get; }
    }

    internal abstract class IndexedBase<TModel> : GeneratedBase<TModel>
        where TModel : class 
    {
        public int Index { get; }

        protected IndexedBase(TModel model, int index) 
            : base(model)
        {
            Index = index;
        }

        public abstract string IndexDefineName { get; }

        public virtual string IndexDefine => $"#define {IndexDefineName} {Index}";
    }
}