namespace VCadApi.Domain.Entities;

public class PersonEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string? MaritalStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}