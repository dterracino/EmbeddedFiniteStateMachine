using EFSM.Designer.Interfaces;
using EFSM.Domain;
using System;

namespace EFSM.Designer.Model
{
    public class StateFactory : ITool
    {
        private readonly Func<State> _factory;
        private readonly string _category;
        private readonly string _name;

        public StateFactory(string category, string name, Func<State> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _category = category;
            _name = name;
        }

        public State Create()
        {
            return _factory();
        }

        public string Category
        {
            get { return _category; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return null; }
        }

        public bool ShowInToolbox
        {
            get { return true; }
        }
    }
}
