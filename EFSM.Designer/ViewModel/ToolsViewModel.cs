using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.Designer.ViewModel
{
    public class ToolsViewModel<TTool> : ViewModelBase
            where TTool : ITool
    {
        private readonly IEnumerable<ToolCategoryViewModel<TTool>> _categories;

        public ToolsViewModel(IEnumerable<TTool> tools)
        {
            var categories = tools
                .Where(t => t.ShowInToolbox)
                .GroupBy(t => t.Category);

            _categories = categories
                .Select(c => new ToolCategoryViewModel<TTool>(c.Key, c.OrderBy(f => f.Name)))
                .OrderBy(c => c.Name)
                .ToArray();
        }



        public IEnumerable<ToolCategoryViewModel<TTool>> Categories => _categories;
    }
}


