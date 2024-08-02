namespace MovieStore.Base;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; }
    public bool IsActive { get; set; }
}