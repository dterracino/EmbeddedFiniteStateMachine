using Autofac;
using Cas.Common.WPF.Interfaces;
using NSubstitute;

namespace EFSM.Designer.Tests
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(vs => Substitute.For<IViewService>()).As<IViewService>();
        }
    }
}
