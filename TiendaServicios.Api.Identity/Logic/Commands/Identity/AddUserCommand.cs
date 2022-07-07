namespace TiendaServicios.Api.Identity.Logic.Commands.Identity;

public sealed class AddUserCommand : ICommand<AddUserResponse, AddUserRequest>
{
    private readonly AddUserRequest _request;
    private readonly ContextoIdentity _ctx;
    public AddUserCommand(AddUserRequest request, ContextoIdentity ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<AddUserResponse>> RunCommandAsync()
    {
        _request.UserModel.Password = Data.EncryptingPasswordFactory.EncodePasswordToBase64(_request.UserModel.Password);
        User user = _request.UserModel.MapToAddUserModelFromRequest();

        _ctx.Add(user);
        await _ctx.SaveChangesAsync();

        UserModel user_model = user.MapToUserModel();
        user_model.Password = Data.EncryptingPasswordFactory.DecodeFrom64(user_model.Password);

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

        return CommandValueObject<AddUserResponse>.Success(add_user_response);
    }
}
