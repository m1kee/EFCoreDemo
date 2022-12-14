namespace EFCoreDemo.Database;

public interface IBaseEntity {
    int Id { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
}