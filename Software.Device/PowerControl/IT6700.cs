using System;
//using Ivi.Visa.Interop;


namespace Software.Device
{
    public class IT6700
    {
        public IT6700()
        {
            //Guid clsid = new Guid("DB8CBF1D-D6D3-11D4-AA51-00A024EE30BD");
            //this.ioDmm = (FormattedIO488)Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
            //clsid = new Guid("DB8CBF1C-D6D3-11D4-AA51-00A024EE30BD");
            //this.mgr = (ResourceManager)Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
        }

        //#region 参数
        //private FormattedIO488 ioDmm;
        //private ResourceManager mgr;
        //#endregion

        //#region 公有函数
        ///// <summary>
        ///// 连接电源,地址格式举例:"ASRL3::INSTR"
        ///// </summary>
        ///// <param name="ioAddress"></param>
        ///// <returns></returns>
        //public bool Connect(string ioAddress)
        //{
        //    try
        //    {
        //        this.ioDmm.IO = (IMessage)this.mgr.Open(ioAddress, AccessMode.NO_LOCK, 2000, "");
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show($"InitIO Error:{ex}");
        //        return false;
        //    }
        //    return true;
        //}
        ///// <summary>
        ///// 设置电压
        ///// </summary>
        ///// <param name="VolValue"></param>
        ///// <returns></returns>
        //public bool SetVol(double VolValue)
        //{
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("VOLT " + VolValue.ToString(), -1 != 0);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 设置电流
        ///// </summary>
        ///// <param name="CurrentValue"></param>
        ///// <returns></returns>
        //public bool SetCurrent(double CurrentValue)
        //{
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("CURR " + CurrentValue.ToString(), -1 != 0);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 读取电压,读取失败时返回-1
        ///// </summary>
        ///// <returns></returns>
        //public double ReadVol()
        //{
        //    double ReadVol = -1;
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("VOLT?", -1 != 0);
        //        double vol = Convert.ToDouble(this.ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_Any, -1 != 0));
        //        ReadVol = vol;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return ReadVol;
        //}
        ///// <summary>
        ///// 读取电流,读取失败时返回-1
        ///// </summary>
        ///// <returns></returns>
        //public double ReadCurrent()
        //{
        //    double ReadCurrent = -1;
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("CURR?", -1 != 0);
        //        double Current = Convert.ToDouble(this.ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_Any, -1 != 0));
        //        ReadCurrent = Current;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return ReadCurrent;
        //}

        ///// <summary>
        ///// 测试电压,测试失败时返回-1
        ///// </summary>
        ///// <returns></returns>
        //public double MeasureVol()
        //{
        //    double MeasureVol = -1;
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("MEAS:VOLT?", -1 != 0);
        //        double vol = Convert.ToDouble(this.ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_Any, -1 != 0));
        //        MeasureVol = vol;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return MeasureVol;
        //}
        ///// <summary>
        ///// 测试电流,测试失败时返回-1
        ///// </summary>
        ///// <returns></returns>
        //public double MeasureCurrent()
        //{
        //    double MeasureCurrent = -1;
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("MEAS:CURR?", -1 != 0);
        //        double Current = Convert.ToDouble(this.ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_Any, -1 != 0));
        //        MeasureCurrent = Current;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return MeasureCurrent;
        //}

        ///// <summary>
        ///// 读取电压输出状态
        ///// </summary>
        ///// <returns></returns>
        //public bool ReadOutPutState()
        //{
        //    bool ReadOutPutState = false;
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        this.ioDmm.WriteString("OUTP?", -1 != 0);
        //        bool state = Convert.ToBoolean(this.ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_Any, -1 != 0));
        //        ReadOutPutState = state;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return ReadOutPutState;
        //}
        ///// <summary>
        ///// 是否输出电压
        ///// </summary>
        ///// <param name="onoff"></param>
        ///// <returns></returns>
        //public bool OutPut(bool onoff)
        //{
        //    try
        //    {
        //        this.ioDmm.WriteString("Syst:Rem", -1 != 0);
        //        if (onoff)
        //        {
        //            this.ioDmm.WriteString("OUTP 1", -1 != 0);
        //        }
        //        else
        //        {
        //            this.ioDmm.WriteString("OUTP 0", -1 != 0);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return false;
        //}
        //#endregion

        //#region 简易方法
        //public void IT67XX_Init(string addstr, double vol, double maxCurrent)
        //{
        //    if (Connect(addstr))
        //    {
        //        if (ReadVol() != vol)
        //        {
        //            OutPut(false);
        //            SetVol(vol);
        //        }

        //        if (ReadCurrent() != maxCurrent)
        //        {
        //            OutPut(false);
        //            SetCurrent(maxCurrent);
        //        }
        //        if (!ReadOutPutState())
        //        {
        //            OutPut(true);
        //        }
        //    }
        //}
        //public void IT67XX_Close()
        //{
        //    SetVol(0);
        //    SetCurrent(0);
        //    OutPut(false);
        //}
        //#endregion
    }
}
