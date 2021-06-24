namespace Joker.EntityFrameworkCore.OptionsBuilders
{
    public class JokerNpDbContextOptionBuilder : JokerDbContextOptionBuilder
    {
        public bool UseNetTopologySuite { get; set; }
    }
}