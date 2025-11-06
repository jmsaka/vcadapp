namespace VCadApi.Application.Commands.Person;

public class UpsertPersonCommand : IRequest<BaseResponse<Guid>>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Email { get; set; }
    public required string MaritalStatus { get; set; }
}
