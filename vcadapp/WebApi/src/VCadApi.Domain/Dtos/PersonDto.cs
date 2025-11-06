namespace VCadApi.Domain.Dtos;

public class PersonDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? MaritalStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
}