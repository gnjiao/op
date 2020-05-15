
using CMotion.Interfaces.IO;
using CMotion.Interfaces;
namespace AxisControlls
{
    public class DaskAllController : ISwitchController
    {
        /// <summary>
        /// 用于切换板卡型号
        /// </summary>
        public CardDaskName M_cardAxisName { get; set; }

        private IdaskControl daskController;

        public DaskAllController(CardDaskName cardAxisName)
        {
            M_cardAxisName = cardAxisName;
            switch (M_cardAxisName)
            {

                case CardDaskName.雷塞科技ADLink_PCI:
                    break;
                case CardDaskName.凌华科技7230:
                    daskController.Type = DASK.PCI_7230;                
                    daskController = new CMotion.AdlinkDash.DaskController();
                    break;
                case CardDaskName.凌华科技7432:
                    daskController.Type = DASK.PCI_7432;
                    daskController = new CMotion.AdlinkDash.DaskController();
                    break;
                default:
                    break;
            }

        }

        public void Initialize()
        {
            daskController.Initialize();
        }

        public bool Read(IoPoint ioPoint)
        {
            return daskController.Read(ioPoint);
        }

        public void Write(IoPoint ioPoint, bool value)
        {
            daskController.Write(ioPoint, value);
        }
    }

    public enum CardDaskName
    {
        雷塞科技ADLink_PCI,
        凌华科技7230,
        凌华科技7432,

    }
}
