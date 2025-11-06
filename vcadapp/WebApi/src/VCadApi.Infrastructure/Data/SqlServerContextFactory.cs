namespace VCadApi.Infrastructure.Data;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<PersonDbContext>
{
    public PersonDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=localhost\\SQLEXPRESS; Database=VcadApi; User Id=sa; Password=SIMERP; TrustServerCertificate=True;";

        Console.WriteLine("CreateDbContext: " + connectionString);

        var optionsBuilder = new DbContextOptionsBuilder<PersonDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new PersonDbContext(optionsBuilder.Options);
    }
}