using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerApi.DummyData;

namespace SwaggerApi.Controllers
{
    [Route("random/joke")]
    [ApiController]
    public class RandomJokeController : ControllerBase
    {
        

        [HttpGet]
        public IActionResult GetWithCount(int? count = 1)
        {
            var data = new DummyDataForJoke().GetData(); //from service layer
            var validCount = ValidateMinMaxCount(count); //from validation logic  
            return Ok(data.Take(validCount).ToList());
        }

        private int ValidateMinMaxCount(int? count)
        {
            int.TryParse(count.ToString(), out var _count);

            if (_count <= 0)
            {
                _count = 1;
            }
            if (_count > 5)
            {
                _count = 5;
            }

            return _count;
        }
    }

    public class DummyDataForJoke
    {
        public List<RandomJoke> GetData()
        {
            List<JokeType> jokeTypes = new List<JokeType>();
            jokeTypes.Add(JokeType.programming);
            jokeTypes.Add(JokeType.issuing);

            List<RandomJoke> list = new List<RandomJoke>();

            var length = 10;
            for (int i = 0; i < length; i++)
            {
                var jktypeIndex = new Random().Next(0, jokeTypes.Count);
                list.Add(new RandomJoke
                {
                    _id = Guid.NewGuid(),
                    type =  jokeTypes[jktypeIndex].ToString(),
                    setup = "setup_" + Guid.NewGuid().ToString(),
                    punchline = "punchline_" + Guid.NewGuid().ToString()
                });
            }

            return list;
        }

        public RandomJoke GetDataSingle()
        {
            return GetData().First();
        }
    }

    public enum JokeType
    {
        programming = 0,
        issuing = 1
    }

}
