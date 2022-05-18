using LiteDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottleCapWebServer
{
    public class Database
    {
        private readonly ILogger<Database> _logger;

        private const string databasePath = "AnswersDB.db";

        public Database()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Database>();
        }

        public void AddAnswer(AnswerDto newAnswer)
        {
            using (LiteDatabase db = new(databasePath))
            {
                var collection = db.GetCollection<AnswerDto>();

                // Insert new answer document (Id will be auto-incremented)
                collection.Insert(newAnswer);
                //collection.EnsureIndex()
            }
        }

        public List<AnswerDto> GetRandomAnswers(int count)
        {
            using (LiteDatabase db = new(databasePath))
            {
                var collection = db.GetCollection<AnswerDto>();

                List<AnswerDto> results = new List<AnswerDto>();

                var rnd = new Random();
                for(int i = 0; i < count; i++)
                {
                    var offset = rnd.Next(0, collection.Count());
                    var result = collection.Query().Limit(1).Offset(offset).SingleOrDefault();
                    results.Add(result);
                }

                return results;
            }
        }
        
        public List<AnswerDto> GetAnswerByAuthor(string author)
        {
            using (LiteDatabase db = new(databasePath))
            {
                var collection = db.GetCollection<AnswerDto>();

                List<AnswerDto> results = collection.Query().Where(x => x.Author == author).Limit(100).ToList();

                if(results.Count == 100)
                {
                    _logger.LogWarning("Capped answers by author results at 100");
                }

                return results;
            }
        }
    }
}
