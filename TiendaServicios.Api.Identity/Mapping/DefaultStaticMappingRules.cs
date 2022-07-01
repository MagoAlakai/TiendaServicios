using Google.Protobuf.WellKnownTypes;
using Mapster;

namespace TiendaServicios.Api.Identity.Mapping
{
    public class DefaultStaticMappingRules
    {
        public static void SetDefaultMappings()
        {
            TypeAdapterConfig<TimeSpan, Duration>.ForType().MapWith(src => Duration.FromTimeSpan(src));
            TypeAdapterConfig<TimeSpan?, Duration>.ForType().MapWith(src => Duration.FromTimeSpan(src ?? TimeSpan.Zero));
            TypeAdapterConfig<DateTime, Timestamp>.ForType().MapWith(src => Timestamp.FromDateTime(src.ToUniversalTime()));
            TypeAdapterConfig<DateTime?, Timestamp>.ForType().MapWith(src => Timestamp.FromDateTime((src ?? DateTime.MinValue).ToUniversalTime()));
        }
    }
}

