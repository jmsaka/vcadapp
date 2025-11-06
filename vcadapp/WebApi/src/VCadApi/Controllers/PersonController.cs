namespace VCadApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<ActionResult<BaseResponse<Guid>>> Post(UpsertPersonCommand command)
    {
        return await HandleCommand(command);
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<ICollection<PersonDto>>>> Get([FromQuery] Guid id)
    {
        return await HandleQuery(new GetPersonQuery() { Id = id });
    }
}
