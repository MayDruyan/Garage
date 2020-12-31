using System.Collections.Generic;

namespace GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Client> m_InProgress;
        private Dictionary<string, Client> m_Repaired;
        private Dictionary<string, Client> m_Paid;

        public Garage()
        {
            m_InProgress = new Dictionary<string, Client>();
            m_Repaired = new Dictionary<string, Client>();
            m_Paid = new Dictionary<string, Client>();
        }

        public bool BuildAndAddVehicleToGarage(string i_ClientName, string i_PhoneNumber, Dictionary<string, string> i_VehicleSpecificParameters, Vehicle i_NewVehicleToAdd)
        {
            Vehicle vehicleToAdd = VehicleFactory.BuildVehicle(i_VehicleSpecificParameters, i_NewVehicleToAdd);
            Client clientToAdd = new Client(i_ClientName, i_PhoneNumber, vehicleToAdd, Client.eRepairStatus.InProgress);
            bool inGarage = CheckIfVehicleInGarage(clientToAdd.Vehicle.LicensePlate);

            if (!inGarage)
            {
                m_InProgress.Add(clientToAdd.Vehicle.LicensePlate, clientToAdd);
            }
            else
            {
                ChangeRepairStatus(clientToAdd.Vehicle.LicensePlate, Client.eRepairStatus.InProgress);
            }

            return inGarage;
        }

        public void ChangeRepairStatus(string i_LicensePlate, Client.eRepairStatus i_StatusToChangeTo)
        {
            bool vehicleInGarage;
            Client clientToMove = null;
            Dictionary<string, Client> dictionaryToRemoveFrom = getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);

            removeFromDictionary(dictionaryToRemoveFrom, i_LicensePlate, out clientToMove);
            Dictionary<string, Client> dictionaryToAddTo = i_StatusToChangeTo == Client.eRepairStatus.InProgress ? m_InProgress : m_Paid;
            dictionaryToAddTo = i_StatusToChangeTo == Client.eRepairStatus.Repaired ? m_Repaired : m_Paid;
            dictionaryToAddTo.Add(i_LicensePlate, clientToMove);
            clientToMove.RepairStatus = i_StatusToChangeTo;
        }

        private void removeFromDictionary(Dictionary<string, Client> i_DictionaryToRemoveFrom, string i_LicensePlate, out Client o_ClientToMove)
        {
            i_DictionaryToRemoveFrom.TryGetValue(i_LicensePlate, out o_ClientToMove);
            i_DictionaryToRemoveFrom.Remove(i_LicensePlate);
        }

        public void InflateVehicleWheels(string i_LicensePlate, float i_AirToAdd)
        {
            bool vehicleInGarage;
            Client currentClient;
            Dictionary<string, Client> dictionaryOfVehicle = getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);

            dictionaryOfVehicle.TryGetValue(i_LicensePlate, out currentClient);
            currentClient.Vehicle.InflateWheels(i_AirToAdd);
        }

        public void InflateVehicleWheelsToMaximum(string i_LicensePlate)
        {
            bool vehicleInGarage = false;
            Dictionary<string, Client> vehicleDictionary = getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);
            Vehicle vehicleToInflate = vehicleDictionary[i_LicensePlate].Vehicle;

            InflateVehicleWheels(i_LicensePlate, vehicleToInflate.Wheels[0].MaxAirPressure - vehicleToInflate.Wheels[0].CurrentAirPressure);
        }

        public void ReCharge(string i_LicensePlate, float i_EnergyToAdd)
        {
            bool vehicleInGarage = false;
            Client currentClient;
            Dictionary<string, Client> dictionaryByLicensePlate = getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);

            dictionaryByLicensePlate.TryGetValue(i_LicensePlate, out currentClient);
            currentClient.Vehicle.Engine.AddEnergy(i_EnergyToAdd);
        }

        public void ReFuel(string i_LicensePlate, float i_EnergyToAdd, FuelEngine.eFuelType i_FuelType)
        {
            bool vehicleInGarage = false;
            Client currentClient;
            Dictionary<string, Client> dictionaryByLicensePlate = getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);

            dictionaryByLicensePlate.TryGetValue(i_LicensePlate, out currentClient);
            FuelEngine fuelEngine = (FuelEngine) currentClient.Vehicle.Engine;
            fuelEngine.AddEnergy(i_FuelType, i_EnergyToAdd);
        }

        public bool CheckIfVehicleInGarage(string i_LicensePlate)
        {
            bool vehicleInGarage = false;

            getDictionaryByLicensePlate(i_LicensePlate, out vehicleInGarage);

            return vehicleInGarage;
        }

        public Dictionary<string, Client> GetDictionaryByStatus(Client.eRepairStatus i_RepairStatus)
        {
            Dictionary<string, Client> dictionaryToReturn = null;

            switch (i_RepairStatus)
            {
                case Client.eRepairStatus.InProgress:
                    dictionaryToReturn = m_InProgress;
                    break;
                case Client.eRepairStatus.Repaired:
                    dictionaryToReturn = m_Repaired;
                    break;
                case Client.eRepairStatus.Paid:
                    dictionaryToReturn = m_Paid;
                    break;
            }

            return dictionaryToReturn;
        }

        private Dictionary<string, Client> getDictionaryByLicensePlate(string i_LicensePlate, out bool o_VehicleInDictionaries)
        {
            Dictionary<string, Client> dictionaryByLicensePlate = null;

            if (o_VehicleInDictionaries = m_InProgress.ContainsKey(i_LicensePlate))
            {
                dictionaryByLicensePlate = m_InProgress;
            }
            else if (o_VehicleInDictionaries = m_Repaired.ContainsKey(i_LicensePlate))
            {
                dictionaryByLicensePlate = m_Repaired;
            }
            else if (o_VehicleInDictionaries = m_Paid.ContainsKey(i_LicensePlate))
            {
                dictionaryByLicensePlate = m_Paid;
            }

            return dictionaryByLicensePlate;
        }

        public Client GetClientByLicensePlate(string i_LicensePlate)
        {
            Dictionary<string, Client> vehiclesDictionary = getDictionaryByLicensePlate(i_LicensePlate, out bool IsVehicleInDictionary);
            Client clientToReturn = vehiclesDictionary[i_LicensePlate];

            return clientToReturn;
        }

        public bool IsGarageEmpty()
        {
            return m_InProgress.Count == 0 && m_Repaired.Count == 0 && m_Paid.Count == 0;
        }
    }
}
