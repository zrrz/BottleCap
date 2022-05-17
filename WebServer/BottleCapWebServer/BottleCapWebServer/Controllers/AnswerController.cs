using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BottleCapWebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        public static AnswerList Answers = new AnswerList
        {
            answers = new List<AnswerDto>
            {
                new AnswerDto
                {
                    Prompt = "Do you work?",
                    Answer = "Yes I do",
                    Author = "zrrz"
                }
            }
        };

        private readonly ILogger<AnswerController> _logger;

        public AnswerController(ILogger<AnswerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
            return data;
        }

        [HttpGet]
        [Route("Answer/GetRandom")]
        public string GetRandom()
        {
            //TODO dont get message u are the author of
            var random = new System.Random();
            int index = random.Next(Answers.answers.Count);
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
            return data;
        }

        [HttpGet]
        public string GetMessagesFromAuthor(string author)
        {
            //TODO dont get message u are the author of
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
            return data;
        }


        [HttpPost]
        public string Post([FromBody] AnswerDto newAnswer)
        {
            if(newAnswer == null)
            {
                _logger.LogError("newAnswer is null");
                return "";
            }
            Answers.answers.Add(newAnswer);
            _logger.LogInformation("Adding: " + newAnswer);
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(Answers);
            return data;
        }
    }
}
