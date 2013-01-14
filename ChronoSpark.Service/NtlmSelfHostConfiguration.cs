using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace ChronoSpark.Service
{
    public class NtlmSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public NtlmSelfHostConfiguration(string baseAddress)
            : base(baseAddress)
        { }

        public NtlmSelfHostConfiguration(Uri baseAddress)
            : base(baseAddress)
        { }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.TransportCredentialOnly;
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            return base.OnConfigureBinding(httpBinding);
        }
    }
}
