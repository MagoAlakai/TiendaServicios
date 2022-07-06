using TiendaServicios.Api.Libro.API.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextoLibreria>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ContextoLibreria))));

builder.Services.AddGrpc(opt => opt.EnableDetailedErrors = true);

WebApplication app = builder.Build();
using IServiceScope? service_scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
using ContextoLibreria? ctx = service_scope?.ServiceProvider.GetRequiredService<ContextoLibreria>();
ctx?.Database.Migrate();
// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<LibroService>();
    endpoints.MapGet("/", async context
        => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client."));
});

app.Run();
