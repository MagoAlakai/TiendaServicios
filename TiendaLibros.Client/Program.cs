Console.WriteLine("Server starting...");
Thread.Sleep(1000);

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
GradoAcademicoServices.GradoAcademicoServicesClient grado_client = new(channel);

//// AddAutorAsync - AddGradoAcademicoSync
//Console.WriteLine("AddAutorAsync started...");
//Thread.Sleep(2000);

//// AddGradoAcademicoSync
//GradoAcademicoModel grado_model = new()
//{
//    GradoAcademicoId = 5,
//    Nombre = "Escoles Píes",
//    CentroAcademico = "Rosselló 200",
//    FechaGrado = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
//    AutorLibroId = 5,
//    GradoAcademicoGuid = Guid.NewGuid().ToString(),
//};

//AddGradoAcademicoRequest add_grado_request = new()
//{
//    GradoAcademicoModel = grado_model,
//};

//AddGradoAcademicoResponse add_grado_response = await grado_client.AddGradoAcademicoAsync(add_grado_request);
//Console.WriteLine($"AddGradoAcademicoAsync Response Success: {add_grado_response.Success}");
//Console.WriteLine($"AddGradoAcademicoAsync Response Model: {add_grado_response.GradoAcademicoModel}");

// AddAutorAsync
AutorModel add_autor_model = new()
{
    AutorLibroId = 1,
    Nombre = "Alba",
    Apellido = "Casadellà",
    GradoAcademicoGuid = "2f95fe65-4d07-41a4-94ee-8ca3c022f644",
    FechaNacimiento = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    AutorLibroGuid = "2f9af589-5ebf-405c-8524-50c0b2e67f08",
};

AddAutorRequest add_autor_request = new()
{
    AutorModel = add_autor_model,
};

AddAutorResponse add_autor_response = await client.AddAutorAsync(add_autor_request);
Console.WriteLine($"AddAutorAsync Response Success: {add_autor_response.Success}");
Console.WriteLine($"AddAutorAsync Response Model: {add_autor_response.AutorModel}");

// GetAutorAsync
Console.WriteLine("GetAutorAsync started...");
Thread.Sleep(1000);

GetAutorRequest get_autor_request = new()
{
    AutorLibroId = 5,
};

GetAutorResponse get_autor_response = await client.GetAutorAsync(get_autor_request);
Console.WriteLine($"GetAutorAsync Response Success: {get_autor_response.Success}");
Console.WriteLine($"GetAutorAsync Response Model: {get_autor_response.AutorModel}");


// GetAllAutorsAsync
Console.WriteLine("GetAllAutorsAsync started...");
Thread.Sleep(1000);

GetAllAutorsRequest get_all_autors_request = new();
AutorModelListResponse get_all_autors_response = await client.GetAllAutorsAsync(get_all_autors_request);

Console.WriteLine($"GetAllAutorsAsync Response Success: {get_all_autors_response.Success}");
Console.WriteLine($"GetAllAutorsAsync Response Model List:");

foreach (AutorModel autor_model in get_all_autors_response.AutorModelList)
{
    Console.WriteLine(autor_model);
}

// UpdateAutorAsync
Console.WriteLine("UpdateAutorAsync started...");
Thread.Sleep(1000);

AutorModel update_autor_model = new()
{
    AutorLibroId = 5,
    Nombre = "Idurre",
    Apellido = "Arrazola",
    GradoAcademicoGuid = "ffcaf377-c7e9-4f10-9cec-af7abb6a15fc",
    FechaNacimiento = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
    AutorLibroGuid = "60e6a601-e65a-41a1-a1e7-9085711a9302",
};

UpdateAutorRequest update_autor_request = new()
{
    AutorLibroId = 5,
    AutorModel = update_autor_model
};

UpdateAutorResponse update_autor_response = await client.UpdateAutorAsync(update_autor_request);
Console.WriteLine($"UpdateAutorAsync Response Success: {update_autor_response.Success}");
Console.WriteLine($"UpdateAutorAsync Response Model: {update_autor_response.AutorModel}");

// DeleteAutorAsync
Console.WriteLine("DeleteAutorAsync started...");
Thread.Sleep(1000);

DeleteAutorRequest delete_autor_request = new()
{
    AutorLibroId = 1,
};

DeleteAutorResponse delete_autor_response = await client.DeleteAutorAsync(delete_autor_request);
Console.WriteLine($"DeleteAutorAsync Response Success: {update_autor_response.Success}");


Console.ReadKey();
