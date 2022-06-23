Console.WriteLine("Server starting...");
Thread.Sleep(1000);

HttpClientHandler http_client_handler = new()
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    SslProtocols = SslProtocols.Tls12
};
http_client_handler.UseUnsignedServerCertificateValidation();

HttpClient http_client = new(http_client_handler);
using GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.libro:53445", new()
{
    HttpClient = http_client
});

LibrosServices.LibrosServicesClient client = new(channel);

// AddLibroAsync
LibroModel add_libro_model = new()
{
    Id = 4,
    Titulo = "Tha clander",
    AutorLibro = "63b9b893-c911-4927-9b44-194141fbf25b",
    LibreriaMaterialGuid = Guid.NewGuid().ToString(),
    FechaPublicacion = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
};

AddLibroRequest add_libro_request = new()
{
    LibroModel = add_libro_model,
};

AddLibroResponse add_libro_response = await client.AddLibroAsync(add_libro_request);
Console.WriteLine($"AddLibroAsync Response Success: {add_libro_response.Success}");
Console.WriteLine($"AddLibroAsync Response Model: {add_libro_response.LibroModel}");

// GetLibroAsync
Console.WriteLine("GetLibroAsync started...");
Thread.Sleep(1000);

GetLibroRequest get_libro_request = new()
{
    LibroId = 4,
};

GetLibroResponse get_libro_response = await client.GetLibroAsync(get_libro_request);
Console.WriteLine($"GetLibroAsync Response Success: {get_libro_response.Success}");
Console.WriteLine($"GetLibroAsync Response Model: {get_libro_response.LibroModel}");

// GetAllLibrosAsync
Console.WriteLine("GetAllLibrosAsync started...");
Thread.Sleep(1000);

GetAllLibrosRequest get_all_libros_request = new();
LibroModelListResponse get_all_libros_response = await client.GetAllLibrosAsync(get_all_libros_request);

Console.WriteLine($"GetAllLibrosAsync Response Success: {get_all_libros_response.Success}");
Console.WriteLine($"GetAllLibrosAsync Response Model List:");

foreach (LibroModel libro_model in get_all_libros_response.LibroModelList)
{
    Console.WriteLine(libro_model);
}

// UpdateLibroAsync
Console.WriteLine("UpdateLibroAsync started...");
Thread.Sleep(1000);

LibroModel update_libro_model = new()
{
    Id = 4,
    Titulo = "Tha jander clander",
    AutorLibro = "63b9b893-c911-4927-9b44-194141fbf25b",
    LibreriaMaterialGuid = Guid.NewGuid().ToString(),
    FechaPublicacion = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
};

UpdateLibroRequest update_libro_request = new()
{
    LibroId = 4,
    LibroModel = update_libro_model
};

UpdateLibroResponse update_libro_response = await client.UpdateLibroAsync(update_libro_request);
Console.WriteLine($"UpdateLibroAsync Response Success: {update_libro_response.Success}");
Console.WriteLine($"UpdateLibroAsync Response Model: {update_libro_response.LibroModel}");

// DeleteLibroAsync
//Console.WriteLine("DeleteLibroAsync started...");
//Thread.Sleep(1000);

//DeleteLibroRequest delete_libro_request = new()
//{
//    LibroId = 4,
//};

//DeleteLibroResponse delete_libro_response = await client.DeleteLibroAsync(delete_libro_request);
//Console.WriteLine($"DeleteLibroAsync Response Success: {update_libro_response.Success}");

Console.ReadLine();
