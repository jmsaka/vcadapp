namespace VCadApi.Application.Queries.Person;

public class GetPersonQueryHandler(IRepository<PersonEntity> repository,
                                   IMapper mapper) : IRequestHandler<GetPersonQuery, BaseResponse<ICollection<PersonDto>>>
{
    private readonly IRepository<PersonEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<ICollection<PersonDto>>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var person = _mapper.Map<ICollection<PersonDto>>(await _repository.GetAllAsync(cancellationToken));

            if (person == null)
            {
                return new BaseResponse<ICollection<PersonDto>>(null, false, $"Dados não encontrados.");
            }

            return new BaseResponse<ICollection<PersonDto>>(person);
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<PersonDto>>(null, false, ex.Message);
        }
    }
}