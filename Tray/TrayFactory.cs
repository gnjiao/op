using System;
using System.Collections.Generic;
using System.ToolKit;

namespace Motion.Tray
{
    /// <summary>
    /// 托盘工厂模式
    /// </summary>
    /// <typeparam name="T">泛型:int,uint,long,double,float,short</typeparam>
    public class TrayFactory
    {
        /// <summary>
        /// 存放所有托盘对象的集合
        /// </summary>
        private static Dictionary<string, Tray> TrayDictionary = new Dictionary<string, Tray>();
        /// <summary>
        /// 通过指定字符串获取对象
        /// </summary>
        /// <param name="key">字符串</param>
        /// <returns>返回托盘对象</returns>
        public static Tray GetTrayFactory(string key) => TrayDictionary.ContainsKey(key) ? TrayDictionary[key] : null;
        public static Dictionary<string, Tray> GetTrayDict
        {
            get
            {
                return TrayDictionary;
            }
        }
        /// <summary>
        /// 获取托盘的个数
        /// </summary>
        /// <returns>返回托盘个数</returns>
        public static int GetTrayCount() => TrayDictionary.Count;
        /// <summary>
        /// 设置或添加对象
        /// </summary>
        /// <param name="key">托盘对应的字符串</param>
        /// <param name="tray">托盘对象</param>
        public static void SetTray(string key, Tray tray)
        {
            if (TrayDictionary.ContainsKey(key)) TrayDictionary[key] = tray;
            else TrayDictionary.Add(key, tray);
        }
        public static void RemoveTray(string key)
        {
            TrayDictionary.Remove(key);
        }
        /// <summary>
        /// 读取文件初始化对象
        /// </summary>
        /// <param name="strPath"></param>
        public static void LoadTrayFactory(string strPath)
        {
            TrayDictionary.Clear();
            var sectionList = System.ToolKit.Helpers.IniHelper.GetAllSectionNames(strPath);
            foreach (var section in sectionList)
            {
                var type = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Id", strPath, "1");
                var strName = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Name", strPath, "托盘" + section);
                var Row = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Row", strPath, 10);
                var Column = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Column", strPath, 10);
                var strStart = System.ToolKit.Helpers.IniHelper.ReadValue(section, "StartPose", strPath, "左上角");
                var strDirect = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Direction", strPath, "行");
                var strChangeLineType = System.ToolKit.Helpers.IniHelper.ReadValue(section, "ChangeLineType", strPath, "Z");
                var strEmpty = System.ToolKit.Helpers.IniHelper.ReadValue(section, "Empty", strPath, "");
                var bIsCalibration = System.ToolKit.Helpers.IniHelper.ReadValue(section, "IsCalibration", strPath, false);
                var iRowXoffset = System.ToolKit.Helpers.IniHelper.ReadValue(section, "RowXoffset", strPath, 0.0);
                var iRowYoffset = System.ToolKit.Helpers.IniHelper.ReadValue(section, "RowYoffset", strPath, 0.0);
                var iColumnXoffset = System.ToolKit.Helpers.IniHelper.ReadValue(section, "ColumnXoffset", strPath, 0.0);
                var iColumnYoffset = System.ToolKit.Helpers.IniHelper.ReadValue(section, "ColumnYoffset", strPath, 0.0);
                var iBaseIndex = System.ToolKit.Helpers.IniHelper.ReadValue(section, "BaseIndex", strPath, 1);
                var iBasePosition = Point3D<double>.Parse(System.ToolKit.Helpers.IniHelper.ReadValue(section, "BasePosition", strPath, "0,0,0"));
                var iRowIndex = System.ToolKit.Helpers.IniHelper.ReadValue(section, "RowIndex", strPath, 1);
                var iRowPosition = Point3D<double>.Parse(System.ToolKit.Helpers.IniHelper.ReadValue(section, "RowPosition", strPath, "0,0,0"));
                var iColumnIndex = System.ToolKit.Helpers.IniHelper.ReadValue(section, "ColumnIndex", strPath, 1);
                var iColumnPosition = Point3D<double>.Parse(System.ToolKit.Helpers.IniHelper.ReadValue(section, "ColumnPosition", strPath, "0,0,0"));
                var tray = new Tray(type, strName, Row, Column)
                {
                    IsCalibration = bIsCalibration,
                    RowXoffset = iRowXoffset,
                    RowYoffset = iRowYoffset,
                    ColumnXoffset = iColumnXoffset,
                    ColumnYoffset = iColumnYoffset,
                    BaseIndex = iBaseIndex,
                    BasePosition = iBasePosition,
                    RowIndex = iRowIndex,
                    RowPosition = iRowPosition,
                    ColumnIndex = iColumnIndex,
                    ColumnPosition = iColumnPosition
                };
                tray.InitTray(strStart, strDirect, strChangeLineType, strEmpty);
                SetTray(section, tray);
            }
        }
        /// <summary>
        /// 保存所有托盘对象参数到文件
        /// </summary>
        /// <param name="strPath">文件名,以.ini结尾</param>
        /// <returns>返回保存状态</returns>
        public static bool SaveTrayFactory(string strPath)
        {
            var bResult = true;
            foreach (KeyValuePair<string, Tray> m_tray in TrayDictionary)
            {
                Tray tray = m_tray.Value;
                var strSection = m_tray.Key;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Id", tray.Type.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Name", tray.Name, strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Row", tray.Row.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Column", tray.Column.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "StartPose", tray.StartPose.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Direction", tray.Direction.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "ChangeLineType", tray.ChangeLineType.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "Empty", tray.Empty, strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "IsCalibration", tray.IsCalibration.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "RowXoffset", tray.RowXoffset.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "RowYoffset", tray.RowYoffset.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "ColumnXoffset", tray.ColumnXoffset.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "ColumnYoffset", tray.ColumnYoffset.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "BaseIndex", tray.BaseIndex.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "BasePosition", tray.BasePosition.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "RowIndex", tray.RowIndex.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "RowPosition", tray.RowPosition.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "ColumnIndex", tray.ColumnIndex.ToString(), strPath) != 0;
                bResult &= System.ToolKit.Helpers.IniHelper.WriteValue(strSection, "ColumnPosition", tray.ColumnPosition.ToString(), strPath) != 0;
            }
            return bResult;
        }
        /// <summary>
        /// 标定计算
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool Calibration(string key)
        {
            Tray tray = TrayFactory.GetTrayFactory(key);
            if (tray == null) throw new Exception("托盘不存在！");
            var retR12 = (tray.dic_Index[tray.RowIndex].Row - tray.dic_Index[tray.BaseIndex].Row) != 0;
            var retC12 = (tray.dic_Index[tray.RowIndex].Col - tray.dic_Index[tray.BaseIndex].Col) != 0;
            var retR13 = (tray.dic_Index[tray.ColumnIndex].Row - tray.dic_Index[tray.BaseIndex].Row) != 0;
            var retC13 = (tray.dic_Index[tray.ColumnIndex].Col - tray.dic_Index[tray.BaseIndex].Col) != 0;
            if ((retR12 == retR13) || (retC12 == retC13)) throw new Exception("三点重合，或者三点再同一直线上！");
            if ((retR12 == retC12) || (retR13 == retC13)) throw new Exception("三点无法形成直角坐标系，非有效点！");
            var iRow = 0;
            var iColumn = 0;
            double detaRowX, detaRowY, detaColX, detaColY;
            if (retR12 && !retR13)
            {
                iRow = tray.dic_Index[tray.RowIndex].Row - tray.dic_Index[tray.BaseIndex].Row;
                iColumn = tray.dic_Index[tray.ColumnIndex].Col - tray.dic_Index[tray.BaseIndex].Col;
                detaRowX = tray.RowPosition.X - tray.BasePosition.X;
                detaRowY = tray.RowPosition.Y - tray.BasePosition.Y;
                detaColX = tray.ColumnPosition.X - tray.BasePosition.X;
                detaColY = tray.ColumnPosition.Y - tray.BasePosition.Y;
            }
            else
            {
                iRow = tray.dic_Index[tray.ColumnIndex].Row - tray.dic_Index[tray.BaseIndex].Row;
                iColumn = tray.dic_Index[tray.RowIndex].Col - tray.dic_Index[tray.BaseIndex].Col;
                detaColX = tray.RowPosition.X - tray.BasePosition.X;
                detaColY = tray.RowPosition.Y - tray.BasePosition.Y;
                detaRowX = tray.ColumnPosition.X - tray.BasePosition.X;
                detaRowY = tray.ColumnPosition.Y - tray.BasePosition.Y;
            }
            tray.RowXoffset = detaRowX / iRow;
            tray.RowYoffset = detaRowY / iRow;
            tray.ColumnXoffset = detaColX / iColumn;
            tray.ColumnYoffset = detaColY / iColumn;
            tray.IsCalibration = true;
            return true;
        }
    }
}
