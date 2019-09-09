using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(
            Vehicle.eTypeOfVehicle i_TypeOfVehicle, 
            EnergySource.eSourceType i_EnergySource, 
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelManufacturer)
        {
            Vehicle typeOfVehicle = null;
            switch (i_TypeOfVehicle)
            {
                case Vehicle.eTypeOfVehicle.Car:
                    typeOfVehicle = new Car(i_ModelName, i_LicenseNumber, i_WheelManufacturer, i_EnergySource);
                    break;

                case Vehicle.eTypeOfVehicle.Motorcycle:
                    typeOfVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_WheelManufacturer, i_EnergySource);
                    break;

                case Vehicle.eTypeOfVehicle.Truck:
                    typeOfVehicle = new Truck(i_ModelName, i_LicenseNumber, i_WheelManufacturer, i_EnergySource);
                    break;
            }

            return typeOfVehicle;
        }
    }
}
