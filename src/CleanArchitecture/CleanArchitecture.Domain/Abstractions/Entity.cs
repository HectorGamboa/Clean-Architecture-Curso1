namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {}
        private readonly List<IDomainEvent> _domainEvents = new();
        protected Entity(Guid id)
        {
            Id = id;
        }
       
        public Guid Id { get; init; }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents(){
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents(){
         _domainEvents.Clear();   
        }

        protected void RaiseDomainEvent(IDomainEvent domain)
        {
            _domainEvents.Add(domain);
        }

    }
}