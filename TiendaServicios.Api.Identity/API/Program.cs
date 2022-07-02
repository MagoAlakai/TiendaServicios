WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextoIdentity>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ContextoIdentity))));

builder.Services.AddGrpc(opt => opt.EnableDetailedErrors = true);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

WebApplication app = builder.Build();
using IServiceScope? service_scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
using ContextoIdentity? ctx = service_scope?.ServiceProvider.GetRequiredService<ContextoIdentity>();
ctx?.Database.Migrate();

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<UserService>();
    endpoints.MapGet("/", async context
        => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client."));
});

app.Run();
