using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using UploadWcfService;

namespace UpLoadConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CreateDomain("Server").DoCallBack(delegate
            {
                ServiceHost host = new ServiceHost(typeof(UpLoadService),
                  new Uri("http://xiaozhuang:8111/UpLoadService.svc"));

                BasicHttpBinding binding = new BasicHttpBinding();
                binding.TransferMode = TransferMode.Streamed;
                binding.MaxReceivedMessageSize = 67108864;

                host.AddServiceEndpoint(typeof(IUpLoadService), binding, "");

                host.Open();
            });

            BasicHttpBinding binding2 = new BasicHttpBinding();
            binding2.TransferMode = TransferMode.Streamed;

            IUpLoadService channel = ChannelFactory<IUpLoadService>.CreateChannel(binding2,
              new EndpointAddress("http://xiaozhuang:8111/UpLoadService.svc"));

            using (channel as IDisposable)
            {
                FileUploadMessage file = new FileUploadMessage();
                file.SavePath = "ppp";
                file.FileName = "safs.bak";
                file.FileData = new FileStream("F:\\lll.bak", FileMode.Open);

                channel.UploadFile(file);

                file.FileData.Close();
            }

        }
    }
}
