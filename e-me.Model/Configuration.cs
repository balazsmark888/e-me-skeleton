using System;
using System.Globalization;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using e_me.Model.Repositories;

namespace e_me.Model
{
    public class Configuration : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserContext _appUserContext;
        private readonly IApplicationSettingRepository _applicationSettingRepository;

        public Configuration(ApplicationDbContext context, ApplicationUserContext appUserContext, IApplicationSettingRepository applicationSettingRepository)
        {
            _context = context;
            _appUserContext = appUserContext;
            _applicationSettingRepository = applicationSettingRepository;
        }

        public ApplicationSetting SessionTimeOut
        {
            get => _applicationSettingRepository.GetSettingByElement("SessionTimeOut");

            set => _applicationSettingRepository.Add(value);
        }

        public int SessionTimeOutProperty
        {
            get => SessionTimeOut == null || string.IsNullOrWhiteSpace(SessionTimeOut.Value) ? 1440 : Convert.ToInt32(SessionTimeOut.Value);

            set
            {
                var sessionTimeOut = SessionTimeOut;
                if (sessionTimeOut != null)
                {
                    sessionTimeOut.Value = value.ToString(CultureInfo.InvariantCulture);
                    _applicationSettingRepository.Update(sessionTimeOut);
                }
            }
        }

        public ApplicationSetting MaxSignatureSize
        {
            get => _applicationSettingRepository.GetSettingByElement("MaxSignatureSize");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public int MaxSignatureSizeProperty
        {
            get
            {
                var maxSignatureSize = MaxSignatureSize;
                if (maxSignatureSize == null || string.IsNullOrWhiteSpace(maxSignatureSize.Value))
                {
                    return 0;
                }

                int.TryParse(maxSignatureSize.Value, out var result);
                return result;
            }

            set
            {
                var maxSignatureSize = MaxSignatureSize;
                if (maxSignatureSize != null)
                {
                    maxSignatureSize.Value = value.ToString(CultureInfo.InvariantCulture);
                    _applicationSettingRepository.AddOrUpdate(maxSignatureSize);
                }
            }
        }

        public ApplicationSetting AllowedSignatureTypes
        {
            get => _applicationSettingRepository.GetSettingByElement("AllowedSignatureTypes");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string AllowedSignatureTypesProperty
        {
            get => AllowedSignatureTypes == null ? string.Empty : AllowedSignatureTypes.Value;

            set
            {
                var allowedSignatureTypes = AllowedSignatureTypes;
                if (allowedSignatureTypes != null)
                {
                    allowedSignatureTypes.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(allowedSignatureTypes);
                }
            }
        }

        public ApplicationSetting SmtpServer
        {
            get => _applicationSettingRepository.GetSettingByElement("SmtpServer");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string SmtpServerProperty
        {
            get => SmtpServer == null ? string.Empty : SmtpServer.Value;

            set
            {
                var smtpServer = SmtpServer;
                if (smtpServer != null)
                {
                    smtpServer.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(smtpServer);
                }
            }
        }

        public ApplicationSetting SmtpUser
        {
            get => _applicationSettingRepository.GetSettingByElement("SmtpUser");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string SmtpUserProperty
        {
            get => SmtpUser == null ? string.Empty : SmtpUser.Value;

            set
            {
                var smtpUser = SmtpUser;
                if (smtpUser != null)
                {
                    smtpUser.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(smtpUser);
                }
            }
        }

        public ApplicationSetting SmtpPassword
        {
            get => _applicationSettingRepository.GetSettingByElement("SmtpPassword");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string SmtpPasswordProperty
        {
            get => SmtpPassword == null ? string.Empty : SmtpPassword.Value;

            set
            {
                var smtpPassword = SmtpPassword;
                if (smtpPassword != null)
                {
                    smtpPassword.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(smtpPassword);
                }
            }
        }

        public ApplicationSetting EmailFromAddress
        {
            get => _applicationSettingRepository.GetSettingByElement("EmailFromAddress");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string EmailFromAddressProperty
        {
            get => EmailFromAddress == null ? string.Empty : EmailFromAddress.Value;

            set
            {
                var emailFromAddress = EmailFromAddress;
                if (emailFromAddress != null)
                {
                    emailFromAddress.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(emailFromAddress);
                }
            }
        }

        public ApplicationSetting SmtpPort
        {
            get => _applicationSettingRepository.GetSettingByElement("SmtpPort");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public int SmtpPortProperty
        {
            get
            {
                var smtpPort = SmtpPort;
                if (smtpPort == null || smtpPort.Value == string.Empty || !int.TryParse(smtpPort.Value, out var nr))
                {
                    return 0;
                }

                return nr;
            }

            set
            {
                var smtpPort = SmtpPort;
                if (SmtpPort != null)
                {
                    smtpPort.Value = value.ToString(CultureInfo.InvariantCulture);
                    _applicationSettingRepository.AddOrUpdate(smtpPort);
                }
            }
        }

        public ApplicationSetting SmtpSsl
        {
            get => _applicationSettingRepository.GetSettingByElement("SmtpSsl");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public bool SmtpSslProperty
        {
            get
            {
                if (SmtpSsl == null)
                {
                    return false;
                }

                bool.TryParse(SmtpSsl.Value, out var result);
                return result;
            }

            set
            {
                var smtpSsl = SmtpSsl;
                if (smtpSsl != null)
                {
                    smtpSsl.Value = value.ToString();
                    _applicationSettingRepository.AddOrUpdate(smtpSsl);
                }
            }
        }

        public ApplicationSetting EmailFromDisplayName
        {
            get => _applicationSettingRepository.GetSettingByElement("EmailFromDisplayName");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string EmailFromDisplayNameProperty
        {
            get => EmailFromDisplayName == null ? string.Empty : EmailFromDisplayName.Value;

            set
            {
                var emailFromDisplayName = EmailFromDisplayName;
                if (emailFromDisplayName != null)
                {
                    emailFromDisplayName.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(emailFromDisplayName);
                }
            }
        }

        public ApplicationSetting ApplicationLink
        {
            get => _applicationSettingRepository.GetSettingByElement("ApplicationLink");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public string ApplicationLinkProperty
        {
            get => ApplicationLink == null ? string.Empty : ApplicationLink.Value;

            set
            {
                var applicationLink = ApplicationLink;
                if (applicationLink != null)
                {
                    applicationLink.Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                    _applicationSettingRepository.AddOrUpdate(applicationLink);
                }
            }
        }

        public ApplicationSetting TokenTimeOut
        {
            get => _applicationSettingRepository.GetSettingByElement("TokenTimeOut");

            set => _applicationSettingRepository.AddOrUpdate(value);
        }

        public int TokenTimeOutProperty
        {
            get => TokenTimeOut == null || string.IsNullOrWhiteSpace(TokenTimeOut.Value) ? 24 : Convert.ToInt32(TokenTimeOut.Value);

            set
            {
                var tokenTimeOut = TokenTimeOut;
                if (tokenTimeOut != null)
                {
                    tokenTimeOut.Value = value.ToString(CultureInfo.InvariantCulture);
                    _applicationSettingRepository.AddOrUpdate(tokenTimeOut);
                }
            }
        }

        public void SaveElems()
        {
            _applicationSettingRepository.SaveAsync();
        }

        public void Dispose()
        {
            _applicationSettingRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
