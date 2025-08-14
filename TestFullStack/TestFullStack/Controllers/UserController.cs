using Microsoft.AspNetCore.Mvc;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Services;

namespace TestFullStack.Controllers;

public class UserController : Controller
{
    private readonly IQuestionService _questionService;
    private readonly IOptionService _optionService;

    public UserController(IQuestionService questionService, IOptionService optionService)
    {
        _questionService = questionService;
        _optionService = optionService;
    }

    public async Task<IActionResult> Index()
    {
        var allQuestions = await _questionService.GetAllAsync();
        var questions = allQuestions
                            .OrderBy(x => Guid.NewGuid())
                            .Take(10)
                            .ToList();

        foreach (var q in questions)
        {
            q.Options = await _optionService.GetByQuestionIdAsync(q.Id);
        }

        return View(questions);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Dictionary<int, int> answers)
    {
        int correctCount = 0;

        var resultData = new Dictionary<Question, Option>();

        foreach (var kv in answers)
        {
            var question = await _questionService.GetByIdAsync(kv.Key);
            var selectedOption = question.Options.FirstOrDefault(o => o.Id == kv.Value);

            if (selectedOption != null && selectedOption.IsCorrect)
                correctCount++;

            resultData.Add(question, selectedOption);
        }

        ViewBag.Score = correctCount;
        ViewBag.Total = answers.Count;

        return View("Result", resultData);
    }
}
