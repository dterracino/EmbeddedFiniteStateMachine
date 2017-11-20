using System.Collections.Generic;
using System.Linq;
using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public class ConditionEditServiceManager
    {
        private readonly Dictionary<ConditionType, IConditionEditService> _services;

        //TODO: Inject IEnumerable<IConditionEditService>
        public ConditionEditServiceManager()
        {
            var services = new IConditionEditService[]
            {
                //TODO: Add other condition edit types
                new InputConditonEditService(), 
            };

            _services = services
                .ToDictionary(s => s.Type, s => s);
        }

        public IConditionEditService this[ConditionType key]
        {
            get { return _services[key]; }
        }
    }

    public class InputConditonEditService : IConditionEditService
    {
        public ConditionType Type => ConditionType.Input;

        public int? MaximumNumberOfChildren => 0;

        public int? MinimumNumberOfChildren => null;

        public bool CanSelectInput => true;

        public string ErrorMessage => "Cannot have any children";
    }
}