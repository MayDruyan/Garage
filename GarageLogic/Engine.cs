using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public enum eEngineType
    {
        Fuel,
        Electric
    }

    public abstract class Engine
    {
        protected float m_CurrentEnergy;
        protected float m_MaxEnergy;

        public abstract void AddProperties(List<string> i_List);

        public float MaxEnergy
        {
            get
            {
                return m_MaxEnergy;
            }

            set
            {
                m_MaxEnergy = value;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }

        internal void AddEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd > m_MaxEnergy || m_CurrentEnergy + i_EnergyToAdd > m_MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergy);
            }

            try
            {
                m_CurrentEnergy += i_EnergyToAdd;
            }
            catch (FormatException)
            {
                Console.WriteLine("The type you entered is invalid. The energy should be entered as a float.");
            }
        }

        public bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isPropertyValid = false;
            float givenCurrentEnergy = 0;

            try
            {
                isPropertyValid = float.TryParse(i_InputFromUser, out givenCurrentEnergy);
            }
            catch (FormatException)
            {
                Console.WriteLine(string.Format("The value you entered {0}, isn't a float.", i_InputFromUser));
            }

            isPropertyValid = isPropertyValid && givenCurrentEnergy <= m_MaxEnergy && givenCurrentEnergy >= 0;

            if (isPropertyValid)
            {
                m_CurrentEnergy = givenCurrentEnergy;
            }

            return isPropertyValid;
        }

        internal string GetSpecificData()
        {
            StringBuilder engineData = new StringBuilder();
            engineData.AppendLine(string.Format("The current energy of the vehicle: {0}", m_CurrentEnergy));
            engineData.AppendLine(string.Format("The max energy of the vehicle: {0}", m_MaxEnergy));

            return engineData.ToString();
        }
    }
}
