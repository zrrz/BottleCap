using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BottleCapWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        //public static AnswerList Answers = new AnswerList
        //{
        //    answers = new List<AnswerDto>
        //    {
        //        new AnswerDto
        //        {
        //            Prompt = "Do you work?",
        //            Answer = "Yes I do",
        //            Author = "zrrz"
        //        }
        //    }
        //};

        private readonly ILogger<AnswerController> _logger;

        private Database database;

        public AnswerController(ILogger<AnswerController> logger)
        {
            _logger = logger;
            database = new Database();
        }

        [HttpGet]
        public IActionResult Get()
        {
            //var al = new AnswerList
            //{
            //    answers = new List<AnswerDto>
            //{
            //    new AnswerDto
            //    {
            //        Prompt = "Do you work?",
            //        Answer = "Yes I do",
            //        Author = "zrrz"
            //    }
            //}
            //};
            var answers = database.GetRandomAnswers(10);
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(answers);
            //_logger.LogInformation(data);
            return Ok(data);
        }

        //[HttpGet]
        //[Route("Answer/GetRandom")]
        //public string GetRandom()
        //{
        //    //TODO dont get message u are the author of
        //    var random = new System.Random();
        //    int index = random.Next(Answers.answers.Count);
        //    string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
        //    return data;
        //}

        [HttpGet]
        [Route("answer/author")]
        public string GetMessagesFromAuthor(string author)
        {
            //TODO dont get message u are the author of
            var answers = database.GetAnswerByAuthor(author);
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(answers);
            return data;
        }


        [HttpPost]
        public IActionResult Post([FromBody] AnswerDto newAnswer)
        {
            if(newAnswer == null)
            {
                _logger.LogError("newAnswer is null");
                //return "";
            }
            database.AddAnswer(newAnswer);
            _logger.LogInformation("Adding: " + newAnswer);
            return Ok();
            //string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
            //return data;
        }
    }
}
