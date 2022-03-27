using Microsoft.Extensions.Configuration;

namespace Joker.Logging.Models;

public class ElkOptions : LoggerOptions
{
    public string Url { get; set; }
    public IConfiguration Configuration { get; set; }
    public string EnvironmentName { get; set; }
    public bool BasicAuthEnabled { get; set; } = false;
    public string Username { get; set; }
    public string Password { get; set; }
    public string IndexFormat { get; set; }
}