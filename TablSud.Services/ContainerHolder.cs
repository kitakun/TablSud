using Nancy.TinyIoc;

namespace TablSud.Services
{
    /// <summary>
    /// TinyIoc wrapper for static context
    /// </summary>
    public static class ContainerHolder
    {
        private static TinyIoCContainer _contaner;

        /// <summary>
        /// Place nancy container to static context
        /// </summary>
        public static void Place(this TinyIoCContainer input)
        {
            _contaner = input;
        }

        /// <summary>
        /// Use resolve via static context
        /// </summary>
        public static T Resolve<T>() where T : class
        {
            return _contaner.Resolve<T>();
        }
    }
}
