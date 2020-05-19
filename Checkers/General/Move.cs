using System;
using System.Text;

namespace Checkers
{
    public class Move
    {
        // Constants declarations.
        public const byte k_NormalMovesOffset = 1;
        public const byte k_JumpMovesOffset = 2;
        public const byte k_MoveStringSize = 5;
        public const byte k_PossibleDiagonalMoves = 4;

        private CheckersCoordinates m_CurrentLocation;
        private CheckersCoordinates m_TargetLocation;
        private Player m_Player;
        private bool m_HasDoubleJump;
        private bool m_IsJumpMove;

        /**
         * Constructor - creates a new move object using a given current location and target location.
         */
        public Move(CheckersCoordinates i_CurrentLocation, CheckersCoordinates i_TargetLocation)
        {
            m_CurrentLocation = i_CurrentLocation;
            m_TargetLocation = i_TargetLocation;
            m_HasDoubleJump = false;

            initIsJumpMove();
        }

        /**
         * This method sets whether the Move represents a jump move.
         */
        private void initIsJumpMove()
        {
            int columnsDelta = m_TargetLocation.Column - m_CurrentLocation.Column;

            m_IsJumpMove = Math.Abs(columnsDelta) == k_JumpMovesOffset;
        }

        /**
         * This method receives a string, a checkers board's size and an output parameter.
         * The method then validates the string given whether it represents a valid move string and if so it returns true and returns the move object
         * as the output parameter.
         * The move string must be in the following format:
         * [A-Z][a-z]>[A-Z][a-z]
         */
        public static bool TryParse(string i_MoveString, byte i_BoardSize, out Move o_Parsed)
        {
            bool isValid = true;
            char maxAllowedColumnChar = (char)('A' + i_BoardSize);
            char maxAllowedRowChar = (char)('a' + i_BoardSize);

            o_Parsed = null;

            if (i_MoveString.Length != k_MoveStringSize)
            {
                isValid = false;
            }
            else if (i_MoveString[2] != '>')
            {
                isValid = false;
            }
            else if (i_MoveString[0] < 'A' || i_MoveString[0] > maxAllowedColumnChar || i_MoveString[3] < 'A' || i_MoveString[3] > maxAllowedColumnChar)
            {
                isValid = false;
            }
            else if (i_MoveString[1] < 'a' || i_MoveString[1] > maxAllowedRowChar || i_MoveString[4] < 'a' || i_MoveString[3] > maxAllowedRowChar)
            {
                isValid = false;
            }
            else
            {
                o_Parsed = Parse(i_MoveString);
            }

            return isValid;
        }

        /**
         * This method receives a valid string which represents a Move object and returns the Move object that the string represents.
         */
        public static Move Parse(string i_MoveString)
        {
            char currentColumn = i_MoveString[0];
            char targetColumn = i_MoveString[3];
            char currentRow = i_MoveString[1];
            char targetRow = i_MoveString[4];

            CheckersCoordinates currentLocation = new CheckersCoordinates(currentColumn, currentRow);
            CheckersCoordinates targetLocation = new CheckersCoordinates(targetColumn, targetRow);
            Move parsed = new Move(currentLocation, targetLocation);

            return parsed;
        }

        /**
         * This method returns a string that represents the Move object.
         */
        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            char currentCol = m_CurrentLocation.Column;
            char currentRow = m_CurrentLocation.Row;
            char targetCol = m_TargetLocation.Column;
            char targetRow = m_TargetLocation.Row;

            returnString.AppendFormat("{0}{1}>{2}{3}", currentCol, currentRow, targetCol, targetRow);

            return returnString.ToString();
        }

        /**
         * Getter and setter for the move's current location.
         */
        public CheckersCoordinates CurrentLocation
        {
            get
            {
                return m_CurrentLocation;
            }

            set
            {
                m_CurrentLocation = value;
            }
        }

        /**
         * Getter and setter for the move's target location.
         */
        public CheckersCoordinates TargetLocation
        {
            get
            {
                return m_TargetLocation;
            }

            set
            {
                m_TargetLocation = value;
            }
        }

        /**
         * Getter and setter for the move's player.
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

        /**
         * Getter and setter for whether we can do another jump after the current move.
         */
        public bool HasDoubleJump
        {
            get
            {
                return m_HasDoubleJump;
            }

            set
            {
                m_HasDoubleJump = value;
            }
        }

        /**
         * Getter and setter for whether the move is a jump move.
         */
        public bool IsJumpMove
        {
            get
            {
                return m_IsJumpMove;
            }

            set
            {
                m_IsJumpMove = value;
            }
        }
    }
}