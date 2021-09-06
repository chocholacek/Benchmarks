using Contracts.Models;

namespace Contracts.TestData
{
    public static class PersonServiceTestData
    {
        public static Guid TestPersonGuid { get; } = new Guid("b5043039-07f3-491c-b9d4-7208412eb78b");

        public static Dictionary<Guid, Person> TestDb { get; } = new()
        {
            { TestPersonGuid, new ("Marek", 25) { Id = TestPersonGuid } }
        };
    }
}