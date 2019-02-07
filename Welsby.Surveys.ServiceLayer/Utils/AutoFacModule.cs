using System.Reflection;
using Autofac;

namespace Welsby.Surveys.ServiceLayer.Utils
{
    public class AutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder) 
        {
            builder.RegisterAssemblyTypes(GetType().GetTypeInfo().Assembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsImplementedInterfaces(); 
        }
    }
}
