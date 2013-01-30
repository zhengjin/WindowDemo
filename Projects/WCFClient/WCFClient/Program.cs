using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using WCFClient.SchedulerDBEntities;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Uri svruri = new Uri("http://localhost:12345/SchedulerService.svc/Users(1)");

            SchedulerDBEntities.SchedulerDBEntities objDB = new SchedulerDBEntities.SchedulerDBEntities(svruri);

            DataServiceQuery<WCFClient.SchedulerDBEntities.Appointments> selDatas = 
                objDB.Appointments;

            foreach (WCFClient.SchedulerDBEntities.Appointments appointments in selDatas)
            {
                Console.WriteLine("Appointments Subject: {0}",appointments.Subject.ToString());
            }

            Console.ReadLine();
        }
    }
}
