namespace Contracts.Models
{
    public record Person(string Name, int Age)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}