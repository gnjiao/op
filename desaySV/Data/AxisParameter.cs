using System;
using CMotion.Interfaces.Axis;
using CMotion.Interfaces.Configuration;
namespace desaySV
{

    /// <summary>
    /// 各轴参数
    /// </summary>
    [Serializable]
    public class AxisParameter
    {
        [NonSerialized]
        public static AxisParameter Instance = new AxisParameter();

        public AxisSpeed XaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax=400.000,
            velocityRate=20,
            HomeAddSpeed=50,
            HomestartSpeed=10,
            HomeRunSpeed=1000
        };

        public AxisSpeed YaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed ZaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed LFXaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed LFYaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed LRXaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed LRYaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
        public AxisSpeed RYaxisSpeed = new AxisSpeed()
        {
            startSpeed = 10,
            AddSpeed = 50,
            RunSpeed = 1000,
            velocityMax = 400.000,
            velocityRate = 20,
            HomeAddSpeed = 50,
            HomestartSpeed = 10,
            HomeRunSpeed = 1000
        };
       
        //速度参数
        public VelocityCurve XVelocityCurve
        {
            get
            {              
                return new VelocityCurve(XaxisSpeed.startSpeed, XaxisSpeed.RunSpeed, XaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve YVelocityCurve
        {
            get
            {
                return new VelocityCurve(YaxisSpeed.startSpeed, YaxisSpeed.RunSpeed, YaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve ZVelocityCurve
        {
            get
            {
                return new VelocityCurve(ZaxisSpeed.startSpeed, ZaxisSpeed.RunSpeed, ZaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve LFXVelocityCurve
        {
            get
            {
                return new VelocityCurve(LFXaxisSpeed.startSpeed, LFXaxisSpeed.RunSpeed, LFXaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve LFYVelocityCurve
        {
            get
            {
                return new VelocityCurve(LFYaxisSpeed.startSpeed, LFYaxisSpeed.RunSpeed, LFYaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve LRXVelocityCurve
        {
            get
            {
                return new VelocityCurve(LRXaxisSpeed.startSpeed, LRXaxisSpeed.RunSpeed, LRXaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve LRYVelocityCurve
        {
            get
            {
                return new VelocityCurve(LRYaxisSpeed.startSpeed, LRYaxisSpeed.RunSpeed, LRYaxisSpeed.AddSpeed);
            }
        }
        public VelocityCurve RYVelocityCurve
        {
            get
            {
                return new VelocityCurve(RYaxisSpeed.startSpeed, RYaxisSpeed.RunSpeed, RYaxisSpeed.AddSpeed);
            }
        }
        //速度参数
        public VelocityCurve HomeXVelocityCurve
        {
            get
            {
                return new VelocityCurve(XaxisSpeed.HomestartSpeed, XaxisSpeed.HomeRunSpeed, XaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeYVelocityCurve
        {
            get
            {
                return new VelocityCurve(YaxisSpeed.HomestartSpeed, YaxisSpeed.HomeRunSpeed, YaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeZVelocityCurve
        {
            get
            {
                return new VelocityCurve(ZaxisSpeed.HomestartSpeed, ZaxisSpeed.HomeRunSpeed, ZaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeLFXVelocityCurve
        {
            get
            {
                return new VelocityCurve(LFXaxisSpeed.HomestartSpeed, LFXaxisSpeed.HomeRunSpeed, LFXaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeLFYVelocityCurve
        {
            get
            {
                return new VelocityCurve(LFYaxisSpeed.HomestartSpeed, LFYaxisSpeed.HomeRunSpeed, LFYaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeLRXVelocityCurve
        {
            get
            {
                return new VelocityCurve(LRXaxisSpeed.HomestartSpeed, LRXaxisSpeed.HomeRunSpeed, LRXaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeLRYVelocityCurve
        {
            get
            {
                return new VelocityCurve(LRYaxisSpeed.HomestartSpeed, LRYaxisSpeed.HomeRunSpeed, LRYaxisSpeed.HomeAddSpeed);
            }
        }
        public VelocityCurve HomeRYVelocityCurve
        {
            get
            {
                return new VelocityCurve(RYaxisSpeed.HomestartSpeed, RYaxisSpeed.HomeRunSpeed, RYaxisSpeed.HomeAddSpeed);
            }
        }
        
        public TransmissionParams XTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams YTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams ZTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams LFXTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams LFYTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams LRXTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams LRYTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
        public TransmissionParams RYTransParams { get; set; } = new TransmissionParams() { Lead = 1, SubDivisionNum = 1 };
       
    }


}


