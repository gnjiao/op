using System;
using System.Collections.Generic;
using System.Drawing;
using System.ToolKit;
namespace Motion.Tray
{

    /// <summary>
    /// 托盘类，包含托盘类型，计数，移位，坐标等
    /// </summary>
    public class Tray
    {
        private List<Index> lstEmpty = new List<Index>();//盘中无效位置
        public Action updateColor;//更新点位时的委托
        /// <summary>
        /// 有效排序位置
        /// </summary>
        public Dictionary<int, Index> dic_Index = new Dictionary<int, Index>();
        /// <summary>
        /// 托盘ID号
        /// </summary>
        public string Type { get; set; }//托盘id
        /// <summary>
        /// 托盘名称
        /// </summary>
        public string Name { get; set; }//名称
        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get; set; }//托盘行数 
        /// <summary>
        /// 列数
        /// </summary>
        public int Column { get; set; }//托盘列数
        /// <summary>
        /// 起始位置
        /// </summary>
        public EStartPos StartPose { get; set; }
        /// <summary>
        /// 排列方向
        /// </summary>
        public EIndexDirect Direction { get; set; }
        /// <summary>
        /// 换行方式
        /// </summary>
        public EChangeLine ChangeLineType { get; set; }
        /// <summary>
        /// 屏蔽点位
        /// </summary>
        public string Empty
        {
            get
            {
                string strReturn = "";
                int iLen = lstEmpty.Count;
                if (iLen <= 0) return "";
                for (int i = 0; i < iLen; i++)
                {
                    if (i == iLen - 1)
                    {
                        strReturn += lstEmpty[i].ToString();
                        break;
                    }
                    strReturn += lstEmpty[i].ToString() + ",";
                }
                return strReturn;
            }
        }
        /// <summary>
        /// 当前位置
        /// </summary>
        public int CurrentPos { get; set; }
        /// <summary>
        /// 起始位置
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public int EndPos { get; set; }
        /// <summary>
        /// 是否已标定
        /// </summary>
        public bool IsCalibration { get; set; }
        /// <summary>
        /// 行X偏差值
        /// </summary>
        public double RowXoffset { get; set; }
        /// <summary>
        /// 行Y偏差值
        /// </summary>
        public double RowYoffset { get; set; }
        /// <summary>
        /// 列X偏差值
        /// </summary>
        public double ColumnXoffset { get; set; }
        /// <summary>
        /// 列Y偏差值
        /// </summary>
        public double ColumnYoffset { get; set; }

