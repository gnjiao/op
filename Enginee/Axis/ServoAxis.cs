using AxisControlls;
namespace System.Enginee
{
    /// <summary>
    ///     凌华 Adlink 伺服马达驱动轴
    /// </summary>
    public class ServoAxis : ApsAxis
    {
        public ServoAxis(AxisController apsController) : base(apsController)
        {
        }
        public override double CurrentPos
        {
            get
            {
                return ApsController.GetCurrentCommandPosition(NoId) * Transmission.PulseEquivalent;
            }
        }
        public override double BackPos
        {
            get
            {
                return ApsController.GetCurrentFeedbackPosition(NoId) * Transmission.PulseEquivalent;
            }
        }
       
       
       
        public override bool IsDone
        {
            get { return ApsController.IsInp(NoId); }
        }
        public override void SetCurrentPos(double pos)
        {

            ApsController.SetCommandPosition(NoId, Convert.ToInt32(pos / Transmission.PulseEquivalent));
            //ApsController.SetFeedbackPosition(NoId, pos);
        }
        /// <summary>
        ///     是否到位。
        /// </summary>
        public override bool IsInPosition(double pos)
        {
            bool i = ApsController.IsInp(NoId);
            bool i1 = (BackPos <= (pos + 0.15) && BackPos >= (pos - 0.15));
            return i & i1 && (CurrentSpeed == 0);
        }
    }
}