using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ServiceDetails
    {
        public enum eCarStatus
        {
            InService,
            Fixed,
            Payed
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNum;
        private eCarStatus m_CarStatus;
        private Vehicle m_Vehicle;

        public ServiceDetails(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNum)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNum = i_OwnerPhoneNum;
            m_Vehicle = i_Vehicle;
            m_CarStatus = eCarStatus.InService;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNum
        {
            get { return m_OwnerPhoneNum; }
            set { m_OwnerPhoneNum = value; }
        }

        public eCarStatus CarStatus
        {
            get { return m_CarStatus; }
            set { m_CarStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public static bool IsValidPhoneNumber(string i_inputFromUser)
        {
            long.Parse(i_inputFromUser);
            if (i_inputFromUser.Length > 10)
            {
                throw new ValueOutOfRangeException(9, 10);
            }

            return true;
        }

        public void CheckEqualStatus(eCarStatus userChoice)
        {
            if ((ServiceDetails.eCarStatus)userChoice == m_CarStatus)
            {
                throw new ArgumentException(
                    string.Format(
                    "The vehicle is already in {0} status",
                    m_CarStatus));
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string TreatmentDetails;

            TreatmentDetails = string.Format(
@"Owner name: {0}
Owner phone: {1}
Vehicle status: {2}
",
            m_OwnerName,
            m_OwnerPhoneNum,
            m_CarStatus.ToString());
            result.Append(TreatmentDetails);
            result.Append(m_Vehicle.ToString());

            return result.ToString();
        }
    }
}
