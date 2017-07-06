namespace TablSud.Core.Configuration
{
    /// <summary>
    /// Db settings model
    /// </summary>
    public class DbConnectionLink
    {
        /// <summary>
        /// Url link to mongodb instance
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// Name of db instance (if don't exist)
        /// </summary>
        public string DatabaseName { get; set; }

        public static DbConnectionLink Empty => new DbConnectionLink()
        {
            DatabaseName = "TablSudTemp",
            ServerUrl = "mongodb://localhost:27017"
        };
    }
}
