namespace TiendaServicios.Api.Carrito.Mapping
{
    public static class UpdateCarritoSesionStaticMappingFromRequest
    {
        public static CarritoSesion MapToUpdateCarritoSesionModelFromRequest(this CarritoSesionModel carrito_sesion_model)
        {
            TypeAdapterConfig<CarritoSesionModel, CarritoSesion>.NewConfig().IgnoreNullValues(true);
            CarritoSesion carrito_sesion = TypeAdapter.Adapt<CarritoSesionModel, CarritoSesion>(carrito_sesion_model);
            return carrito_sesion;
        }
    }
}
