using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected string m_LicensePlate;
        protected float m_EnergyMeter;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;
        private static readonly string r_VehicleModelProperty = "vehicle model:";
        private static readonly string r_LicensePlateProperty = "vehicle's license plate:";
        protected List<string> m_Properties = new List<string>()
        {
            r_VehicleModelProperty, r_LicensePlateProperty
        };

        internal abstract void BuildVehicle(Dictionary<string, string> i_Properties);

        internal abstract void AddProperties();

        public List<string> Properties
        {
            get
            {
                return m_Properties;
            }
        }

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        internal void BuildBasicVehicle(Dictionary<string, string> i_Properties)
        {
            try
            {
                m_Model = i_Properties[r_VehicleModelProperty];
                m_LicensePlate = i_Properties[r_LicensePlateProperty];
            }
            catch (FormatException)
            {
                Console.WriteLine("The type you entered is invalid, please enter a string.");
            }
        }

        internal void SetEnergyMeter()
        {
            m_EnergyMeter = (m_Engine.CurrentEnergy / m_Engine.MaxEnergy) * 100;
        }

        internal void InitializeWheels(int i_NumOfWheels)
        {
            if (m_Wheels[0].CurrentAirPressure > m_Wheels[0].MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_Wheels[0].MaxAirPressure);
            }

            for (int i = 1; i < i_NumOfWheels; i++)
            {
                m_Wheels[i].CurrentAirPressure = m_Wheels[0].CurrentAirPressure;
                m_Wheels[i].Manufacturer = m_Wheels[0].Manufacturer;
            }
        }

        internal void InflateWheels(float i_AirToAdd)
        {
            if (i_AirToAdd > m_Wheels[0].MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_Wheels[0].MaxAirPressure);
            }

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateWheel(i_AirToAdd);
            }
        }

        public virtual bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isInputValid = false;

            if (i_Property.Equals(r_VehicleModelProperty))
            {
                isInputValid = i_InputFromUser.All(char.IsLetterOrDigit);
                if (isInputValid)
                {
                    m_Model = i_InputFromUser;
                }
            }
            else if (i_Property.Equals(r_LicensePlateProperty))
            {
                isInputValid = i_InputFromUser.All(char.IsLetterOrDigit);
                if (isInputValid)
                {
                    m_LicensePlate = i_InputFromUser;
                }
            }

            return isInputValid;
        }

        internal virtual string GetSpecificData()
        {
            StringBuilder vehicleData = new StringBuilder();
            vehicleData.Append(m_Engine.GetSpecificData());
            vehicleData.Append(m_Wheels[0].GetSpecificData());

            return vehicleData.ToString();
        }
    }
}
