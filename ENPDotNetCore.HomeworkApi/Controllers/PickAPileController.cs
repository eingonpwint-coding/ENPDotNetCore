using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        return Ok(model.Questions.FirstOrDefault(x => x.QuestionId == id));
    }

    [HttpGet("{questionNo}/{answerNo}")]
    public async Task<IActionResult> GetResult(int questionNo, int answerNo)
    {
        var model = await GetDataAsync();
        return Ok(model.Answers.FirstOrDefault(x => x.QuestionId == questionNo && x.AnswerId == answerNo));

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
