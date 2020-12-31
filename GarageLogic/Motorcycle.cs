using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B
        }

        private readonly float r_MaxWheelsPressure = 30;
        private readonly int r_NumOfWheels = 2;
        private readonly float r_MaxEnergy = 1.2F;
        private readonly int r_MaxTank = 7;
        private readonly string r_LicenseTypeProperty = "license type (A, A1, AA, B):";
        private readonly string r_EngineCapacityProperty = "engine capacity:";
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(eEngineType i_EngineType)
        {
            m_Engine = i_EngineType == eEngineType.Fuel ? (Engine)new FuelEngine(FuelEngine.eFuelType.Octan95, r_MaxTank) : new ElectricEngine(r_MaxEnergy);
            m_Wheels = new List<Wheel>(r_NumOfWheels);
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(r_MaxWheelsPressure));
            }
        }

        internal override void AddProperties()
        {
            Properties.Add(r_LicenseTypeProperty);
            Properties.Add(r_EngineCapacityProperty);
        }

        internal override void BuildVehicle(Dictionary<string, string> i_Properties)
        {
            try
            {
                BuildBasicVehicle(i_Properties);
                m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_Properties[r_LicenseTypeProperty]);
                m_EngineCapacity = int.Parse(i_Properties[r_EngineCapacityProperty]);
                InitializeWheels(r_NumOfWheels);
            }
            catch (FormatException)
            {
                Console.WriteLine("The type you entered is invalid.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The value you entered is invalid.");
            }
        }

        public override bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isInputValid = base.ValidateAndSetParameters(i_Property, i_InputFromUser);

            if (i_Property.Equals(r_LicenseTypeProperty))
            {
                isInputValid = Enum.TryParse(i_InputFromUser, out m_LicenseType);
            }
            else if (i_Property.Equals(r_EngineCapacityProperty))
            {
                isInputValid = int.TryParse(i_InputFromUser, out m_EngineCapacity);
            }

            return isInputValid;
        }

        internal override string GetSpecificData()
        {
            StringBuilder motorcycleData = new StringBuilder();
            motorcycleData.AppendLine(string.Format("The motorcycle's license type: {0}", m_LicenseType.ToString()));
            motorcycleData.AppendLine(string.Format("The motorcycle's engine capacity: {0}", m_EngineCapacity));
            motorcycleData.Append(base.GetSpecificData());

            return motorcycleData.ToString();
        }
    }
}
