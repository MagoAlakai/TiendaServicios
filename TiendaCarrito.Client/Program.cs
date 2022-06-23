Console.WriteLine("Server starting...");
Thread.Sleep(1000);

HttpClientHandler http_client_handler = new()
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    SslProtocols = SslProtocols.Tls12
};
http_client_handler.UseUnsignedServerCertificateValidation();

HttpClient http_client = new(http_client_handler);
using GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.libro:53447", new()
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

// GetCarritoSesionAsync
Console.WriteLine("GetCarritoSesionAsync started...");
Thread.Sleep(2000);

GetCarritoSesionRequest get_carrito_request = new()
{
    CarritoSesionId = 1,
};

GetCarritoSesionResponse get_carrito_sesion_response = await client.GetCarritoSesionAsync(get_carrito_request);
Console.WriteLine($"GetCarritoSesionAsync Response Success: {get_carrito_sesion_response.Success}");
Console.WriteLine($"GetCarritoSesionAsync Response Model: {get_carrito_sesion_response.CarritoSesionModel}");

// GetAllCarritosSesionAsync
Console.WriteLine("GetCarritoSesionAsync started...");
Thread.Sleep(2000);

GetAllCarritosSesionRequest get_all_carritos_request = new();
GetAllCarritosSesionResponse get_all_carritos_sesion_response = await client.GetAllCarritosSesionAsync(get_all_carritos_request);

Console.WriteLine($"GetAllCarritosSesionAsync Response Success: {get_carrito_sesion_response.Success}");
foreach (CarritoSesionModel carrito_sesion in get_all_carritos_sesion_response.CarritoSesionModelList)
{
    Console.WriteLine(carrito_sesion.CarritoSesionId);
    Console.WriteLine(carrito_sesion.CarritoSesionDate);
    foreach (CarritoSesionDetalleModel carrito_sesion_detalle in carrito_sesion.ListaDetalle)
    {
        Console.WriteLine(carrito_sesion_detalle.CarritoSesionDetalleId);
        Console.WriteLine(carrito_sesion_detalle.CarritoSesionDetalleDate);
        Console.WriteLine(carrito_sesion_detalle.ProductoSeleccionado);
    }
}

// UpdateCarritoSesionAsync
Console.WriteLine("UpdateCarritoSesionAsync started...");
Thread.Sleep(2000);

CarritoSesionModel update_carrito_model = new()
{
    CarritoSesionId = 1,
    CarritoSesionDate = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
};

update_carrito_model.ListaDetalle.Add(ListaDetalle);

UpdateCarritoSesionRequest update_carrito_request = new()
{
    CarritoSesionId = 1,
    CarritoSesionModel = update_carrito_model
};

UpdateCarritoSesionResponse update_carrito_sesion_response = await client.UpdateCarritoSesionAsync(update_carrito_request);
Console.WriteLine($"UpdateCarritoSesionAsync Response Success: {update_carrito_sesion_response.Success}");
Console.WriteLine($"UpdateCarritoSesionAsync Response Model: {update_carrito_sesion_response.CarritoSesionModel}");

// DeleteCarritoSesionAsync
//Console.WriteLine("DeleteCarritoSesionAsync started...");
//Thread.Sleep(2000);

//DeleteCarritoSesionRequest delete_carrito_request = new()
//{
//    CarritoSesionId = 3,
//};

//DeleteCarritoSesionResponse delete_carrito_sesion_response = await client.DeleteCarritoSesionAsync(delete_carrito_request);
//Console.WriteLine($"DeleteCarritoSesionAsync Response Success: {delete_carrito_sesion_response.Success}");

Console.ReadKey();