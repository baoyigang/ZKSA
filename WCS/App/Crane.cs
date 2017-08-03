using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace App
{
    public class Crane
    {
        public string CraneNo { get; set; }
        private int _mode = 0;
        public int Mode { get { return _mode; } set { _mode = value; } }
        private int _AlarmCode = 0;
        public int AlarmCode { get { return _AlarmCode; } set { _AlarmCode = value; } }
        private object[] station = new object[] { 0, 0 };
        public object[] Station { get { return station; } set { station = value; } }
        private string _taskno = "";
        public string TaskNo { get { return _taskno; } set { _taskno = value; } }
        private int _state = 0;
        public int State { get { return _state; } set { _state = value; } }
        private int _fork = 0;
        public int Fork { get { return _fork; } set { _fork = value; } }
    }

    public delegate void CraneEventHandler(CraneEventArgs args);
    public class CraneEventArgs
    {
        private Crane _crane;
        public Crane crane
        {
            get
            {
                return _crane;
            }
        }
        public CraneEventArgs(Crane crane)
        {
            this._crane = crane;
        }
    }
    public class Cranes
    {
        public static event CraneEventHandler OnCrane = null;

        public static void CraneInfo(Crane crane)
        {
            if (OnCrane != null)
            {
                OnCrane(new CraneEventArgs(crane));
            }
        }
    }

    public class Car
    {
        public string CarNo { get; set; }
        public object[] Status { get; set; }
        public string TaskNo { get; set; }
        public int AlarmCode { get; set; }
    }

    public delegate void CarEventHandler(CarEventArgs args);
    public class CarEventArgs
    {
        private Car _car;
        public Car car
        {
            get
            {
                return _car;
            }
        }
        public CarEventArgs(Car car)
        {
            this._car = car;
        }
    }
    public class Cars
    {
        public static event CarEventHandler OnCar = null;

        public static void CarInfo(Car car)
        {
            if (OnCar != null)
            {
                OnCar(new CarEventArgs(car));
            }
        }
    }
    public class Conveyor
    {
        public string ctlName { get; set; }
        public string ID { get; set; }
        public string value { get; set; }
        public string BarCode { get; set; }
    }

    public delegate void ConveyorEventHandler(ConveyorEventArgs args);
    public class ConveyorEventArgs
    {
        private Conveyor _conveyor;
        public Conveyor conveyor
        {
            get
            {
                return _conveyor;
            }
        }
        public ConveyorEventArgs(Conveyor conveyor)
        {
            this._conveyor = conveyor;
        }
    }
    public class Conveyors
    {
        public static event ConveyorEventHandler OnConveyor = null;

        public static void ConveyorInfo(Conveyor conveyor)
        {
            if (OnConveyor != null)
            {
                OnConveyor(new ConveyorEventArgs(conveyor));
            }
        }
    }

    public class Miniload
    {
        public string MiniloadNo { get; set; }
        private int _mode = 0;
        public int Mode { get { return _mode; } set { _mode = value; } }
        private int _AlarmCode = 0;
        public int AlarmCode { get { return _AlarmCode; } set { _AlarmCode = value; } }
        private object[] station = new object[] { 0, 0 };
        public object[] Station { get { return station; } set { station = value; } }
      
        private string _taskno1 = "";
        public string TaskNo1 { get { return _taskno1; } set { _taskno1 = value; } }
        private int _state1 = 0;
        public int State1 { get { return _state1; } set { _state1 = value; } }
        private int _fork1 = 0;
        public int Fork1 { get { return _fork1; } set { _fork1 = value; } }

        private string _taskno2 = "";
        public string TaskNo2 { get { return _taskno2; } set { _taskno2 = value; } }
        private int _state2 = 0;
        public int State2 { get { return _state2; } set { _state2 = value; } }
        private int _fork2 = 0;
        public int Fork2 { get { return _fork2; } set { _fork2 = value; } }
    }

    public delegate void MiniloadEventHandler(MiniloadEventArgs args);
    public class MiniloadEventArgs
    {
        private Miniload _miniload;
        public Miniload miniload
        {
            get
            {
                return _miniload;
            }
        }
        public MiniloadEventArgs(Miniload miniload)
        {
            this._miniload = miniload;
        }
    }
    public class Miniloads
    {
        public static event MiniloadEventHandler OnMiniload = null;

        public static void MiniloadInfo(Miniload miniload)
        {
            if (OnMiniload != null)
            {
                OnMiniload(new MiniloadEventArgs(miniload));
            }
        }
    }
}
