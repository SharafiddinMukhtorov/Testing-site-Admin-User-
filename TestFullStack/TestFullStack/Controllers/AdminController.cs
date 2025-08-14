using Microsoft.AspNetCore.Mvc;
using Test.Domain.Interfaces;

namespace TestFullStack.Controllers;

public class AdminController : Controller
{
    private readonly IQuestionService _questionService;

    public AdminController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    public async Task<IActionResult> Index()
    {
        var questions = await _questionService.GetAllAsync();
        return View(questions);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string questionText, List<string> options, int correctIndex)
    {
        if (string.IsNullOrWhiteSpace(questionText) || options == null || options.Count == 0)
        {
            ViewBag.Error = "Please fill all fields.";
            return View();
        }

        var dto = new Test.Domain.DTOs.CreateQuestionDto(questionText, options, correctIndex - 1);
        await _questionService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var question = await _questionService.GetByIdAsync(id);
        if (question == null) return NotFound();
        return View(question);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string questionText, List<string> options, int correctIndex)
    {
        if (string.IsNullOrWhiteSpace(questionText) || options == null || options.Count != 3)
        {
            ViewBag.Error = "Iltimos, barcha maydonlarni to‘ldiring va 3 ta javob kiriting.";
            var question = await _questionService.GetByIdAsync(id);
            return View(question);
        }

        if (correctIndex < 0 || correctIndex > 3)
        {
            ViewBag.Error = "To‘g‘ri javobni tanlang (0-3).";
            var question = await _questionService.GetByIdAsync(id);
            return View(question);
        }

        var questionToUpdate = await _questionService.GetByIdAsync(id);
        if (questionToUpdate == null) return NotFound();

        questionToUpdate.QuestionText = questionText;

        for (int i = 0; i < questionToUpdate.Options.Count; i++)
        {
            if (i < options.Count)
            {
                questionToUpdate.Options[i].Text = options[i];
                questionToUpdate.Options[i].IsCorrect = (i == correctIndex);

                await _questionService.UpdateOptionAsync(questionToUpdate.Options[i]);
            }
        }

        await _questionService.UpdateAsync(questionToUpdate);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _questionService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
