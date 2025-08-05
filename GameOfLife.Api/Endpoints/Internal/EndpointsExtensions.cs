using System.Reflection;

namespace GameOfLife.Api.Endpoints.Internal;

internal static class EndpointsExtensions
{
    public static void UseEndpoints<TMarker>(this IApplicationBuilder app) => UseEndpoints(app, typeof(TMarker));

    public static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        IEnumerable<TypeInfo> endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (TypeInfo endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, [app]);
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        IEnumerable<TypeInfo> endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x => !x.IsAbstract &&
                        !x.IsInterface &&
                        typeof(IEndpoints).IsAssignableFrom(x));
        return endpointTypes;
    }
}
