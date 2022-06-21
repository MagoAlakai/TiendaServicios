namespace TiendaServicios.Api.Carrito.Mapping;
public static class AddCarritoSesionStaticMappingFromRequest
{
    public static CarritoSesion MapToAddCarritoSesionModelFromRequest(this CarritoSesionModel carrito_sesion_model)
    {
        TypeAdapterConfig<CarritoSesionModel, CarritoSesion>.NewConfig().IgnoreNullValues(true)
            .Map(x => x.CarritoSesionId, src => src.CarritoSesionId)
            .Map(x => x.CarritoSesionDate, src => src.CarritoSesionDate.ToDateTime().ToUniversalTime())
            .Map(x => x.ListaDetalle, src => src.ListaDetalle.Select(y => new CarritoSesionDetalle()
             {
                 CarritoSesionDetalleDate = y.CarritoSesionDetalleDate.ToDateTime().ToUniversalTime(),
                 CarritoSesionDetalleId = y.CarritoSesionDetalleId,
                 CarritoSesionId = y.CarritoSesionId,
                 ProductoSeleccionado = y.ProductoSeleccionado
             }));

        //.AfterMapping((src, x) => src.ListaDetalle.Select(y => new CarritoSesionDetalleModel()
        //{
        //    CarritoSesionDetalleDate = y.CarritoSesionDetalleDate,
        //    CarritoSesionDetalleId = y.CarritoSesionDetalleId,
        //    CarritoSesionId = y.CarritoSesionDetalleId,
        //    ProductoSeleccionado = y.ProductoSeleccionado
        //}));

        DefaultStaticMappingRules.SetDefaultMappings();
        CarritoSesion carrito_sesion = TypeAdapter.Adapt<CarritoSesionModel, CarritoSesion>(carrito_sesion_model);
        return carrito_sesion;
    }
}
