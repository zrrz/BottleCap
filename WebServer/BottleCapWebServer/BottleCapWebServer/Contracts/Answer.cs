using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BottleCapWebServer
{
    [JsonObject, Serializable]
    public class AnswerDto
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject, Serializable]
    public class AnswerList
    {
        public List<AnswerDto> answers;
    }
}
