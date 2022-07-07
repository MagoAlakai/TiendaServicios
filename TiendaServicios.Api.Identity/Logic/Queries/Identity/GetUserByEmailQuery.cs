namespace TiendaServicios.Api.Identity.Logic.Queries.Identity;

public sealed class GetUserByEmailQuery : IQuery<GetUserByEmailResponse, GetUserByEmailRequest>
{
    private readonly GetUserByEmailRequest _request;
    private readonly ContextoIdentity _ctx;
    public GetUserByEmailQuery(GetUserByEmailRequest request, ContextoIdentity ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<GetUserByEmailResponse>> RunQueryAsync()
    {
        User? user = await _ctx.User.Where(x => x.Email == _request.UserEmail).FirstOrDefaultAsync();
        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with Email={_request.UserEmail} is not found"));
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

        return QueryValueObject<GetUserByEmailResponse>.Success(get_user_by_email_response);
    }
}
