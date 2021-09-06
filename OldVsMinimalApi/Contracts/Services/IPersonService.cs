using Contracts.Models;

namespace Contracts.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();

        Person? Get(Guid id);

        Guid AddOrUpdate(Person person);
    }
}