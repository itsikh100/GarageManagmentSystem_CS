using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            A2,
            B
        }

        private const int k_AmountOfWheels = 2;
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, string i_WheelManufactur, EnergySource.eSourceType i_EnergySource)
            : base(i_ModelName, i_LicenseNumber, i_EnergySource)
        {
            for (int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufactur, (float)Wheel.eMaxAirPressure.Motorcycle));
            }

            InitEnergySource();
        }

        protected override void InitEnergySource()
        {
            if (EnergySource is GasTank)
            {
                ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Octan95;
                EnergySource.MaxAmountOfEnergy = 8f;
            }
            else
            {
                EnergySource.MaxAmountOfEnergy = 1.4f;
            }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public override string ToString()
        {
            return string.Format("{1}{0}License type: {2}",
                Environment.NewLine,
                base.ToString(),
                m_LicenseType.ToString());             
        }
    }
}