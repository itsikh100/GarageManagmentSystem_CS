using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, ServiceDetails> m_ServiceDetailsList = new Dictionary<string, ServiceDetails>();

        public Dictionary<string, ServiceDetails> ServiceDetailsList
        {
            get { return m_ServiceDetailsList; }
        }

        public void ChangeStatusForService(string i_LicenseNumber, ServiceDetails.eCarStatus i_NewStatus)
        {
            m_ServiceDetailsList[i_LicenseNumber].CarStatus = i_NewStatus;
        }

        public void EnterNewCarForService(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNum)
        {
            ServiceDetails newTreatmentToAdd = new ServiceDetails(i_Vehicle, i_OwnerName, i_OwnerPhoneNum);

            m_ServiceDetailsList.Add(i_Vehicle.LicenseNumber, newTreatmentToAdd);
        }

        public void carExistInGarage(string i_LicenseNumber)
        {
            if (m_ServiceDetailsList.ContainsKey(i_LicenseNumber) == false)
            {
                throw new ArgumentException(
                    string.Format(
                    "the vehicle with the license plate {0} doesnt exists in the garage",
                    i_LicenseNumber));
            }
        }
    }
}
