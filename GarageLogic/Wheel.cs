using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerProperty = "wheel manufacturer:";
        private readonly string r_CurrentAirPressureProperty = "wheels' current air pressure:";
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        internal void InflateWheel(float i_AirToAdd)
        {            
            if (i_AirToAdd > m_MaxAirPressure || m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }

            try
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            catch (FormatException)
            {
                Console.WriteLine("The type you entered is invalid. The air pressure should be entered as a float.");
            }
        }

        public void AddProperties(List<string> i_List)
        {
            i_List.Add(r_ManufacturerProperty);
            i_List.Add(r_CurrentAirPressureProperty);
        }

        public bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isInputValid = false;
            float inputAsFloat = 0;

            if (i_Property.Equals(r_ManufacturerProperty))
            {
                isInputValid = i_InputFromUser.All(char.IsLetter);
                if (isInputValid)
                {
                    m_Manufacturer = i_InputFromUser;
                }
            }

            if (i_Property.Equals(r_CurrentAirPressureProperty))
            {
                isInputValid = float.TryParse(i_InputFromUser, out inputAsFloat);

                if (isInputValid)
                {
                    isInputValid = inputAsFloat >= 0 && inputAsFloat <= m_MaxAirPressure;
                }
            }

            if (isInputValid)
            {
                m_CurrentAirPressure = inputAsFloat <= m_MaxAirPressure ? inputAsFloat : 0;
            }

            return isInputValid;
        }

        internal string GetSpecificData()
        {
            StringBuilder wheelData = new StringBuilder();
            wheelData.AppendLine(string.Format("The wheels manufacturer: {0}", m_Manufacturer));
            wheelData.AppendLine(string.Format("The current pressure of the wheels: {0}", m_CurrentAirPressure));
            wheelData.AppendLine(string.Format("The max pressure of the wheels: {0}", m_MaxAirPressure));

            return wheelData.ToString();
        }
    }
}
