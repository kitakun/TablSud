namespace TablSud.Core.Configuration
{
    /// <summary>
    /// Service for getting custom settings
    /// </summary>
    public interface IDbConfigurator
    {
        /// <summary>
        /// Get db connection setting model
        /// </summary>
        /// <returns></returns>
        DbConnectionLink GetConfiguration();
    }
}
