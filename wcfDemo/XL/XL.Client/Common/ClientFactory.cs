using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using XL.ServiceAPI;
using System.IO;
using System.ServiceModel.Channels;

namespace XL.Client.Common
{
    public class ClientFactory<TClient> : IDisposable
    {
        static EndpointAddress serviceAddress;
        static WSHttpBinding binding;
        ChannelFactory<TClient> factory;
        TClient proxy;
        OperationContextScope scope;
        public TClient CreateClient()
        {
            factory = new ChannelFactory<TClient>(binding, serviceAddress);
            proxy = factory.CreateChannel();
            ((IClientChannel)proxy).Faulted += new EventHandler(a_Faulted);
            scope = new OperationContextScope(((IClientChannel)proxy));
            var curId = CacheStrategy.CurUser == null ? Guid.Empty : CacheStrategy.CurUser.Id;
            MessageHeader<Guid> mhg = new MessageHeader<Guid>(curId);
            MessageHeader untyped = mhg.GetUntypedHeader("token", "ns");
            OperationContext.Current.OutgoingMessageHeaders.Add(untyped);
            return proxy;
        }
        void a_Faulted(object sender, EventArgs e)
        {
            //todo:此处得不到异常的内容
        }
        public void Dispose()
        {
            try
            {
                scope.Dispose();
                ((IClientChannel)proxy).Close();
                factory.Close();
            }
            catch
            {
            }
        }
        static ClientFactory()
        {
            var surl = ConfigurationManager.AppSettings["ServiceURL"];
            var iname = typeof(TClient).FullName.Substring("XL.ServiceAPI.".Length);
            iname = iname.Replace(".I", "-");
            var sname = string.Format("{0}Service", iname);
            var url = Path.Combine(surl, sname);
            serviceAddress = new EndpointAddress(url);


            binding = new WSHttpBinding();
            binding.CloseTimeout = new TimeSpan(10, 10, 10);
            binding.OpenTimeout = new TimeSpan(10, 10, 10);
            binding.SendTimeout = new TimeSpan(10, 10, 10);
            binding.ReceiveTimeout = new TimeSpan(10, 10, 10);
            binding.AllowCookies = true;            
        }
    }
}
