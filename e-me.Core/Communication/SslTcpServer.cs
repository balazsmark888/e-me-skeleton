using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using e_me.Core.Application;

namespace e_me.Core.Communication
{
    public class SslTcpServer
    {
        private readonly X509Certificate _serverCertificate;

        private readonly TcpListener _listener;

        public SslTcpServer(ApplicationUserContext applicationUserContext)
        {
            _serverCertificate = X509Certificate.CreateFromCertFile(applicationUserContext.SslTcpServerCertificatePath);
            _listener = new TcpListener(IPAddress.Any, applicationUserContext.SslTcpServerPort);
        }

        public void RunServer()
        {
            _listener.Start();
        }
    }
}
