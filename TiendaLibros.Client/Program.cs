
Console.WriteLine("Server starting...");
Thread.Sleep(500);

HttpClientHandler http_client_handler = new()
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    SslProtocols = SslProtocols.Tls12
};
http_client_handler.UseUnsignedServerCertificateValidation();

HttpClient http_client = new(http_client_handler);
using GrpcChannel channel = GrpcChannel.ForAddress("https://127.0.0.1:53443", new()
{
    HttpClient = http_client
});

AutorServices.AutorServicesClient client = new(channel);

// AddProductAsync
Console.WriteLine("AddProductAsync started...");
Thread.Sleep(2000);

AutorModel autor_model = new()
{
    AutorLibroId = 9,
    Nombre = "Alba",
    Apellido = "Casadella",
    FechaNacimiento = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    AutorLibroGuid = Guid.NewGuid().ToString(),
};

AddAutorRequest add_autor_request = new()
{
    AutorModel = autor_model,
};

AddAutorResponse add_autor_response = await client.AddAutorAsync(add_autor_request);
Console.WriteLine($"AddAutorAsync Response Success: {add_autor_response.Success}");
Console.WriteLine($"AddAutorAsync Response Model: {add_autor_response.AutorModel}");
