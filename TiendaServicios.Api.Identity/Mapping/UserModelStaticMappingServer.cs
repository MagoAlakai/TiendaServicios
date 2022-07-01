using Mapster;
using TiendaServicios.Contracts.Protos.Models;

namespace TiendaServicios.Api.Identity.Mapping
{
    public static class UserModelStaticMappingServer
    {
        public static UserModel MapToUserModel(this User user)
        {
            TypeAdapterConfig<User, UserModel>.NewConfig().IgnoreNullValues(true);
            UserModel user_model = TypeAdapter.Adapt<User, UserModel>(user);
            return user_model;
        }
    }
}
