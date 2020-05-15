
namespace CMotion.AdvantechAps
{
    public enum APS_Define
    {
        RDY=0,
        ALM,
        PLMT,
        NLMT,
        ORG,
        DIR,
        EMG,
        PCS,
        ERC,
        EZ,
        CLR,
        LTC,
        SD,
        INP,
        SVON,
        ALRM,
        PSLMT,
        NSLMT,
        CMP,
        CAMDO
    }
    public enum APS_StateStatus
    {
        STA_AxDisable = 0,
        STA_AxReady,
        STA_Stopping,
        STA_AxErrorStop,
        STA_AxHoming,
        STA_AxPtpMotion,
        STA_AxContiMotion,
        STA_AxSyncMotion,
        STA_AX_EXT_JOG,
        STA_AX_EXT_MPG
    }
    public enum APS_MotionStatus
    {
        Stop = 0,
        Res1,
        WaitERC,
        Res2,
        CorrectBksh,
        Res3,
        InFA,
        InFL,
        InACC,
        WaitINP
    }
}
