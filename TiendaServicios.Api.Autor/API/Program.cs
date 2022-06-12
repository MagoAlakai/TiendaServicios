WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextoAutor>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ContextoAutor))));

builder.Services.AddGrpc(opt => opt.EnableDetailedErrors = true);

WebApplication app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<AutorService>();
    endpoints.MapGrpcService<GradoAcademicoService>();
    endpoints.MapGet("/", async context
        => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client."));
});

app.Run();
