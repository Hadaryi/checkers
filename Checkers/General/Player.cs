using System.Collections.Generic;

namespace Checkers
{
    public class Player
    {
        private string m_Name;
        private eType m_PlayerType;
        private bool m_IsComputer;
        private List<Soldier> m_Soldiers;
        private int m_CurrentGameScore;
        private int m_Score;

        public enum eType
        {
            PlayerOne = 'X',
            PlayerTwo = 'O'
        }

        /**
         * Constructor - creates a new Player with a given name and whether the player is played by the computer or not.
         */
        public Player(string i_Name, bool i_IsComputer = false)
        {
            m_Name = i_Name;
            m_IsComputer = i_IsComputer;
        }

        /**
         * Getter and setter for the player's name.
         */
        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        /**
         * Getter for the player's Soldier's type.
         */
        public eType PlayerType
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
         * Getter and setter for whether the player is played by the computer or not.
         */
        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }

            set
            {
                m_IsComputer = value;
            }
        }

        /**
         * Getter that fetches the player's soldiers.
         */
        public List<Soldier> Soldiers
        {
            get
            {
                return m_Soldiers;
            }

            set
            {
                m_Soldiers = value;
            }
        }

        /**
         * Getter and setter for the player's score for the current game.
         */
        public int CurrentGameScore
        {
            get
            {
                return m_CurrentGameScore;
            }

            set
            {
                m_CurrentGameScore = value;
            }
        }

        /**
         * Getter and setter for the player's total score.
         */
        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
    }
}