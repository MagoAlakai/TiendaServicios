namespace TiendaServicios.Api.Gateway.Controllers;

[ApiController]
[Route("api/libros")]
[Authorize]
public class LibroController : Controller
{
    private LibrosServices.LibrosServicesClient CreateClient()
    {
        HttpClientHandler http_client_handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12
        };
        http_client_handler.UseUnsignedServerCertificateValidation();

        HttpClient http_client = new(http_client_handler);
        GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.libro:53445", new()
        {
            HttpClient = http_client
        });

        LibrosServices.LibrosServicesClient client = new(channel);

        return client;
    }
    // GET: /get/{LibroId}
    [HttpGet("get/{LibroId}", Name = "GetLibro")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int LibroId)
    {
        GetLibroRequest get_libro_request = new()
        {
            LibroId = LibroId,
        };

        LibrosServices.LibrosServicesClient client = CreateClient();

        GetLibroResponse get_libro_response = await client.GetLibroAsync(get_libro_request);
        return CreatedAtRoute("GetLibro", get_libro_response);
    }

    // GET: api/libros/get/Libros
    [HttpGet("get/libros", Name = "GetAllLibros")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAll()
    {
        GetAllLibrosRequest get_all_libros_request = new()
        {
        };

        LibrosServices.LibrosServicesClient client = CreateClient(); 

        LibroModelListResponse all_libros_response = await client.GetAllLibrosAsync(get_all_libros_request);
        return CreatedAtRoute("GetAllLibros", all_libros_response);
    }

    // POST: api/libros/create
    [HttpPost("create", Name = "CreateLibro")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LibroModel LibroModel)
    {
        AddLibroRequest add_libro_request = new()
        {
            LibroModel = LibroModel,
        };

        LibrosServices.LibrosServicesClient client = CreateClient();

        AddLibroResponse add_libro_response = await client.AddLibroAsync(add_libro_request);
        return CreatedAtRoute("CreateLibro", add_libro_response);
    }

    // POST: api/libroes/update/{LibroId}
    [HttpPost("update/{LibroId}", Name = "UpdateLibro")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(LibroModel LibroModel, int LibroId)
    {
        UpdateLibroRequest update_libro_request = new()
        {
            LibroId = LibroId,
            LibroModel = LibroModel,
        };

        LibrosServices.LibrosServicesClient client = CreateClient();

        UpdateLibroResponse update_libro_response = await client.UpdateLibroAsync(update_libro_request);
        return CreatedAtRoute("UpdateLibro", update_libro_response);
    }

    // POST: api/libroes/delete/{LibroId}
    [HttpPost("delete/{LibroId}", Name = "DeleteLibro")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int LibroId)
    {
        DeleteLibroRequest delete_libro_request = new()
        {
            LibroId = LibroId,
        };

        LibrosServices.LibrosServicesClient client = CreateClient();

        DeleteLibroResponse delete_libro_response = await client.DeleteLibroAsync(delete_libro_request);
        return CreatedAtRoute("DeleteLibro", delete_libro_response);
    }
}
