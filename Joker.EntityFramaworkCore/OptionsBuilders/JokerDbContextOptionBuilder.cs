using System;

namespace Joker.EntityFramaworkCore.OptionsBuilders
{
    public class JokerDbContextOptionBuilder
    {
        public string ConnectionString { get; set; }
        public bool EnableMigration { get; set; }
        public int MaxRetryCount { get; set; }
        public TimeSpan? MaxRetryDelay { get; set; }
    }
}
