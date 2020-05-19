using System;

namespace Checkers
{
    public class Soldier
    {
        private eType m_SoldierType;
        private Player.eType m_PlayerType;
        private CheckersCoordinates m_Location;
        private Player m_Player;

        public event Action<Soldier> BecameKing;

        public enum eType
        {
            PlayerOne = 'X',
            PlayerTwo = 'O',
            PlayerOneKing = 'K',
            PlayerTwoKing = 'U'
        }

        /**
         * Constructor - creates a new soldier with a given set of coordinates and its type.
         */
        public Soldier(eType i_SoldierType, CheckersCoordinates i_Location)
        {
            m_SoldierType = i_SoldierType;
            m_Location = i_Location;

            initPlayerType();
        }

        /**
         * This method initializes the soldier's player type according to the soldier's type.
         */
        private void initPlayerType()
        {
            switch (m_SoldierType)
            {
                case eType.PlayerOne:
                case eType.PlayerOneKing:
                    m_PlayerType = Player.eType.PlayerOne;

                    break;

                case eType.PlayerTwo:
                case eType.PlayerTwoKing:
                    m_PlayerType = Player.eType.PlayerTwo;

                    break;
            }
        }

        private void OnBecameKing()
        {
            if (BecameKing != null)
            {
                BecameKing.Invoke(this);
            }
        }

        /**
         * This method converts the soldier to a king.
         */
        public void MakeKing()
        {
            if (m_SoldierType == eType.PlayerOne)
            {
                m_SoldierType = eType.PlayerOneKing;
            }
            else if (m_SoldierType == eType.PlayerTwo)
            {
                m_SoldierType = eType.PlayerTwoKing;
            }

            OnBecameKing();
        }

        /**
         * This method returns whether the Soldier is a king or not.
         */
        public bool IsKing()
        {
            return m_SoldierType == eType.PlayerOneKing || m_SoldierType == eType.PlayerTwoKing;
        }

        /**
         * This method returns the character that represents the Soldier.
         */
        public char GetChar()
        {
            return (char)m_SoldierType;
        }

        /**
         * Getter for the Soldier's type.
         */
        public eType SoldierType
        {
            get
            {
                return m_SoldierType;
            }
        }

        /**
         * Getter and setter for the Soldier's player type.
         */
        public Player.eType PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value;
            }
        }

        /**
         * Getter and setter for the Soldier's location.
         */
        public CheckersCoordinates Location
        {
            get
            {
                return m_Location;
            }

            set
            {
                m_Location = value;
            }
        }

        /**
         * Getter and setter for the Soldier's player.
         */
        public Player Player
        {
            get
            {
                return m_Player;
            }

            set
            {
                m_Player = value;
            }
        }
    }
}