using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_AmountOfWheels = 4;      

        public enum eColor
        {
            Red,
            Blue,
            Black,
            Gray
        }

        public enum eDoor
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private eColor m_Color;
        private eDoor m_AmountOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber, string i_WheelManufactur, EnergySource.eSourceType i_EnergySource) 
            : base(i_ModelName, i_LicenseNumber, i_EnergySource)
        {
            for(int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufactur, (float)Wheel.eMaxAirPressure.Car));
            }

            InitEnergySource();
        }

        protected override void InitEnergySource()
        {
            if(EnergySource is GasTank)
            {
                ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Octan96;
                EnergySource.MaxAmountOfEnergy = 55f;
            }
            else
            {
               EnergySource.MaxAmountOfEnergy = 1.8f;
            }
        }

        public eColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eDoor AmountOfDoors
        {
            get { return m_AmountOfDoors; }
            set { m_AmountOfDoors = value; }
        }

        public override string ToString()
        {
            return string.Format("{1}{0}Car's color: {2}{0}Amount of doors: {3}",
                Environment.NewLine,
                base.ToString(),
                m_Color.ToString(),
                m_AmountOfDoors.ToString());
        }
    }
}
