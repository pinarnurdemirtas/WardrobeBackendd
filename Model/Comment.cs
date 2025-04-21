namespace WardrobeBackendd.Model;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CombineId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}