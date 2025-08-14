namespace Test.Domain.DTOs;

public record CreateQuestionDto(string Text, List<string> Options, int CorrectIndex);
