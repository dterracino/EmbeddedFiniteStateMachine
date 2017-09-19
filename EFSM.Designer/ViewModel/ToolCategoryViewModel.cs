using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.Designer.ViewModel
{
    public class ToolCategoryViewModel<TTool> : ViewModelBase
        where TTool : ITool
    {
        private readonly string _name;
        private readonly ToolViewModel<TTool>[] _tools;

        public ToolCategoryViewModel(string name, IEnumerable<TTool> tools)
        {
            if (tools == null)
                throw new ArgumentNullException(nameof(tools));

            _name = name;
            _tools = tools.Select(t => new ToolViewModel<TTool>(t)).ToArray();
        }

        public string Name => _name;

        public IEnumerable<ToolViewModel<TTool>> Tools => _tools;
    }
}
