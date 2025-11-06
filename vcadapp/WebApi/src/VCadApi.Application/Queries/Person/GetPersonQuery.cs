namespace VCadApi.Application.Queries.Person;

public class GetPersonQuery : IRequest<BaseResponse<ICollection<PersonDto>>>
{
    public Guid Id { get; set; }
}
