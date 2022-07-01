using Mapster;
using TiendaServicios.Contracts.Protos.Models;

namespace TiendaServicios.Api.Identity.Mapping
{
    public static class AddUserStaticMappingFromRequest
    {
        public static User MapToAddUserModelFromRequest(this UserModel user_model)
        {
            TypeAdapterConfig<UserModel, User>.NewConfig().IgnoreNullValues(true)
                .Map(x => x.UserId, src => src.UserId)
                .Map(x => x.Email, src => src.Email)
                .Map(x => x.Password, src => src.Password)
                .Map(x => x.UserGuid, src => src.UserGuid);
            DefaultStaticMappingRules.SetDefaultMappings();
            User user = TypeAdapter.Adapt<UserModel, User>(user_model);
            return user;
        }
    }
}
