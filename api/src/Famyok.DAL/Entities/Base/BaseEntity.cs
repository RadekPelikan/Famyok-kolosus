namespace Famyok.DAL.Entities.Base;

public abstract class BaseEntity {
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateLastModified { get; set; }
}