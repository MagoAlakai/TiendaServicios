namespace TiendaServicios.Api.Identity.Logic.Queries.Identity;

public sealed class GetUserQuery : IQuery<GetUserResponse, GetUserRequest>
{
    private readonly GetUserRequest _request;
    private readonly ContextoIdentity _ctx;
    public GetUserQuery(GetUserRequest request, ContextoIdentity ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<GetUserResponse>> RunQueryAsync()
    {
        User? user = await _ctx.User.FindAsync(_request.UserId);
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with ID={_request.UserId} is not found"));
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

        return QueryValueObject<GetUserResponse>.Success(get_user_response);
    }
}
