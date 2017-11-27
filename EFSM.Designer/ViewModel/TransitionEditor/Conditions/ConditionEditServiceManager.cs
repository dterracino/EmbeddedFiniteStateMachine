using EFSM.Domain;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public class ConditionEditServiceManager
    {
        private readonly Dictionary<ConditionType, IConditionEditService> _services;

        public ConditionEditServiceManager(IEnumerable<IConditionEditService> services)
        {
            _services = services
                .ToDictionary(s => s.Type, s => s);
        }

        public IConditionEditService this[ConditionType key]
        {
            get { return _services[key]; }
        }
    }
}