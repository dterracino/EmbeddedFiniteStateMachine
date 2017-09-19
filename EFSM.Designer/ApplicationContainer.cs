using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Engine;
using EFSM.Designer.Interfaces;
using EFSM.Designer.View;
using EFSM.Designer.ViewModel;
using EFSM.Designer.ViewModel.StateEditor;
using EFSM.Designer.ViewModel.TransitionEditor;
using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer
{
    public static class ApplicationContainer
    {
        public static IContainer Container { get; }

        static ApplicationContainer()
        {
            var builder = new ContainerBuilder();

            // ViewService
            IViewService viewService = new ViewService();

            viewService.Register<StateMachineDialogWindowViewModel, StateMachineDialogWindow>();
            viewService.Register<SimulationViewModel, SimulationWindow>();
            viewService.Register<TransitionEditorViewModel, TransitionEditorDialog>();
            viewService.Register<StateDialogViewModel, StateEditorDialog>();

            builder.RegisterInstance(viewService).As<IViewService>();

            // Register all view models
            builder.RegisterAssemblyTypes(typeof(MainViewModel).Assembly)
                .Where(t => t.IsSubclassOf(typeof(ViewModelBase)))
                .AsSelf();

            builder.RegisterType<FilePersistor>().As<IPersistor>();
            builder.RegisterType<SelectionService>().As<ISelectionService>();
            builder.RegisterType<UndoService<StateMachine>>().As<IUndoService<StateMachine>>();

            builder.RegisterType<DirtyService>()
                .As<IDirtyService>()
                .As<IMarkClean>()
                .As<IMarkDirty>()
                .SingleInstance();

            Container = builder.Build();
        }
    }
}
