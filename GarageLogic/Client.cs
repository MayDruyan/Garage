namespace GarageLogic
{
    public class Client
    {
        public enum eRepairStatus
        {
            InProgress = 1,
            Repaired = 2,
            Paid = 3
        }

        private string m_Name;
        private string m_PhoneNumber;
        private Vehicle m_Vehicle;
        private eRepairStatus m_RepairStatus;

        public Client(string i_Name, string i_PhoneNumber, Vehicle i_Vehicle, eRepairStatus i_VehicleRepairStatus)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
            m_Vehicle = i_Vehicle;
            m_RepairStatus = i_VehicleRepairStatus;
        }

        public string GetClientData()
        {
            string clientData = string.Format(
@"Vehicle Plate: {0}
Model name: {1}
Owner's name: {2}
Vehicle Status: {3}
{4}
",
m_Vehicle.LicensePlate,
m_Vehicle.Model,
m_Name,
m_RepairStatus,
m_Vehicle.GetSpecificData());

            return clientData;
        }

        public eRepairStatus RepairStatus
        {
            get
            {
                return m_RepairStatus;
            }

            set
            {
                m_RepairStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
    }
}
