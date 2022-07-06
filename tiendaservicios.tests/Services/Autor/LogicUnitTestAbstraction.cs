using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using TiendaServicios.Api.Autor.Database;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using static Google.Protobuf.JsonFormatter;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace TiendaServicios.Tests.Services.Autor;

public abstract class LogicUnitTestAbstraction
{
    protected const string _default_app_settings_json_filename = "TiendaServicios.Api.Autor.appsettings.Tests.json";
    protected IConfiguration? _configuration;
    protected IHost? _host;
    protected readonly IServiceCollection _services;
    protected IServiceProvider? _provider;
    protected IDbConnection? _connection;
    protected TestServer? _test_server;
    protected ContextoAutor? _ctx;

    public LogicUnitTestAbstraction()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(_default_app_settings_json_filename).Build();
        _services = new ServiceCollection().AddLogging();
        _host = new HostBuilder()
                .ConfigureWebHost(web_builder =>
                {
                    web_builder.UseTestServer()
                        .UseEnvironment("Development")
                        .UseConfiguration(_configuration)
                        .UseTestServer();
                })
                .UseEnvironment("Development")
                .Start();
        _test_server = _host.GetTestServer();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _host?.StopAsync();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        if (_services is null)
        {
            throw new Exception(nameof(_services));
        }

        if (_configuration is null)
        {
            throw new Exception(nameof(_configuration));
        }

        if (_test_server is null)
        {
            throw new Exception(nameof(_test_server));
        }

        DefaultHttpContext default_http_context = new();
        default_http_context.Request.Path = "/";
        default_http_context.Request.Host = new("localhost");
        string fake_tenant_id = "LogicUnitTestAbstraction";
        default_http_context.Request.Headers["Tenant-ID"] = fake_tenant_id;

        ClaimsPrincipal default_http_context_user = new();
        ClaimsIdentity claims_identity = new();
        Claim email_claim = new("Email", "mago+1@gmail.com");
        Claim passowrd_claim = new("Password", "Iloveswing77!");
        claims_identity.AddClaim(email_claim);

        default_http_context_user.AddIdentity(claims_identity);
        default_http_context.User = default_http_context_user;
        Mock<IHttpContextAccessor> moq_http_context_accessor = new();
        moq_http_context_accessor.Setup(x => x.HttpContext).Returns(default_http_context);
        _services.AddSingleton<IHttpContextAccessor>(moq_http_context_accessor.Object);

        _host.RunAsync();
        _ctx = _host?.Services.GetService(typeof(ContextoAutor)) as ContextoAutor;
        if (_ctx?.Database is null)
        {
            throw new Exception(nameof(_ctx));
        }

        if (_host?.Services.GetService(typeof(Settings)) is not Settings settings)
        {
            throw new Exception(nameof(settings));
        }
        _ctx.Database.Migrate();
    }
}