namespace VCadApi.Application.Profiles.Person;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<UpsertPersonCommand, PersonEntity>().ReverseMap();
        CreateMap<PersonDto, PersonEntity>().ReverseMap();
    }
}