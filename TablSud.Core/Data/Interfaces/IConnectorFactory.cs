namespace TablSud.Core.Data.Interfaces
{
    public interface IConnectorFactory<T> where T : new()
    {
        /// <summary>
        /// Get connection object to db
        /// </summary>
        T GetConnection();
    }
}
