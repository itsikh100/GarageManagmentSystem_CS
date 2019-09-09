using System;
using System.Collections.Generic;
using System.Text;

using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManagement
    {
        public enum eDisplayOption
        {
            AllVehicles = 1,
            SpecificStatus,
        }

        private Garage m_Garage = new Garage();
        private UserInterface m_UI = new UserInterface();

        public Garage Garage
        {
            get
            {
                return m_Garage;
            }

            set
            {
                m_Garage = value;
            }
        }

        public UserInterface UI
        {
            get
            {
                return m_UI;
            }

            set
            {
                m_UI = value;
            }
        }

        public void Run()
        {
            UserInterface.eUserChoice userChoice;
            bool exitProgram = false;

            while (!exitProgram)
            {
                UI.PrintToScreen(Environment.NewLine);
                try
                {
                    userChoice = UI.PrintMenuAndGetChoice();
                    switch (userChoice)
                    {
                        case UserInterface.eUserChoice.InsertVehicleToGarage:
                            insertVehicleToGarage();
                            break;

                        case UserInterface.eUserChoice.DisplayLicensePlates:
                            displayVehiclesInGarageLicensePlates();
                            break;

                        case UserInterface.eUserChoice.ChangeVehicleStatus:
                            changeVehicleTreatmentStatus();
                            break;

                        case UserInterface.eUserChoice.InflateWheels:
                            inflateWheelsToMax();
                            break;

                        case UserInterface.eUserChoice.FillEnergySource:
                            fillEnergySource();
                            break;

                        case UserInterface.eUserChoice.DisplayFullVehicleData:
                            displayFullVehicleData();
                            break;

                        case UserInterface.eUserChoice.Exit:
                            exitProgram = true;
                            break;

                        default:
                            UI.InvalidInputTryAgainMsg();
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    UI.PrintToScreen(ae.Message);
                }
                catch (FormatException)
                {
                    UI.InvalidInputTryAgainMsg();
                }
                catch (OverflowException)
                {
                    UI.SomethingWentWrongMsg();
                }
            }
        }

        private void displayFullVehicleData()
        {
            string licensePlate;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            UI.PrintToScreen(UI.CreateSpaceIfNeeded(m_Garage.ServiceDetailsList[licensePlate].ToString()));
        }

        private void fillEnergySource()
        {
            string licensePlate;
            GasTank.eFuelType fuelOptions = new GasTank.eFuelType();
            string PartOfOptionsHeaderMsg = string.Format("fuel type");
            GasTank.eFuelType fuelType;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            GasTank gasTank = m_Garage.ServiceDetailsList[licensePlate].Vehicle.EnergySource as GasTank;
            if (gasTank != null)
            {
                fuelType = (GasTank.eFuelType)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, fuelOptions);
                gasTank.CheckFuelType(fuelType);
            }

            insertAmountOfEnergyToAdd(m_Garage.ServiceDetailsList[licensePlate].Vehicle);
        }

        private void inflateWheelsToMax()
        {
            string licensePlate;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            foreach (Wheel currentWheel in m_Garage.ServiceDetailsList[licensePlate].Vehicle.Wheels)
            {
                currentWheel.InflateWheel(currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure);
            }
        }

        private void changeVehicleTreatmentStatus()
        {
            int userChoice;
            string licensePlate;
            ServiceDetails.eCarStatus statusOptions = new ServiceDetails.eCarStatus();
            string PartOfOptionsHeaderMsg = string.Format("to which treatment status you want to change");

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, statusOptions);

            m_Garage.ServiceDetailsList[licensePlate].CheckEqualStatus((ServiceDetails.eCarStatus)userChoice);
            m_Garage.ServiceDetailsList[licensePlate].CarStatus = (ServiceDetails.eCarStatus)userChoice;
        }

        private void displayVehiclesInGarageLicensePlates()
        {
            ServiceDetails.eCarStatus statusOptions = new ServiceDetails.eCarStatus();
            eDisplayOption displayOption = new eDisplayOption();
            int displayChoice;
            int userChoice = 0;
            string displayOptionMsg = string.Format("how to want to filter your search");
            string PartOfOptionsHeaderMsg = string.Format("which vehicels you want to see");
            bool displayAll = false;

            displayChoice = UI.GetSpecificEnumInput(displayOptionMsg, displayOption);
            if ((eDisplayOption)displayChoice == eDisplayOption.AllVehicles)
            {
                displayAll = true;
            }
            else
            {
                userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, statusOptions);
            }

            UI.PrintLicensePlates(userChoice, m_Garage.ServiceDetailsList, displayAll);
        }

        private void insertVehicleToGarage()
        {
            bool isVehicleExists = true;
            string licensePlate = null, modelName, wheelManufacturer, ownerName, ownerPhone;
            Vehicle newVehicle;

            try
            {
                UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            }
            catch (ArgumentException)
            {
                isVehicleExists = false;
            }

            if (isVehicleExists)
            {
                UI.VehicleIsAlreadyExsistsMsg();
                m_Garage.ChangeStatusForService(licensePlate, ServiceDetails.eCarStatus.InService);
            }
            else
            {
                modelName = UI.GetVehicleModelName();
                wheelManufacturer = UI.GetWheelManufacturer();
                newVehicle = createNewVehicle(licensePlate, modelName, wheelManufacturer, out ownerName, out ownerPhone);
                m_Garage.EnterNewCarForService(newVehicle, ownerName, ownerPhone);
            }
        }

        private Vehicle createNewVehicle(
            string i_LicensePlate,
            string i_ModelName,
            string i_WheelManufacturer,
            out string o_OwnerName,
            out string o_OwnerPhone)
        {
            Vehicle newVehicle;
            Vehicle.eTypeOfVehicle vehicleType, vehicleOptions = new Vehicle.eTypeOfVehicle();
            EnergySource.eSourceType vehicleEnergySource;
            EnergySource.eSourceType energySourceOptions = new EnergySource.eSourceType();
            string PartOfOptionsHeaderMsg = string.Format("vehicle type");

            o_OwnerName = UI.GetOwnerName();
            o_OwnerPhone = UI.GetOwnerPhone();

            vehicleType = (Vehicle.eTypeOfVehicle)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, vehicleOptions);
            PartOfOptionsHeaderMsg = string.Format("energy source");
            if (vehicleType != Vehicle.eTypeOfVehicle.Truck)
            {
                vehicleEnergySource = (EnergySource.eSourceType)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, energySourceOptions);
            }
            else
            {
                vehicleEnergySource = EnergySource.eSourceType.Gas;
            }

            newVehicle = VehicleFactory.CreateVehicle(vehicleType, vehicleEnergySource, i_LicensePlate, i_ModelName, i_WheelManufacturer);

            insertVehicleDetails(newVehicle);

            return newVehicle;
        }

        private void insertVehicleDetails(Vehicle i_NewVehicle)
        {
            if (i_NewVehicle is Motorcycle)
            {
                insertLicenseType((Motorcycle)i_NewVehicle);
            }
            else if (i_NewVehicle is Car)
            {
                insertColor((Car)i_NewVehicle);
                insertAmountOfDoors((Car)i_NewVehicle);
            }
            else
            {
                insertVolumeOfCargo((Truck)i_NewVehicle);
            }

            insertAmountOfEnergyToAdd(i_NewVehicle);
        }

        private void insertAmountOfEnergyToAdd(Vehicle i_NewVehicle)
        {
            string input;
            float AmountOfEnergyToEnter;
            bool isValidInput = true;

            do
            {
                try
                {
                    input = Console.ReadLine();
                    AmountOfEnergyToEnter = float.Parse(input);
                    i_NewVehicle.EnergySource.UpdateEnergy(AmountOfEnergyToEnter);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    UI.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
                catch (ValueOutOfRangeException)
                {
                    UI.PrintToScreen(string.Format(
@"{0}
please enter the amount to add again",
i_NewVehicle.EnergySource.CreateOutOfRangMsg()));
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            i_NewVehicle.UpdateEnergyPercent();
        }

        private void insertVolumeOfCargo(Truck i_NewTruck)
        {
            int volumeOfCargo;

            volumeOfCargo = UI.GetVolumeOfCargo();
            i_NewTruck.TrunkVolume = volumeOfCargo;
        }

        private void insertAmountOfDoors(Car i_NewCar)
        {
            int userChoice;
            Car.eDoor amountOfDoorsOptions = new Car.eDoor();
            string PartOfOptionsHeaderMsg = string.Format("amount of doors");

            userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, amountOfDoorsOptions);
            i_NewCar.AmountOfDoors = (Car.eDoor)userChoice;
        }

        private void insertColor(Car i_NewCar)
        {
            int color;
            Car.eColor colorOptions = new Car.eColor();
            string PartOfOptionsHeaderMsg = string.Format("car's color");

            color = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, colorOptions);
            i_NewCar.Color = (Car.eColor)color;
        }   

        private void insertLicenseType(Motorcycle i_NewMotorcycle)
        {
            int licenseType;
            Motorcycle.eLicenseType licenseOptions = new Motorcycle.eLicenseType();
            string PartOfOptionsHeaderMsg = string.Format("license type");

            licenseType = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, licenseOptions);
            i_NewMotorcycle.LicenseType = (Motorcycle.eLicenseType)licenseType;
        }
    }
}