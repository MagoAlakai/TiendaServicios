namespace TiendaServicios.Api.Gateway.Controllers;

[ApiController]
[Route("api/autores")]
public class AutorController : ControllerBase
{
    private AutorServices.AutorServicesClient CreateClient()
    {
        HttpClientHandler http_client_handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12
        };
        http_client_handler.UseUnsignedServerCertificateValidation();

        HttpClient http_client = new(http_client_handler);
        GrpcChannel channel = GrpcChannel.ForAddress("https://tiendaservicios.api.autor:53443", new()
        {
            HttpClient = http_client
        });

        AutorServices.AutorServicesClient client = new(channel);
        //GradoAcademicoServices.GradoAcademicoServicesClient grado_client = new(channel);

        return client;
    }
    // GET: /get/{AutorId}
    [HttpGet("get/{AutorId}", Name ="GetAutor")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int AutorId)
    {
        GetAutorRequest get_autor_request = new()
        {
            AutorLibroId = AutorId,
        };

        AutorServices.AutorServicesClient client = CreateClient();

        GetAutorResponse get_autor_response = await client.GetAutorAsync(get_autor_request);
        return CreatedAtRoute("GetAutor", get_autor_response);
    }

    // GET: api/autores/get/Autores
    [HttpGet("get/Autores", Name ="GetAllAutors")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAll()
    {
        GetAllAutorsRequest get_all_autors_request = new()
        {
        };

        AutorServices.AutorServicesClient client = CreateClient();

        AutorModelListResponse all_autors_response = await client.GetAllAutorsAsync(get_all_autors_request);
        return CreatedAtRoute("GetAllAutors", all_autors_response);
    }

    // POST: api/autores/create
    [HttpPost("create", Name = "CreateAutor")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AutorModel AutorModel)
    {
        AddAutorRequest add_autor_request = new()
        {
            AutorModel = AutorModel,
        };

        AutorServices.AutorServicesClient client = CreateClient();

        AddAutorResponse add_autor_response = await client.AddAutorAsync(add_autor_request);
        return CreatedAtRoute("CreateAutor", add_autor_response);
    }

    // POST: api/autores/update/{AutorId}
    [HttpPost("update/{AutorId}", Name = "UpdateAutor")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AutorModel AutorModel, int AutorId)
    {
        UpdateAutorRequest update_autor_request = new()
        {
            AutorLibroId = AutorId,
            AutorModel = AutorModel,
        };

        AutorServices.AutorServicesClient client = CreateClient();

        UpdateAutorResponse update_autor_response = await client.UpdateAutorAsync(update_autor_request);
        return CreatedAtRoute("UpdateAutor", update_autor_response);
    }

    // POST: api/autores/delete/{AutorId}
    [HttpPost("delete/{AutorId}", Name = "DeleteAutor")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int AutorId)
    {
        DeleteAutorRequest delete_autor_request = new()
        {
            AutorLibroId = AutorId,
        };

        AutorServices.AutorServicesClient client = CreateClient();

        DeleteAutorResponse delete_autor_response = await client.DeleteAutorAsync(delete_autor_request);
        return CreatedAtRoute("DeleteAutor", delete_autor_response);
    }

    //private string BuildToken(User user)
    //{
    //    var claims = new[] {
    //        new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
    //    };
    //    // privte signing key which is just a string
    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    //    // how are you going to sign the jwt
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
    //    var token = new JwtSecurityToken(
    //      _config["Jwt:Issuer"],
    //      _config["Jwt:Issuer"],
    //      claims,
    //      expires: DateTime.Now.AddMinutes(30),
    //      signingCredentials: creds
    //    );
    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
