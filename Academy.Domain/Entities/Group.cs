namespace Academy.Domain.Entities;

public class Group : Entity
{
    public required string Name { get; set; }

    public List<Student>? Students { get; set; } = [];

    public List<TeacherGroup>? TeacherGroups { get; set; } = [];
}
