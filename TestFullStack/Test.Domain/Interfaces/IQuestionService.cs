using Test.Domain.DTOs;
using Test.Domain.Entities;

namespace Test.Domain.Interfaces;

public interface IQuestionService
{
    Task<Question> GetByIdAsync(int id);
    Task<List<Question>> GetAllAsync();
    Task<Question> CreateAsync(CreateQuestionDto dto);
    Task<bool> UpdateAsync(Question question);
    Task<bool> UpdateOptionAsync(Option option);
    Task<bool> DeleteAsync(int id);
}
