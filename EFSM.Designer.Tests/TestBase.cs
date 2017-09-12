using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Engine;
using EFSM.Designer.Interfaces;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using NSubstitute;

namespace EFSM.Designer.Tests
{
    public abstract class TestBase
    {
        public TestBase()
        {
            ApplicationContainer.Container = CreateTestContainer();
        }

        private IContainer CreateTestContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(Substitute.For<IViewService>()).As<IViewService>();

            // Register all view models
            builder.RegisterAssemblyTypes(typeof(MainViewModel).Assembly)
                .Where(t => t.IsSubclassOf(typeof(ViewModelBase)))
                .AsSelf();

            builder.RegisterType(Substitute.For<IUndoProvider>().GetType()).As<IUndoProvider>();
            builder.RegisterType(Substitute.For<IIsDirtyService>().GetType()).As<IIsDirtyService>();

            builder.RegisterType<FilePersistor>().As<IPersistor>();
            builder.RegisterType<SelectionService>().As<ISelectionService>();
            builder.RegisterType<UndoService<StateMachine>>().As<IUndoService<StateMachine>>();

            return builder.Build();
        }
    }
}
