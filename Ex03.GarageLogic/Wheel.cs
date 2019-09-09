using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public enum eMaxAirPressure
        {
            Car = 31,
            Motorcycle = 33,
            Truck = 26       
        }

        private readonly string r_ManufacturerName;
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void InflateWheel(float i_AirToFeel)
        {
            if (i_AirToFeel <= 0 || i_AirToFeel + m_CurrentAirPressure > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(MaxAirPressure - CurrentAirPressure, 0);
            }

            m_CurrentAirPressure += i_AirToFeel;
        }

        public override string ToString()
        {
            string WheelDetails = string.Format(
                                    "Manufacturer Name: {1}{0}Current Air Pressure: {2}{0}Max Air Pressure: {3}{0}",
                                    Environment.NewLine,
                                    r_ManufacturerName,
                                    m_CurrentAirPressure,
                                    r_MaxAirPressure);

            return WheelDetails;
        }
    }
}
