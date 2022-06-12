
Console.WriteLine("Server starting...");
Thread.Sleep(500);

HttpClientHandler http_client_handler = new()
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    SslProtocols = SslProtocols.Tls12
};
http_client_handler.UseUnsignedServerCertificateValidation();

HttpClient http_client = new(http_client_handler);
using GrpcChannel channel = GrpcChannel.ForAddress("https://tienda-svc:53443", new()
{
    HttpClient = http_client
});

AutorServices.AutorServicesClient client = new(channel);

// AddProductAsync
Console.WriteLine("AddProductAsync started...");
Thread.Sleep(2000);

GradoAcademicoModel grado_model = new()
{
    GradoAcademicoId = 2,
    Nombre = "Escoles Píes",
    CentroAcademico = "Rosselló 200",
    FechaGrado = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    AutorLibroId = 5,
    GradoAcademicoGuid = Guid.NewGuid().ToString(),
};

List<GradoAcademicoModel> ListaGradoAcademico = new();
ListaGradoAcademico.Add(grado_model);

AutorModel autor_model = new()
{
    AutorLibroId = 3,
    Nombre = "Roser",
    Apellido = "Ros",
    GradoAcademicoGuid = grado_model.GradoAcademicoGuid,
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

Console.ReadKey();
