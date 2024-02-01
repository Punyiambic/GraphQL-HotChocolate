using Product.Data.Context;
using Product.QL.Filters;
using Product.QL.Listeners;
using Product.QL.Query;
using Product.QL.Types;
using HotChocolate.AspNetCore;
using HotChocolate.Language;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddDbContextPool<ProductDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"), x => { x.EnableRetryOnFailure(3); });
    
    //Uncomment below line to enable EF Core sql logging
    //.LogTo(Console.WriteLine, LogLevel.Trace);
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services
    .AddSha256DocumentHashProvider(HashFormat.Hex)
    .AddGraphQLServer()
    .AddQueryType<ProductQuery>()
    .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
    .AddErrorFilter<GraphQLErrorFilter>()
    .RegisterDbContext<ProductDataContext>(DbContextKind.Resolver)
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddTypeExtension<BrandType>()
    .AddTypeExtension<LobTeamType>()
    .SetPagingOptions(new PagingOptions
    {
        DefaultPageSize = 50,
        MaxPageSize = 500,
        IncludeTotalCount = true,
        AllowBackwardPagination = true
    })
    //.TryAddTypeInterceptor<FilterCollectionTypeInterceptor>()
    ;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.MapGraphQL().WithOptions( new GraphQLServerOptions
{
    EnableBatching = true
});
app.UseResponseCompression();
app.UseCors("*");
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapBananaCakePop("/ui/graphql").WithOptions(new GraphQLToolOptions
    {
        Enable = true,
        ServeMode = GraphQLToolServeMode.Embedded,
        Title = "ProductQL",
        GraphQLEndpoint = "/graphql",
        Document = "query { \r\n    lobTeams {\r\n        name\r\n        brands {\r\n            name\r\n            lobs {\r\n                name\r\n            }\r\n        }\r\n    }\r\n}"
    });

    endpoints.MapGet("/", (context) =>
    {
        context.Response.Redirect("/ui/graphql", true);
        return Task.FromResult(0);
    });

});
app.Run();
