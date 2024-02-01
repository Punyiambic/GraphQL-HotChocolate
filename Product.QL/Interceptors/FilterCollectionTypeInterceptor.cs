using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using HotChocolate.Types.Descriptors;

namespace Product.QL.Interceptors
{
    public class FilterCollectionTypeInterceptor : TypeInterceptor
    {
        private static bool IsCollectionType(Type t)
            => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>);

        public override void OnAfterRegisterDependencies(ITypeDiscoveryContext discoveryContext, DefinitionBase? definition)
        {
            if (definition is not ObjectTypeDefinition objectTypeDefinition) return;

            for (var i = 0; i < objectTypeDefinition.Fields.Count; i++)
            {
                var field = objectTypeDefinition.Fields[i];
                if (field.ResultType is null || !IsCollectionType(field.ResultType)) continue;

                var descriptor = field.ToDescriptor(discoveryContext.DescriptorContext)
                    .UseProjection()
                    //.UseFiltering()
                    //.UseSorting()
                    ;
                objectTypeDefinition.Fields[i] = descriptor.ToDefinition();
            }
        }
    }
}
