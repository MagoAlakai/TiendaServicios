namespace TiendaServicios.Api.Carrito.Mapping;

public static class CarritoSesionModelStaticMappingServer
{
    public static CarritoSesionModel MapToCarritoSesionModel(this CarritoSesion carrito_sesion)
    {
        TypeAdapterConfig<CarritoSesion, CarritoSesionModel>.NewConfig().IgnoreNullValues(true);
        CarritoSesionModel carrito_sesion_model = TypeAdapter.Adapt<CarritoSesion, CarritoSesionModel>(carrito_sesion);
        return carrito_sesion_model;
    }
}
