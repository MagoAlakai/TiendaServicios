using TiendaServicios.Contracts.Protos.Dto.Carrito.Request;
using TiendaServicios.Contracts.Protos.Dto.Carrito.Response;

namespace TiendaServicios.Api.Gateway.Controllers;

[ApiController]
[Route("api/carrito")]
[Authorize]
public class CarritoController : Controller
{
    private CarritoSesionServices.CarritoSesionServicesClient CreateClient()
    {
        HttpClientHandler http_client_handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12
        };
        http_client_handler.UseUnsignedServerCertificateValidation();

        HttpClient http_client = new(http_client_handler);
        GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.carrito:53447", new()
        {
            HttpClient = http_client
        });

        CarritoSesionServices.CarritoSesionServicesClient client = new(channel);

        return client;
    }
    // GET: /get/{CarritoSesionId}
    [HttpGet("get/{CarritoSesionId}", Name = "GetCarritoSesion")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int CarritoSesionId)
    {
        GetCarritoSesionRequest get_carrito_sesion_request = new()
        {
            CarritoSesionId = CarritoSesionId,
        };

        CarritoSesionServices.CarritoSesionServicesClient client = CreateClient();

        GetCarritoSesionResponse get_carrito_sesion_response = await client.GetCarritoSesionAsync(get_carrito_sesion_request);
        return CreatedAtRoute("GetCarritoSesion", get_carrito_sesion_response);
    }

    // GET: api/carrito/get/caritos
    [HttpGet("get/carritos", Name = "GetAllCarritoSesions")]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAll()
    {
        GetAllCarritosSesionRequest get_all_carrito_sesions_request = new()
        {
        };

        CarritoSesionServices.CarritoSesionServicesClient client = CreateClient();

        GetAllCarritosSesionResponse all_carrito_sesions_response = await client.GetAllCarritosSesionAsync(get_all_carrito_sesions_request);
        return CreatedAtRoute("GetAllCarritoSesions", all_carrito_sesions_response);
    }

    // POST: api/carrito/create
    public class CreateParameters
    {
        public CarritoSesionModel? CarritoSesionModel { get; set; }
        public CarritoSesionDetalleModel? CarritoSesionDetalleModel { get; set; }
    }
    [HttpPost("create", Name = "CreateCarritoSesion")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateParameters parameters)
    {
        parameters.CarritoSesionModel?.ListaDetalle.Add(parameters.CarritoSesionDetalleModel);

        AddCarritoSesionRequest add_carrito_sesion_request = new()
        {
            CarritoSesionModel = parameters.CarritoSesionModel,
        };

        CarritoSesionServices.CarritoSesionServicesClient client = CreateClient();

        AddCarritoSesionResponse add_carrito_sesion_response = await client.AddCarritoSesionAsync(add_carrito_sesion_request);
        return CreatedAtRoute("CreateCarritoSesion", add_carrito_sesion_response);
    }

    // POST: api/carrito/update/{CarritoSesionId}
    [HttpPost("update/{CarritoSesionId}", Name = "UpdateCarritoSesion")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(CarritoSesionModel CarritoSesionModel, int CarritoSesionId)
    {
        UpdateCarritoSesionRequest update_carrito_sesion_request = new()
        {
            CarritoSesionId = CarritoSesionId,
            CarritoSesionModel = CarritoSesionModel,
        };

        CarritoSesionServices.CarritoSesionServicesClient client = CreateClient();

        UpdateCarritoSesionResponse update_carrito_sesion_response = await client.UpdateCarritoSesionAsync(update_carrito_sesion_request);
        return CreatedAtRoute("UpdateCarritoSesion", update_carrito_sesion_response);
    }

    // POST: api/carrito/delete/{CarritoSesionId}
    [HttpPost("delete/{CarritoSesionId}", Name = "DeleteCarritoSesion")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int CarritoSesionId)
    {
        DeleteCarritoSesionRequest delete_carrito_sesion_request = new()
        {
            CarritoSesionId = CarritoSesionId,
        };

        CarritoSesionServices.CarritoSesionServicesClient client = CreateClient();

        DeleteCarritoSesionResponse delete_carrito_sesion_response = await client.DeleteCarritoSesionAsync(delete_carrito_sesion_request);
        return CreatedAtRoute("DeleteCarritoSesion", delete_carrito_sesion_response);
    }
}
