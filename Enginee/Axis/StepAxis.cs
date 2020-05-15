using AxisControlls;

namespace System.Enginee
{
    /// <summary>
    ///     雷塞
    /// </summary>
    public class StepAxis : ApsAxis
    {
        public StepAxis(AxisController apsController) : base(apsController)
        {
        }
        public override bool IsDone
        {
            get { return true; }
        }
        public override double CurrentPos
        {
            get
            {
                return ApsController.GetCurrentCommandPosition(NoId) * Transmission.PulseEquivalent;
            }
        }
      
        public override void SetCurrentPos(double pos)
        {          
            ApsController.SetCommandPosition(NoId, Convert.ToInt32(pos));
        }
        /// <summary>
        ///     是否原点
        /// </summary>
#pragma warning disable CS0108 // '“StepAxis.IsOrigin”隐藏继承的成员“ApsAxis.IsOrigin”。如果是有意隐藏，请使用关键字 new。
        public bool IsOrigin
#pragma warning restore CS0108 // '“StepAxis.IsOrigin”隐藏继承的成员“ApsAxis.IsOrigin”。如果是有意隐藏，请使用关键字 new。
        {
            get { return ApsController.IsOrg(NoId); }
        }

        
        /// <summary>
        ///     是否到位。
        /// </summary>
        public override bool IsInPosition(double pos)
        {
            bool I1 = (CurrentPos - 0.01 < pos && CurrentPos + 0.1 > pos);
            return ApsController.IsInp(NoId)  & (CurrentPos - 0.02 < pos && CurrentPos + 0.02 > pos) && (CurrentSpeed == 0);
        }
    }
}