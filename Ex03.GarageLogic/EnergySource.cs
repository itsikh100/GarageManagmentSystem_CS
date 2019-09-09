using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public enum eSourceType
        {
            Gas,
            Battery
        }

        protected float m_CurrentAmountOfEnergy;
        protected float m_MaxAmountOfEnergy;

        public float CurrentAmountOfEnergy
        {
            get { return m_CurrentAmountOfEnergy; }
            set { m_CurrentAmountOfEnergy = value; }
        }

        public float MaxAmountOfEnergy
        {
            get { return m_MaxAmountOfEnergy; }
            set { m_MaxAmountOfEnergy = value; }
        }

        public void UpdateEnergy(float i_EnergyToUpdate)
        {
            if (i_EnergyToUpdate <= 0 || i_EnergyToUpdate + m_CurrentAmountOfEnergy > m_MaxAmountOfEnergy)
            {
                throw new ValueOutOfRangeException(MaxAmountOfEnergy - CurrentAmountOfEnergy, 0);	
            }

            m_CurrentAmountOfEnergy += i_EnergyToUpdate;
        }

        public abstract string CreateGetEnergyMsg();

        public abstract string CreateOutOfRangMsg();
    }
}
