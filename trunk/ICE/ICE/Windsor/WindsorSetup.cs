using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using ICE.MasterReceivers;
using ICE.RavenDB;

namespace ICE.Windsor
{
    public class WindsorSetup
    {
        private readonly WindsorContainer _container;
        public WindsorSetup()
        {
            _container = new WindsorContainer();
            _container.AddFacility<StartableFacility>();
            _container.Kernel.Resolver.AddSubResolver(new ListResolver(_container.Kernel));
            _container.Register(Component.For<IConsoleReceiver>().ImplementedBy<ConsoleReceiver>(),
                Component.For<ICommandReceiver>().ImplementedBy<CommandReceiver>(),
                Component.For<IMasterCommandReceiver>().ImplementedBy<DirCommandReceiver>().LifeStyle.Singleton.Start(),
                Component.For<IRavenDBConnector>().ImplementedBy<RavenDBConnector>().LifeStyle.Singleton.Start());
        }

        public WindsorContainer Container
        {
            get { return _container; }
        }
    }
}
