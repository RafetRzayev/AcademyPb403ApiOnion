namespace Academy.Domain.Entities;

public class TeacherGroup : Entity
{
    public Teacher? Teacher { get; set; }
    public int TeacherId { get; set; }

    public Group? Group { get; set; }
    public int GroupId { get; set; }
}
