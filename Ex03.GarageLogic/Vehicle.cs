using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eTypeOfVehicle
        {
            Car,
            Motorcycle,
            Truck
        }

        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_EnergyPercent;
        private EnergySource m_EnergySource;
        private List<Wheel> m_Wheels;

        public Vehicle(string i_ModelName, string i_LicenseNumber, EnergySource.eSourceType i_EnergySource)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;           

            if (i_EnergySource.Equals(EnergySource.eSourceType.Gas))
            {
                m_EnergySource = new GasTank();
            }
            else
            {
                m_EnergySource = new Battery();
            }

            m_Wheels = new List<Wheel>();
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
            set { m_EnergySource = value; }
        }

        protected abstract void InitEnergySource();

        public void UpdateEnergyPercent()
        {
            m_EnergyPercent = (EnergySource.CurrentAmountOfEnergy / EnergySource.MaxAmountOfEnergy) * 100;
        }

        public override string ToString()
        {
            return string.Format("Vehicle license plate: {1}{0}Vehicle model name: {2}{0}Wheels information: {3}{0}Energy: {4}%{0}{5}",
                Environment.NewLine,
                m_LicenseNumber,
                m_ModelName,
                Wheels[0].ToString(),
                m_EnergyPercent,
                m_EnergySource.ToString());
        }
    }
}
