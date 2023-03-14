namespace TestApp.Domain.Models.Common
{
    public class EntityModel
    {
        public EntityModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}