        /// <summary>
        /// 基准点索引号
        /// </summary>
        public int BaseIndex { get; set; }
        /// <summary>
        /// 基准点坐标
        /// </summary>
        public Point3D<double> BasePosition { get; set; }
        /// <summary>
        /// 行方向索引号
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 行方向坐标
        /// </summary>
        public Point3D<double> RowPosition { get; set; }
        /// <summary>
        /// 列方向索引号
        /// </summary>
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 列方向坐标
        /// </summary>
        public Point3D<double> ColumnPosition { get; set; }
        /// <summary>
        /// 建立托盘
        /// </summary>
        /// <param name="id">托盘序号</param>
        /// <param name="name">托盘命名</param>
        /// <param name="Row">托盘行数</param>
        /// <param name="Column">托盘列数</param>
        public Tray(string type, string name, int Row, int Column)
        {
            Type = type;
            Name = name;
            this.Row = Row;
            this.Column = Column;
            StartPose = EStartPos.左上角;
            Direction = EIndexDirect.行;
            ChangeLineType = EChangeLine.Z;
            CurrentPos = 0;
            StartPos = 1;
            EndPos = 100;
        }
        /// <summary>
        /// 初始化托盘,加载盘时调用
        /// </summary>
        public void InitTray(string start, string direct,string lineType, string strEmpty)
        {
            lstEmpty.Clear();
            StartPose = (EStartPos)Enum.Parse(typeof(EStartPos), start);
            Direction = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), direct);
            ChangeLineType = (EChangeLine)Enum.Parse(typeof(EChangeLine), lineType);
            List<string> lst = new List<string>();
            lst.AddRange(strEmpty.Split(','));
            lst.Remove("");
            foreach (string value in lst)
            {
                Index pos = new Index(value);
                lstEmpty.Add(pos);
            }
            SortTray(StartPose, Direction, ChangeLineType);
        }

        public void InitTrayValue(Color c)
        {
            for (int i = StartPos; i <= EndPos; i++)
            {
                SetNumColor(i, c);
            }
            CurrentPos = StartPos;
        }

        /// <summary>
        /// 找出盘中有效点，按起始位置和方向排列点位
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="direct">方向</param>
        /// <param name="lineType">换行方式</param>
        public void SortTray(EStartPos start, EIndexDirect direct,EChangeLine lineType)
        {
            dic_Index.Clear();
            int i = 1;
            switch (start)
            {
                #region"左上角"
                case EStartPos.左上角:
                    if (direct == EIndexDirect.行)
                    {
                        for (int r = 0; r < Row; r++)
                        {
                            //Z型换行
                            if (r % 2 == 1 && lineType == EChangeLine.S)
                            {
                                for (int c = Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = 0; c < Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < Column; c++)
                        {
                            if (c % 2 == 1 && lineType == EChangeLine.S)
                            {
                                for (int r = Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                            else
                            {
                                for (int r = 0; r < Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"左下角"
                case EStartPos.左下角:
                    if (direct == EIndexDirect.行)
                    {
                        for (int r = Row - 1; r >= 0; r--)
                        {
                            if (((Row % 2 == 1 && r % 2 == 1) || (Row % 2 == 0 && r % 2 == 0))
                                 && lineType == EChangeLine.S)
                            {
                                for (int c = Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                            else
                            {
                                for (int c = 0; c < Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        for (int c = 0; c < Column; c++)
                        {
                            if (c % 2 == 1 && lineType == EChangeLine.S)
                            {
                                for (int r = 0; r < Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右上角"
                case EStartPos.右上角:
                    if (direct == EIndexDirect.行)
                    {
                        for (int r = 0; r < Row; r++)
                        {
                            if (r % 2 == 1 && lineType == EChangeLine.S)
                            {
                                for (int c = 0; c < Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int c = Column - 1; c >= 0; c--)
                        {
                            if (((Column%2==0 && c % 2 == 0)|| (Column % 2 == 1 && c % 2 == 1))
                                && lineType == EChangeLine.S)
                            {
                                for (int r = Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = 0; r < Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region"右下角"
                case EStartPos.右下角:
                    if (direct == EIndexDirect.行)
                    {
                        for (int r = Row - 1; r >= 0; r--)
                        {
                            if ((( Row % 2 ==1 && r % 2 == 1)|| (Row % 2 == 0 && r % 2 == 0))
                                && lineType == EChangeLine.S)
                            {
                                for (int c = 0; c < Column; c++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int c = Column - 1; c >= 0; c--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int c = Column - 1; c >= 0; c--)
                        {
                            if (((Column%2==0 && c % 2 == 0)|| (Column % 2 == 1 && c % 2 == 1))
                                && lineType == EChangeLine.S)
                            {
                                for (int r = 0; r < Row; r++)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                for (int r = Row - 1; r >= 0; r--)
                                {
                                    Index pos = new Index(r, c);
                                    if (IsExistEmpty(pos))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dic_Index.Add(i, pos);
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    break;
                    #endregion
            }
            StartPos = 1;
            EndPos = dic_Index.Count;
        }
        /// <summary>
        /// 从指定的索引位置开始查找有效穴号，并返回该穴号位置
        /// </summary>
        /// <param name="_pos">开始查找的位置</param>
        /// <returns>返回有效穴号位置,如果返回-1则代表没有找到</returns>
        public int FindPos(Index _pos)
        {
            int result = -1;
            foreach (int i in dic_Index.Keys)
            {
                if (dic_Index[i].Equals(_pos))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 往盘中添加屏蔽位置
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        public void AddEmptyPos(int row, int col)
        {
            Index pos = new Index(row, col);
            if (!IsExistEmpty(pos))
            {
                lstEmpty.Add(pos);
            }
        }
        /// <summary>
        /// 往盘中添加屏蔽位置
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式赋值</param>        
        public void AddEmptyPos(string r_c)
        {
            Index pos = new Index(r_c);
            if (!IsExistEmpty(pos))
            {
                lstEmpty.Add(pos);
            }
        }
        /// <summary>
        /// 移除屏蔽位置
        /// </summary>
        /// <param name="r_c">以字符口串"R_C"的形式赋值</param>
        public void RemoveEmptyPos(string r_c)
        {
            Index pos = new Index(r_c);
            if (IsExistEmpty(pos))
            {
                lstEmpty.Remove(pos);
            }
        }
        //判断屏蔽位置是否存在
        public bool IsExistEmpty(Index _pos)
        {
            if (lstEmpty.Contains(_pos))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取所有屏蔽点的字符串形式
        /// </summary>
        /// <returns>以"r_c,r_c,...,r_c"形式返回字符串</returns>
        public string GetStringEmpty()
        {
            string strReturn = "";
            int iLen = lstEmpty.Count;
            for (int i = 0; i < iLen; i++)
            {
                if (i == iLen - 1)
                {
                    strReturn += lstEmpty[i].ToString();
                    break;
                }
                strReturn += lstEmpty[i].ToString() + ",";
            }
            return strReturn;
        }
        /// <summary>
        /// 设置指定序号控件的背景颜色
        /// </summary>
        /// <param name="num">穴号</param>
        /// <param name="bColor">颜色</param>
        public void SetNumColor(int num, Color bColor)
        {
            Index index = dic_Index[num];
            index.color = bColor;
            dic_Index[num] = index;
        }
        /// <summary>
        /// 托盘颜色复位
        /// </summary>
        /// <param name="bColor"></param>
        public void ResetTrayColor(Color bColor)
        {
            var keylist = new List<int>();
            foreach (var key in dic_Index.Keys)
            {
                keylist.Add(key);
            }
            foreach (var key in keylist)
            {
                //setNumColor(key, bColor);
                var index = dic_Index[key];
                index.color = bColor;
                dic_Index[key] = index;
            }
        }
        /// <summary>
        /// 设置托盘起始结束位
        /// </summary>
        /// <param name="_startPos">起始位置</param>
        /// <param name="_endPos">结束位置</param>
        /// <param name="fillColor">起始位到结束位的显示颜色</param>
        /// <param name="fillColor2">无效位置的显示颜色</param>
        public void SetStartEndPos(int _startPos, int _endPos, Color fillColor, Color fillColor2)
        {
            if (_startPos > _endPos)
                return;
            CurrentPos = _startPos;
            StartPos = _startPos;
            EndPos = _endPos;
            for (int i = 1; i < _startPos; i++)
            {
                SetNumColor(i, fillColor2);
            }
            int count = dic_Index.Count;
            for (int i = _endPos; i < count + 1; i++)
            {
                SetNumColor(i, fillColor2);
            }
            for (int i = _startPos; i <= _endPos; i++)
            {
                SetNumColor(i, fillColor);
            }
        }
        /// <summary>
        /// 获取当前点数据位置
        /// </summary>
        /// <param name="point">参考点坐标</param>
        /// <param name="Trayindex">托盘索引</param>
        /// <returns></returns>
        public Point3D<double> GetPosition(Point3D<double> point, int Trayindex, bool Xdir = false, bool Ydir = false)
        {
            var pos = new Point3D<double>();
            Index Traypos = dic_Index[Trayindex];
            Index TrayBasePos = dic_Index[1];
            if (point.X == 0 && point.Y == 0)
            {
                pos.X = 0;
                pos.Y = 0;
                pos.Z = 0;
                return pos;
            }
            pos.X = point.X + ((Traypos.Row - TrayBasePos.Row) * RowXoffset
                + (Traypos.Col - TrayBasePos.Col) * ColumnXoffset) * (Xdir ? -1 : 1);
            pos.Y = point.Y + ((Traypos.Row - TrayBasePos.Row) * RowYoffset
                + (Traypos.Col - TrayBasePos.Col) * ColumnYoffset) * (Ydir ? -1 : 1);
            pos.Z = point.Z;
            return pos;
        }
        /// <summary>
        /// 获取当前点数据位置
        /// </summary>
        /// <param name="point">参考点坐标</param>
        /// <param name="Trayindex">托盘索引</param>
        /// <returns></returns>
        public Point<double> GetPosition(Point<double> point, int Trayindex, bool Xdir = false, bool Ydir = false)
        {
            var pos = new Point<double>();
            Index Traypos = dic_Index[Trayindex];
            Index TrayBasePos = dic_Index[1];
            if (point.X == 0 && point.Y == 0)
            {
                pos.X = 0;
                pos.Y = 0;
                return pos;
            }
            pos.X = point.X + ((Traypos.Row - TrayBasePos.Row) * RowXoffset
                + (Traypos.Col - TrayBasePos.Col) * ColumnXoffset) * (Xdir ? -1 : 1);
            pos.Y = point.Y + ((Traypos.Row - TrayBasePos.Row) * RowYoffset
                + (Traypos.Col - TrayBasePos.Col) * ColumnYoffset) * (Ydir ? -1 : 1);
            return pos;
        }
    }
    public enum EStartPos
    {
        左上角,
        左下角,
        右上角,
        右下角
    }
    public enum EIndexDirect
    {
        行,
        列
    }
    public enum EChangeLine
    {
        Z,
        S
    }
}
