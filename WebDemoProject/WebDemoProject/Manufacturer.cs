using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace WebDemoProject
{
    public class Manufacturer
    {
        public bool Insert(tblManufacturer NewManufacturer)
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            try
            {
                objDB.tblManufacturer.AddObject(NewManufacturer);
                
                objDB.SaveChanges();

                return true;
            }
            catch 
            {
                
                return false;
            }
        }

        public bool Update(tblManufacturer NewManufacturer)
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            tblManufacturer OldManufacturer= objDB.tblManufacturer.First(c=>c.MfrID==NewManufacturer.MfrID);

            try
            {
                OldManufacturer.MfrName = NewManufacturer.MfrName;
                OldManufacturer.MfrType = NewManufacturer.MfrType;

                objDB.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(tblManufacturer OldManufacturer)
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            objDB.tblManufacturer.DeleteObject(OldManufacturer);

            try
            {
                objDB.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ObjectQuery<tblManufacturer> FindAll()
        {
            ObjectQuery<tblManufacturer> query;
            
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            query = objDB.tblManufacturer.Where("it.IsDeleted=false");

            return query;
        }

    }
}