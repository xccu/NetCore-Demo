using Microsoft.Extensions.DependencyInjection;

namespace Demo.Decorator;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection Decorate<TService,TNewImplementation>(this IServiceCollection svcs) where TNewImplementation : TService
    {
        var svc = svcs.LastOrDefault(x => x.ServiceType == typeof(TService)) ?? throw new Exception();

        svcs.Remove(svc);

        ServiceDescriptor newSvc;

        if (svc.ImplementationType !=null)
        {
            newSvc = ServiceDescriptor.Describe(typeof(TService), sp => 
            {
                var old = (TService)ActivatorUtilities.CreateInstance(sp,svc.ImplementationType);

                return ActivatorUtilities.CreateInstance<TNewImplementation>(sp,old)!;
            }, svc.Lifetime);

            svcs.Add(newSvc);
        }
        else if (svc.ImplementationFactory !=null)
        {
            newSvc = ServiceDescriptor.Describe(typeof(TService), sp =>
            {
                var old =  (TService)svc.ImplementationFactory(sp);

                return ActivatorUtilities.CreateInstance<TNewImplementation>(sp, old)!;
            },svc.Lifetime);

            svcs.Add(newSvc);
        }
        else
        {
            newSvc = ServiceDescriptor.Describe(typeof(TService), sp =>
            {
                var old = (TService)svc.ImplementationInstance!;

                return ActivatorUtilities.CreateInstance<TNewImplementation>(sp, old)!;
            }, svc.Lifetime);
        }

        svcs.Add(newSvc);

        return svcs;
    }
}
