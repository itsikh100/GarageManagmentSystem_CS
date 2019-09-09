using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasTank : EnergySource
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        private eFuelType m_FuelType;

        public void CheckFuelType(eFuelType i_FuelType)
        {
            if (!i_FuelType.Equals(m_FuelType))
            {
                throw new ArgumentException();
            }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override string ToString()
        {
            return string.Format("Current amount of fuel: {1}{0}Maximum amount of fuel: {2}", 
                Environment.NewLine, 
                CurrentAmountOfEnergy, 
                MaxAmountOfEnergy);
        }

        public override string CreateGetEnergyMsg()
        {
            return "Please enter amount of FUEL you want to add";
        }

        public override string CreateOutOfRangMsg()
        {
            string result;

            result = string.Format(
@"Amount of fuel in the gas tank was about to go out of range
you have {0} liters of gas in your gas tank at this moment 
and at most you can fill up to {1} liters.",
            CurrentAmountOfEnergy,
            MaxAmountOfEnergy);

            return result;
        }
    }
}
