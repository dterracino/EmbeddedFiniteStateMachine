using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public abstract class ToolViewModel : ViewModelBase
    {
        public abstract ITool GetTool();
    }

    public class ToolViewModel<TTool> : ToolViewModel
        where TTool : ITool
    {
        private readonly TTool _tool;
        private IMessageBoxService _messageBoxService;

        public ToolViewModel(TTool tool, IMessageBoxService messageBoxService = null)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));
            _tool = tool;

            _messageBoxService = messageBoxService ?? ApplicationContainer.Container.Resolve<IMessageBoxService>();

            OnMouseLeftButtonDownCommand = new RelayCommand<MouseEventArgs>(MouseLeftButtonDown);
        }

        public string Name => _tool.Name;

        public string Description => _tool.Description;

        public TTool Tool => _tool;

        public override ITool GetTool() => Tool;

        public ICommand OnMouseLeftButtonDownCommand { get; }

        private void MouseLeftButtonDown(MouseEventArgs e)
        {
            try
            {
                var container = e.OriginalSource as FrameworkElement;

                //The tool will be a generic type, so why screw around?
                var toolViewModel = container?.DataContext as ToolViewModel;

                var tool = toolViewModel?.GetTool();

                if (tool != null)
                {
                    var dataObject = new DataObject(typeof(ITool), tool);

                    DragDrop.DoDragDrop(container, dataObject, DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }
    }
}
