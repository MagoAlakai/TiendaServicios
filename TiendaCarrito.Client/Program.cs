Console.WriteLine("Server starting...");
Thread.Sleep(1000);

HttpClientHandler http_client_handler = new()
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    SslProtocols = SslProtocols.Tls12
};
http_client_handler.UseUnsignedServerCertificateValidation();

HttpClient http_client = new(http_client_handler);
using GrpcChannel channel = GrpcChannel.ForAddress("https://carrito-svc:53447", new()
{
    HttpClient = http_client
});

CarritoSesionServices.CarritoSesionServicesClient client = new(channel);

// AddCarritoSesionAsync
Console.WriteLine("AddCarritoSesionAsync started...");
Thread.Sleep(2000);

CarritoSesionModel add_carrito_sesion_model = new()
{
    CarritoSesionId = 1,
    CarritoSesionDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
};

// AddCarritoSesionDetalleASync
CarritoSesionDetalleModel carrito_sesion_detalle_model1 = new()
{
    CarritoSesionDetalleId = 1,
    ProductoSeleccionado = Guid.NewGuid().ToString(),
    CarritoSesionDetalleDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    CarritoSesionId = 1,
};

CarritoSesionDetalleModel carrito_sesion_detalle_model2 = new()
{
    CarritoSesionDetalleId = 2,
    ProductoSeleccionado = Guid.NewGuid().ToString(),
    CarritoSesionDetalleDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    CarritoSesionId = 1,
};

CarritoSesionDetalleModel carrito_sesion_detalle_model3 = new()
{
    CarritoSesionDetalleId = 3,
    ProductoSeleccionado = Guid.NewGuid().ToString(),
    CarritoSesionDetalleDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    CarritoSesionId = 1,
};

Google.Protobuf.Collections.RepeatedField<CarritoSesionDetalleModel> ListaDetalle = new();
ListaDetalle.Add(carrito_sesion_detalle_model1);
ListaDetalle.Add(carrito_sesion_detalle_model2);
ListaDetalle.Add(carrito_sesion_detalle_model3);
add_carrito_sesion_model.ListaDetalle.Add(ListaDetalle);

AddCarritoSesionRequest add_carrito_request = new()
{
    CarritoSesionModel = add_carrito_sesion_model,
};

AddCarritoSesionResponse add_carrito_sesion_response = await client.AddCarritoSesionAsync(add_carrito_request);
Console.WriteLine($"AddCarritoSesionAsync Response Success: {add_carrito_sesion_response.Success}");
Console.WriteLine($"AddCarritoSesionAsync Response Model: {add_carrito_sesion_response.CarritoSesionModel}");

Console.ReadKey();