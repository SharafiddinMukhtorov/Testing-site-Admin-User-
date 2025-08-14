namespace Test.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = default!;
    public List<Option> Options { get; set; } = new();
}
