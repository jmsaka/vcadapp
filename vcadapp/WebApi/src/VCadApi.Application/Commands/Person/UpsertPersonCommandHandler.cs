namespace VCadApi.Application.Commands.Person;

public class UpsertPersonCommandHandler(IRepository<PersonEntity> repository,
                                        IMapper mapper) : IRequestHandler<UpsertPersonCommand, BaseResponse<Guid>>
{
    private readonly IRepository<PersonEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    private async Task<BaseResponse<Guid>> Insert(PersonEntity person, CancellationToken cancellationToken)
    {
        person.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(person, cancellationToken);
        return new BaseResponse<Guid>(person.Id, true);
    }

    private async Task<BaseResponse<Guid>> Update(PersonEntity person, CancellationToken cancellationToken)
    {
        var existingPerson = await _repository.GetByIdAsync(person.Id, cancellationToken);

        if (existingPerson is null)
        {
            return new BaseResponse<Guid>(Guid.Empty, false, $"Id {person.Id} não encontrado.");
        }

        await _repository.UpdateAsync(person, cancellationToken);
        return new BaseResponse<Guid>(person.Id);
    }

    public async Task<BaseResponse<Guid>> Handle(UpsertPersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var person = _mapper.Map<PersonEntity>(request);
            return request.Id.Equals(Guid.Empty) ? await Insert(person, cancellationToken) : await Update(person, cancellationToken);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Guid>(Guid.Empty, false, ex.Message);
        }
    }
}