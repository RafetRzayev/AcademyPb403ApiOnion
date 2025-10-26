namespace Academy.Domain.Entities;

public class Teacher : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public List<TeacherGroup>? TeacherGroups { get; set; } = [];
}
