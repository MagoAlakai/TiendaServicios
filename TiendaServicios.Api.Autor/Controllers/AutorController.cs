namespace TiendaServicios.Api.Autor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AutorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data){
        return await _mediator.Send(data);
    }
}
