using Test.Domain.Entities;

namespace Test.Domain.Interfaces;

public interface IOptionService
{
    Task<Option> GetByIdAsync(int id);
    Task<List<Option>> GetByQuestionIdAsync(int questionId);
    Task<Option> CreateAsync(Option option);
    Task<bool> UpdateAsync(Option option);
    Task<bool> DeleteAsync(int id);
}
