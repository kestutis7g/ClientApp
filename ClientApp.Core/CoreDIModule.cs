using Autofac;
using ClientApp.Core.Interfaces;
using ClientApp.Core.Services;

namespace ClientApp.Core;

public class CoreDIModule : Module
{
       protected override void Load(ContainerBuilder builder)
       {
              builder.RegisterType<ClientService>()
                     .As<IClientService>()
                     .InstancePerLifetimeScope();
       }
}
