using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace WebDemoProject
{
    public class GaugeReportDataSource
    {
        public ObjectQuery<vGauge> GetGauges()
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            ObjectQuery<vGauge> objGauge = objDB.vGauge.Where("it.IsDeleted=false");

            return objGauge;
        }

        public ObjectQuery<tblGauge> GetGauges2()
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            ObjectQuery<tblGauge> query = objDB.tblGauge.Where("it.IsDeleted=false");

            return query;
        }
    }
}