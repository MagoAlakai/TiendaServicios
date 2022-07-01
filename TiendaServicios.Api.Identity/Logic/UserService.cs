namespace TiendaServicios.Api.Identity.Logic;

public class UserService : UserServices.UserServicesBase
{
    private readonly ContextoIdentity _identityContext;
    private readonly ILogger<UserService> _logger;

    public UserService(ContextoIdentity? identityContext, ILogger<UserService>? logger)
    {
        _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        User? user = await _identityContext.User.FindAsync(request.UserId);
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with ID={request.UserId} is not found"));
        }

        UserModel user_model = user.MapToUserModel();

        GetUserResponse get_user_response = user_model is null
            ? new GetUserResponse
            {
                Success = false,
                UserModel = user_model,
            }
            : new GetUserResponse
            {
                Success = true,
                UserModel = user_model,
            };

        return get_user_response;
    }

    public override async Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest request, ServerCallContext context)
    {
        User? user = await _identityContext.User.Where(x =>x.Email == request.UserEmail).FirstOrDefaultAsync();
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with Email={request.UserEmail} is not found"));
        }

        UserModel user_model = user.MapToUserModel();

        GetUserByEmailResponse get_user_by_email_response = user_model is null
            ? new GetUserByEmailResponse
            {
                Success = false,
                UserModel = user_model,
            }
            : new GetUserByEmailResponse
            {
                Success = true,
                UserModel = user_model,
            };

        return get_user_by_email_response;
    }

    public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        User user = request.UserModel.MapToAddUserModelFromRequest();

        _identityContext.Add(user);
        await _identityContext.SaveChangesAsync();

        UserModel user_model = user.MapToUserModel();

        AddUserResponse add_user_response = user_model is null
            ? new AddUserResponse
            {
                Success = false,
                UserModel = user_model,
            }
            : new AddUserResponse
            {
                Success = true,
                UserModel = user_model,
            };

        return add_user_response;
    }
}
