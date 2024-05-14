using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Quic;

namespace ENPDotNetCore.HomeworkApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PickAPileController : ControllerBase
{
    private async Task<PickAPile> GetDataAsync()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("PickAPile.json");
        var item = JsonConvert.DeserializeObject<PickAPile>(jsonStr);
        return item;
    }

    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var model = await GetDataAsync();
        return Ok(model.Questions);
    }

    [HttpGet("questions/{id}")]
    public async Task<IActionResult> GetQuestionById(int id)
    {
        var model = await GetDataAsync();
        var question = model.Questions.FirstOrDefault(x => x.QuestionId == id);
        if (question is null)
        {
            return NotFound("No data Found");
        }
        return Ok(question);
    }

    [HttpGet("{questionNo}/{answerNo}")]
    public async Task<IActionResult> GetResult(int questionNo, int answerNo)
    {
        var model = await GetDataAsync();
        var answer = model.Answers.FirstOrDefault(x => x.QuestionId == questionNo && x.AnswerId == answerNo);
        if (answer is null)
        { 
            return NotFound("No data Found");
        }
        return Ok(answer);

    }
}

public class PickAPile
{
    public Question[] Questions { get; set; }
    public Answer[] Answers { get; set; }
}

public class Question
{
    public int QuestionId { get; set; }
    public string QuestionName { get; set; }
    public string QuestionDesp { get; set; }
}

public class Answer
{
    public int AnswerId { get; set; }
    public string AnswerImageUrl { get; set; }
    public string AnswerName { get; set; }
    public string AnswerDesp { get; set; }
    public int QuestionId { get; set; }
}
