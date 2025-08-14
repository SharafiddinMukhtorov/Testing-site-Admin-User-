using Microsoft.EntityFrameworkCore;
using Test.Domain.DTOs;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Persistence;

namespace Test.Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly AppDbContext _context;
    private readonly IOptionService optionService;

    public QuestionService(AppDbContext context)
    {
        optionService = new OptionService(context);
        _context = context;
    }

    public async Task<Question> GetByIdAsync(int id)
    {
        return await _context.Questions
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == id)
            ?? throw new KeyNotFoundException("Question not found.");
    }

    public async Task<List<Question>> GetAllAsync()
    {
        return await _context.Questions
            .Include(q => q.Options)
            .ToListAsync();
    }

    public async Task<Question> CreateAsync(CreateQuestionDto dto)
    {
        var question = new Question { QuestionText = dto.Text };
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        var questionId = question.Id;

        foreach (var (text, index) in dto.Options.Select((t, i) => (t, i)))
        {
            var option = new Option
            {
                Text = text,
                IsCorrect = index == dto.CorrectIndex,
                QuestionId = question.Id 
            };
            await optionService.CreateAsync(option);
        }

        return question;
    }

    public async Task<bool> UpdateAsync(Question question)
    {
        if (!_context.Questions.Any(q => q.Id == question.Id))
            return false;

        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateOptionAsync(Option option)
    {
        return await optionService.UpdateAsync(option);
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null) return false;

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }
}