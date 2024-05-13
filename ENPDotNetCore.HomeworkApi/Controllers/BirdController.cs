using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ENPDotNetCore.HomeworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private async Task<MainDto> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("Birds.json");
            var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);
            return model;

        }

        [HttpGet("birds")]
        public async Task<IActionResult> Birds()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_Bird);

        }

        [HttpGet("birds/{id}")]
        public async Task<IActionResult> GetBirdById(int id)
        {
            var model = await GetDataAsync();
            var bird = model.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (bird is null)
            {
                return NotFound("No data found");
            }
            return Ok(bird);
        }


    }
    public class MainDto
    {
        public Bird[] Tbl_Bird { get; set; }
    }

    public class Bird
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}



