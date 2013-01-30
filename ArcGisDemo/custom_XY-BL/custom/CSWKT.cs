using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Converters.WellKnownText;
namespace custom
{
    public enum CSType
    {
        GCS_Xian_1980,
        Xian_1980_GK_Zone_18,
        Xian_1980_GK_Zone_19,
        Xian_1980_3_Degree_GK_Zone_35,
        Xian_1980_3_Degree_GK_Zone_36,
        Xian_1980_3_Degree_GK_Zone_37,

        GCS_Beijing_1954,
        Beijing_1954_GK_Zone_18,
        Beijing_1954_GK_Zone_19,
        Beijing_1954_3_Degree_GK_Zone_35,
        Beijing_1954_3_Degree_GK_Zone_36,
        Beijing_1954_3_Degree_GK_Zone_37,
    }


    public class Utility
    {
        public Utility()
        {
        }

        public static double[] TransForm(double x, double y, CSType fromCSType, CSType toCSType)
        {
            ICoordinateSystem fromCS = GetCoordinateSystem(fromCSType);
            ICoordinateSystem toCS = GetCoordinateSystem(toCSType);
            string dH = y.ToString().Substring(0, 2);
            double[] point = new double[2] { y, x };
            double[] result = new double[2];
            if (y.ToString().Length < 3)
            {
                return null;
            }
            if (fromCS != null && toCS != null)
            {
                PtsToPts(fromCS, toCS, point, out result);
            }
            else
            {
                return null;
            }
            return result;
        }

