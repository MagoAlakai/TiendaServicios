namespace TiendaServicios.Api.Identity.API.Services;

public class UserService : UserServices.UserServicesBase
{
    private readonly ContextoIdentity _identityContext;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;

    public UserService(ContextoIdentity? identityContext, ILogger<UserService>? logger, IConfiguration config)
    {
        _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = config;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        GetUserQuery query = new(request, _identityContext);
        QueryValueObject<GetUserResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest request, ServerCallContext context)
    {
        GetUserByEmailQuery query = new(request, _identityContext);
        QueryValueObject<GetUserByEmailResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        AddUserCommand command = new(request, _identityContext);
        CommandValueObject<AddUserResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<CreateTokenResponse> CreateToken(CreateTokenRequest request, ServerCallContext context)
    {
        string? user_token = default!;

        User? user = await _identityContext.User.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with Email={request.Email} is not found"));
        }

        if (Data.EncryptingPasswordFactory.DecodeFrom64(user.Password) == request.Password)
        {
            string encrypted_password = user.Password;
            user_token = BuildToken(request.Email, encrypted_password);
        }

        CreateTokenResponse create_user_response = user_token is null
                ? new CreateTokenResponse
                {
                    Success = false,
                    Token = user_token
                }
                : new CreateTokenResponse
                {
                    Success = true,
                    Token = user_token
                };
        return create_user_response;
    }

    public string? BuildToken(string Email, string Password)
    {
        if (Email is null || Password is null) { return null; }

        //create claims details based on the user information
        Claim[] claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("Password", Password),
                    };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials signIn = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signIn);

        string user_token = new JwtSecurityTokenHandler().WriteToken(token);
        return user_token;
    }
}
