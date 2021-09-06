using Contracts.Models;

namespace Contracts.Services
{
    public class PersonService : IPersonService
    {
        private readonly Dictionary<Guid, Person> _people;

        public PersonService(Dictionary<Guid, Person>? people = null)
        {
            _people = people ?? new();
        }

        public Guid AddOrUpdate(Person person)
        {
            _people[person.Id] = person;
            return person.Id;
        }

        public Person? Get(Guid id)
        {
            if (_people.TryGetValue(id, out var person))
            {
                return person;
            }

            return null;
        }

        public IEnumerable<Person> GetAll()
        {
            return _people.Values;
        }
    }
}