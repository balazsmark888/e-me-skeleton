namespace e_me.Mvc.Application
{
    public class ApplicationConfiguration
    {
        private static readonly object LockInstanceManager = new();

        private static volatile ApplicationConfiguration _instance;


        public static ApplicationConfiguration Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (LockInstanceManager)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }
                    _instance = new ApplicationConfiguration();
                }

                return _instance;
            }
        }

        public string ConnectionString { get; private set; }

        public string ApplicationKey { get; set; }
    }
}