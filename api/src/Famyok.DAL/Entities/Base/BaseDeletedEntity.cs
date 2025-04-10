using Famyok.DAL.Abstraction.Entity;

namespace Famyok.DAL.Entities.Base;

public abstract class BaseDeletedEntity : BaseEntity, IDeletable
{
    public DateTime DateDeleted { get; set; }
}