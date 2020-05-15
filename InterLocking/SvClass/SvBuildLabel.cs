using System;
using System.ToolKit;

namespace InterLocking
{
    public class BuildLabel
    {
        #region 时间信息
        public string YYYY()
        {
            return DateTime.Now.ToString("yyyy");
        }
        public string YYYY(DateTime dt)
        {
            return dt.ToString("yyyy");
        }
        public string YY()
        {
            return DateTime.Now.ToString("yy");
        }
        public string YY(DateTime dt)
        {
            return dt.ToString("yy");
        }
        public string MM()
        {
            return DateTime.Now.ToString("mm");
        }
        public string MM(DateTime dt)
        {
            return dt.ToString("mm");
        }
        public string DD()
        {
            return DateTime.Now.ToString("dd");
        }
        public string DD(DateTime dt)
        {
            return dt.ToString("dd");
        }
        public string YYMMDD()
        {
            return DateTime.Now.ToString("yyMMdd");
        }
        public string YYMMDD(DateTime dt)
        {
            return dt.ToString("yyMMdd");
        }
        public string YYYYMMDD()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        public string YYYYMMDD(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
        #endregion

        #region 项目信息
        public string GetModelWithVirgule(string model)
        {
            return  model.Replace('-', '/').Replace('_', '/');
        }
        public string GetSN_19(string sn)
        {
            return sn.Left(12)+sn.Right(7);
        }
        public string GetA2C(string sn)
        {
            return sn.Left(12);
        }
        public string GetA2CWithSpace(string A2C)
        {
            return  A2C.Replace(" ","").Insert(7," ").Insert(4," ");
        }
        #endregion

        #region 测试代码
        //BuildLabel bl = new BuildLabel();
        //    bl.YYYY().PrintEx();
        //    bl.YY().PrintEx();
        //    bl.MM().PrintEx();
        //    bl.DD().PrintEx();
        //    bl.YYYYMMDD().PrintEx();

        //    string sn = "000001";
        //    string model = "NV2585_40";
        //    string A2C = "903030203440";
        //    bl.Model_Print(model).PrintEx();
        //    bl.A2C_Print(A2C).PrintEx();
        //    string GW_part_N="7901510XG83XB"+"BAQAF"+bl.YYYYMMDD();
        //    string GW_part_code = "67LFPB" + "BAQAF" + sn;
        //    string SV_product_SN = "67LFPB" + "BAQAF" +bl.YYMMDD()+sn;
        #endregion

    }
}
