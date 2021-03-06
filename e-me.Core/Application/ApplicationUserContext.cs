namespace e_me.Core.Application
{
    public class ApplicationUserContext
    {
        public string CurrentUserName { get; set; }

        public string CurrentSessionId { get; set; }

        public string ApplicationKey { get; set; }

        public string ConnectionString { get; set; }

        public string SslTcpServerCertificatePath { get; set; }

        public int SslTcpServerPort { get; set; }
    }
}
