using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using HotChocolate.Types.Descriptors;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace Product.QL.Interceptors
{
    public class FilterCollectionTypeInterceptor : TypeInterceptor
    {
        private static readonly List<string> _mappedTypes = new();

        private static bool IsCollectionType(Type t)
            => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>);

        public override void OnBeforeRegisterDependencies(ITypeDiscoveryContext discoveryContext, DefinitionBase? definition)
        {
            if (definition is not ObjectTypeDefinition objectTypeDefinition) return;

            for (var i = 0; i < objectTypeDefinition.Fields.Count; i++)
            {
                var field = objectTypeDefinition.Fields[i];
                if (field.ResultType is null || !IsCollectionType(field.ResultType)) continue;

                var key = $"{objectTypeDefinition.Name.ToLower()}.{field.Name.ToLower()}";

                if (_mappedTypes.Any(e => e == key)) continue;

                var descriptor = field.ToDescriptor(discoveryContext.DescriptorContext);

                descriptor.UseFiltering(typeof(FilterInputType<>).MakeGenericType(field.ResultType.GenericTypeArguments[0]));

                objectTypeDefinition.Fields[i] = descriptor.ToDefinition();
                _mappedTypes.Add(key);
            }
        }
    }
}
