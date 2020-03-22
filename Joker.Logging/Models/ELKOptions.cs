namespace Joker.Logging.Models
{
    public class ElkOptions : LoggerOptions
    {
        public string Url { get; set; }
        public bool BasicAuthEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IndexFormat { get; set; }
    }
}
