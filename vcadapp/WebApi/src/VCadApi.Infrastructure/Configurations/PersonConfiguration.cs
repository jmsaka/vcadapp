namespace VCadApi.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.MaritalStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");
    }
}
