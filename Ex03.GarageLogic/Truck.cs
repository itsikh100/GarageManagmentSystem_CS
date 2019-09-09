using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_AmountOfWheels = 12;
        private bool m_DangerousMaterials;
        private float m_TrunkVolume;

        public Truck(string i_ModelName, string i_LicenseNumber, string i_WheelManufactur, EnergySource.eSourceType i_EnergySource)
            : base(i_ModelName, i_LicenseNumber, i_EnergySource)
        {
            for (int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufactur, (float)Wheel.eMaxAirPressure.Truck));
            }

            InitEnergySource();
        }

        protected override void InitEnergySource()
        {
            ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Soler;
            EnergySource.MaxAmountOfEnergy = 110f;         
        }

        public bool DangerousMaterials
        {
            get { return m_DangerousMaterials; }
            set { m_DangerousMaterials = value; }
        }

        public float TrunkVolume
        {
            get { return m_TrunkVolume; }
            set { m_TrunkVolume = value; }
        }

        public override string ToString()
        {
            return string.Format("{1}{0}Dangerous Materials: {2}{0}Trunk Volume: {3}",
                Environment.NewLine,
                base.ToString(),
                m_DangerousMaterials.ToString(),
                m_TrunkVolume.ToString());
        }
    }
}