        private static ICoordinateSystem GetCoordinateSystem(CSType csType)
        {
            ICoordinateSystem cs = null;
            switch (csType)
            {
                case CSType.GCS_Xian_1980:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.GCS_Xian_1980) as IGeographicCoordinateSystem;
                    break;
                case CSType.Xian_1980_3_Degree_GK_Zone_35:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Xian_1980_3_Degree_GK_Zone_35) as IProjectedCoordinateSystem;
                    break;
                case CSType.Xian_1980_3_Degree_GK_Zone_36:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Xian_1980_3_Degree_GK_Zone_36) as IProjectedCoordinateSystem;
                    break;
                case CSType.Xian_1980_3_Degree_GK_Zone_37:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Xian_1980_3_Degree_GK_Zone_37) as IProjectedCoordinateSystem;
                    break;
                case CSType.Xian_1980_GK_Zone_18:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Xian_1980_GK_Zone_18) as IProjectedCoordinateSystem;
                    break;
                case CSType.Xian_1980_GK_Zone_19:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Xian_1980_GK_Zone_19) as IProjectedCoordinateSystem;
                    break;
                case CSType.GCS_Beijing_1954:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.GCS_Beijing_1954) as IGeographicCoordinateSystem;
                    break;
                case CSType.Beijing_1954_3_Degree_GK_Zone_35:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Beijing_1954_3_Degree_GK_Zone_35) as IProjectedCoordinateSystem;
                    break;
                case CSType.Beijing_1954_3_Degree_GK_Zone_36:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Beijing_1954_3_Degree_GK_Zone_36) as IProjectedCoordinateSystem;
                    break;
                case CSType.Beijing_1954_3_Degree_GK_Zone_37:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Beijing_1954_3_Degree_GK_Zone_37) as IProjectedCoordinateSystem;
                    break;
                case CSType.Beijing_1954_GK_Zone_18:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Beijing_1954_GK_Zone_18) as IProjectedCoordinateSystem;
                    break;
                case CSType.Beijing_1954_GK_Zone_19:
                    cs = CoordinateSystemWktReader.Parse(CSWKT.Beijing_1954_GK_Zone_19) as IProjectedCoordinateSystem;
                    break;

            }
            return cs;
        }
        public static string DegreeToDMS(double degree)
        {
            double deg = Math.Floor(degree);

            double min = Math.Floor((degree - deg) * 60);

            double sec = (degree - deg) * 3600 - min * 60;


            string d = deg.ToString();
            string m = min.ToString("00");
            string s = sec.ToString("#00.00");

            return d + m + s;
        }

        public static double DMSToDegree(string dms)
        {
            string[] str = dms.Split('.');

            bool right = false;
            double d = 0;
            double m = 0;
            double s = 0;

            if (str.Length >= 1)
            {
                if ((str[0].Length % 2) == 1)
                {
                    if (dms.Length < 3)
                    {
                        return -1;
                    }
                    else
                    {
                        if (!double.TryParse(dms.Substring(0, 3), out d))
                        {
                            return -1;
                        }

                        if (dms.Length == 5)
                        {
                            if (!double.TryParse(dms.Substring(3, 2), out m))
                            {
                                return -1;
                            }
                        }

                        if (dms.Length > 5)
                        {
                            if (!double.TryParse(dms.Substring(3, 2), out m))
                            {
                                return -1;
                            }

                            if (!double.TryParse(dms.Substring(5), out s))
                            {
                                return -1;
                            }
                        }
                    }
                }
                else if ((str[0].Length % 2) == 0)
                {
                    if (dms.Length < 2)
                    {
                        return -1;
                    }
                    else
                    {
                        if (!double.TryParse(dms.Substring(0, 2), out d))
                        {
                            return -1;
                        }

                        if (dms.Length == 4)
                        {
                            if (!double.TryParse(dms.Substring(2, 2), out m))
                            {
                                return -1;
                            }
                        }

                        if (dms.Length > 4)
                        {
                            if (!double.TryParse(dms.Substring(2, 2), out m))
                            {
                                return -1;
                            }
                            if (!double.TryParse(dms.Substring(4), out s))
                            {
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    return -1;
                }


            }
            else
            {
                return -1;
            }

            if (m > 60 && s > 60)
            {
                return -1;
            }
            return d + m / 60 + s / 3600;

        }

        public static double GetValue(string value)
        {
            double result;
            bool b = double.TryParse(value, out result);
            if (!b)
            {
                result = -1;
            }
            return result;
        }

        //public static string GetXianXiangByCode(string code)
        //{
        //    string result = "";
        //    Database db = new Database();
        //    try
        //    {
        //        db.OledbOpen();
        //        string queryString = "select distinct 县乡 from 省县乡_V where trim(乡编号)='" + code + "'";
        //        DataRow dr = db.getAccessDataRow(queryString);
        //        if (dr != null)
        //        {
        //            int i = 0;
        //            result = dr[0].ToString();
        //        }
        //        db.OledbClose();
        //    }
        //    catch
        //    {
        //        db.OledbClose();
        //    }

        //    return result;

        //}

        private static void PtsToPts(ICoordinateSystem fromCS, ICoordinateSystem toCS, double[] point, out double[] result)
        {
            try
            {
                CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
                ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(fromCS, toCS);
                result = trans.MathTransform.Transform(point);
            }
            catch (System.Exception e)
            {
                result = null;
            }


        }

        /// <summary>
        /// 判断即将入ZHDA01A表（现今变形迹象表）中的数据是否合理
        /// </summary>
        /// <param name="dgv">datagridview控件名称</param>
        /// <param name="index">主键index</param>
        /// <returns>bool值</returns>        
        //public static bool IsValidDatagridView(DataGridView dgv, int index)
        //{
        //    bool result = false;
        //    try
        //    {
        //        for (int i = 0; i < dgv.Rows.Count - 1; i++)
        //        {
        //            if (dgv[index, i].Value.ToString().Trim() == "")
        //            {
        //                return false;
        //            }
        //            //for (int j = i+1; j < dgv.Rows.Count - 1; j++)
        //            //{
        //            //    if (dgv[index,i].Value.ToString()==dgv[index,j].Value.ToString())
        //            //    {
        //            //        return false;
        //            //    }
        //            //}
        //        }
        //        result = true;

        //    }
        //    catch (System.Exception e)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}
    }
    class CSWKT
    {
        
        private static string _GCS_Beijing_1954 = "GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]]";
        private static string _Beijing_1954_GK_Zone_18 = "PROJCS[\"Beijing_1954_GK_Zone_18 \", GEOGCS[\"GCS_Beijing_1954\", DATUM[\"D_Beijing_1954\", SPHEROID[\"Krasovsky_1940\", 6378245, 298.3]], PRIMEM[\"Greenwich\", 0], UNIT[\"Degree\", 0.017453292519943295]], PROJECTION[\"Transverse_Mercator\"], PARAMETER[\"False_Easting\", 18500000], PARAMETER[\"False_Northing\", 0], PARAMETER[\"Central_Meridian\", 105], PARAMETER[\"Scale_Factor\", 1], PARAMETER[\"Latitude_Of_Origin\", 0], UNIT[\"Meter\", 1.0]]";
        //private static string _Beijing_1954_GK_Zone_18 = "PROJCS[\"liongg\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Lambert_Conformal_Conic\"],PARAMETER[\"False_Easting\",18500000],PARAMETER[\"False_Northing\",0],PARAMETER[\"Central_Meridian\",105],PARAMETER[\"Standard_Parallel_1\",25],PARAMETER[\"Standard_Parallel_2\",47],PARAMETER[\"Latitude_Of_Origin\",0],UNIT[\"Meter\",1]]"; Transverse_Mercator
        private static string _Beijing_1954_GK_Zone_19 = "PROJCS[\"Beijing_1954_GK_Zone_19 \", GEOGCS[\"GCS_Beijing_1954\", DATUM[\"D_Beijing_1954\", SPHEROID[\"Krasovsky_1940\", 6378245, 298.3]], PRIMEM[\"Greenwich\", 0], UNIT[\"Degree\", 0.017453292519943295]], PROJECTION[\"Transverse_Mercator\"], PARAMETER[\"False_Easting\", 19500000], PARAMETER[\"False_Northing\", 0], PARAMETER[\"Central_Meridian\", 111], PARAMETER[\"Scale_Factor\", 1], PARAMETER[\"Latitude_Of_Origin\", 0], UNIT[\"Meter\", 1.0]]";

        private static string _Beijing_1954_3_Degree_GK_Zone_35 = "PROJCS[\"Beijing_1954_3_Degree_GK_Zone_35\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",35500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private static string _Beijing_1954_3_Degree_GK_Zone_36 = "PROJCS[\"Beijing_1954_3_Degree_GK_Zone_36\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",36500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private static string _Beijing_1954_3_Degree_GK_Zone_37 = "PROJCS[\"Beijing_1954_3_Degree_GK_Zone_37\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",37500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",111.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";

        private static string _GCS_Xian_1980 = "GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]";
        private static string _Xian_1980_GK_Zone_18 = "PROJCS[\"Xian_1980_GK_Zone_18\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",18500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private static string _Xian_1980_GK_Zone_19 = "PROJCS[\"Xian_1980_GK_Zone_19\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",19500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",111.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";

        private static string _Xian_1980_3_Degree_GK_Zone_35 = "PROJCS[\"Xian_1980_3_Degree_GK_Zone_35\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",35500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private static string _Xian_1980_3_Degree_GK_Zone_36 = "PROJCS[\"Xian_1980_3_Degree_GK_Zone_36\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",36500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private static string _Xian_1980_3_Degree_GK_Zone_37 = "PROJCS[\"Xian_1980_3_Degree_GK_Zone_37\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",37500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",111.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";

        public static string GCS_Beijing_1954
        {
            get { return _GCS_Beijing_1954; }
        }
        
        public static string Beijing_1954_GK_Zone_18
        {
            get { return _Beijing_1954_GK_Zone_18; }
        }

        public static string Beijing_1954_GK_Zone_19
        {
            get { return _Beijing_1954_GK_Zone_19; }
        }

        public static string Beijing_1954_3_Degree_GK_Zone_35
        {
            get { return _Beijing_1954_3_Degree_GK_Zone_35; }
        }

        public static string Beijing_1954_3_Degree_GK_Zone_36
        {
            get { return _Beijing_1954_3_Degree_GK_Zone_36; }
        }

        public static string Beijing_1954_3_Degree_GK_Zone_37
        {
            get { return _Beijing_1954_3_Degree_GK_Zone_37; }
        }

        public static string GCS_Xian_1980
        {
            get { return _GCS_Xian_1980; }
        }

        public static string Xian_1980_GK_Zone_18
        {
            get { return _Xian_1980_GK_Zone_18; }
        }

        public static string Xian_1980_GK_Zone_19
        {
            get { return _Xian_1980_GK_Zone_19; }
        }

        public static string Xian_1980_3_Degree_GK_Zone_35
        {
            get { return _Xian_1980_3_Degree_GK_Zone_35; }
        }

        public static string Xian_1980_3_Degree_GK_Zone_36
        {
            get { return _Xian_1980_3_Degree_GK_Zone_36; }
        }

        public static string Xian_1980_3_Degree_GK_Zone_37
        {
            get { return _Xian_1980_3_Degree_GK_Zone_37; }
        }
    }
    
}
