using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Persistence;

namespace Test.Infrastructure.Services;

public class OptionService : IOptionService
{
    private readonly AppDbContext _context;

    public OptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Option> GetByIdAsync(int id)
    {
        return await _context.Options
            .Include(o => o.Question)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new KeyNotFoundException("Option not found.");
    }

    public async Task<List<Option>> GetByQuestionIdAsync(int questionId)
    {
        return await _context.Options
            .Where(o => o.QuestionId == questionId)
            .ToListAsync();
    }

    public async Task<Option> CreateAsync(Option option)
    {
        _context.Options.Add(option);
        await _context.SaveChangesAsync();
        return option;
    }

    public async Task<bool> UpdateAsync(Option option)
    {
        if (!_context.Options.Any(o => o.Id == option.Id))
            return false;

        _context.Options.Update(option);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var option = await _context.Options.FindAsync(id);
        if (option == null) return false;

        _context.Options.Remove(option);
        await _context.SaveChangesAsync();
        return true;
    }
}
