namespace TiendaServicios.Api.Gateway.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : Controller
{
  
    private UserServices.UserServicesClient CreateClient()
    {
        HttpClientHandler http_client_handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12
        };
        http_client_handler.UseUnsignedServerCertificateValidation();

        HttpClient http_client = new(http_client_handler);
        GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.identity:53449", new()
        {
            HttpClient = http_client
        });

        UserServices.UserServicesClient client = new(channel);

        return client;
    }

    // POST: api/identity/signup
    [HttpPost("signup", Name = "Signup")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUser(UserModel UserModel)
    {
        AddUserRequest add_user_request = new()
        {
            UserModel = UserModel,
        };

        UserServices.UserServicesClient client = CreateClient();

        AddUserResponse add_user_response = await client.AddUserAsync(add_user_request);
        return CreatedAtRoute("Signup", add_user_response);
    }

    // POST: api/identity/login
    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> CreateToken(string Email, string Password)
    {
        CreateTokenRequest create_token_request = new()
        {
            Email = Email,
            Password = Password
        };

        UserServices.UserServicesClient client = CreateClient();

        CreateTokenResponse create_token_response = await client.CreateTokenAsync(create_token_request);
        return CreatedAtRoute("Login", create_token_response);
    }

    // GET: api/identity/get/{UserId}
    [HttpGet("get/{UserId}", Name = "GetUser")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetUser(int UserId)
    {
        GetUserRequest get_user_request = new()
        {
            UserId = UserId,
        };

        UserServices.UserServicesClient client = CreateClient();

        GetUserResponse get_user_response = await client.GetUserAsync(get_user_request);
        return CreatedAtRoute("GetUser", get_user_response);
    }
}
