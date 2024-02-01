namespace Product.QL.Filters
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Exception == null? error.Message : error.Exception.Message);
        }
    }
}
