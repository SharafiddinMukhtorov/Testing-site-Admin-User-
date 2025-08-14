namespace Test.Domain.DTOs;
public record QuestionDto(
    int Id,
    string Text,
    List<string> Options,
    int CorrectIndex
);
