namespace TiendaServicios.Api.Gateway.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : Controller
{
    public IConfiguration _configuration;

    public IdentityController(IConfiguration config)
    {
        _configuration = config;
    }
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
    public async Task<IActionResult> Post(string Email, string Password)
    {
        if (Email is not null && Password is not null)
        {

            UserServices.UserServicesClient client = CreateClient();
            GetUserByEmailResponse user = await client.GetUserByEmailAsync(new GetUserByEmailRequest() { UserEmail = Email });

            if (user != null)
            {
                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", user.UserModel.Email),
                        new Claim("Password", user.UserModel.Password),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
        else
        {
            return BadRequest();
        }
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
