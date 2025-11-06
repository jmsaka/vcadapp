using System.Collections.Generic;
using VCadApp.Models;

namespace VCadApp.Repositories
{
    public interface IPersonRepository
    {
        int Insert(Person person);

        List<Person> GetAll();
    }
}