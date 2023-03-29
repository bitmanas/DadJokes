namespace ConsumeWebApi.Models
{
    public class JokeViewModel {
        public bool success { get; set; }
        public JokeModel[] body { get; set; }
    }
    public class JokeModel
    {
        public string _id { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string punchline { get; set; }

    }
}
