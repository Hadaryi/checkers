using System.Collections.Generic;

namespace Checkers
{
    public class CheckersBoard
    {
        // Constant declaration.
        public const char k_PlayerTwoStartRow = 'a';

        private Soldier[,] m_Board;
        private byte m_Size;

        /**
         * This method receives a letter and converts it to a column index in the matrix.
         */
        public static byte GetColumnIndexByLetter(char i_Letter)
        {
            return (byte)(i_Letter - 'A');
        }

        /**
         * This method receives a letter and converts it to a row index in the matrix.
         */
        public static byte GetRowIndexByLetter(char i_Letter)
        {
            return (byte)(i_Letter - 'a');
        }

        /**
         * Constructor - initializes the checkers board.
         */
        public CheckersBoard(byte i_Size)
        {
            m_Board = new Soldier[i_Size, i_Size];
            m_Size = i_Size;
        }

        /**
         * This method initializes the soldiers in the checkers board for each player.
         */
        public void Init()
        {
            byte amountOfSoldiersLines = GetAmountOfSoldiersLines(); // Calculating the amount of initializes soldiers lines for each player.
            char playerOneStartRow = (char)('a' + amountOfSoldiersLines + 2);

            initSoldiersLines(k_PlayerTwoStartRow, Soldier.eType.PlayerTwo); // Initializing the soldiers lines for player two.
            initSoldiersLines(playerOneStartRow, Soldier.eType.PlayerOne); // Initializing the soldiers lines for player one.
        }

        /**
         * This method receives a start row and a soldier type and intializes the soldiers lines for each soldier type starting from the given start row.
         */
        private void initSoldiersLines(char i_StartRow, Soldier.eType i_SoldierType)
        {
            byte amountOfSoldiersLines = GetAmountOfSoldiersLines();

            for (char i = i_StartRow; i < i_StartRow + amountOfSoldiersLines; i++)
            {
                char startColumn = (char)('A' + (i % 2));

                for (char j = startColumn; j < startColumn + m_Size; j += (char)2)
                {
                    CheckersCoordinates soldierLocation = new CheckersCoordinates(j, i);

                    byte realRowIndex = GetRowIndexByLetter(i);
                    byte realColIndex = GetColumnIndexByLetter(j);
                    m_Board[realRowIndex, realColIndex] = new Soldier(i_SoldierType, soldierLocation);
                }
            }
        }

        /**
         * This method receives a Player and returns the soldiers for that player in the board.
         */
        public List<Soldier> GetPlayerSoldiers(Player i_Player)
        {
            byte amountOfSoldiersLines = GetAmountOfSoldiersLines();
            byte initSoldiersAmount = (byte)((m_Size / 2) * amountOfSoldiersLines);
            List<Soldier> soldiers = new List<Soldier>(initSoldiersAmount);

            foreach (Soldier soldier in m_Board)
            {
                if (soldier != null && soldier.PlayerType == i_Player.PlayerType)
                {
                    soldier.Player = i_Player;
                    soldiers.Add(soldier);
                }
            }

            return soldiers;
        }

        /**
         * This method returns the amount of soldiers lines that each player has at the start of the game.
         */
        public byte GetAmountOfSoldiersLines()
        {
            return (byte)((m_Size / 2) - 1);
        }

        /**
         * This method receives a row index and column index in the board's matrix and returns the soldier located in that position.
         */
        public Soldier GetSoldierAt(byte i_RowIndex, byte i_ColIndex)
        {
            return m_Board[i_RowIndex, i_ColIndex];
        }

        /**
         * This method receives a Checkers Coordinates in the board and returns the soldier located in that position.
         */
        public Soldier GetSoldierAt(CheckersCoordinates i_Location)
        {
            byte rowIndex = GetRowIndexByLetter(i_Location.Row);
            byte colIndex = GetColumnIndexByLetter(i_Location.Column);

            return GetSoldierAt(rowIndex, colIndex);
        }

        /**
         * This method receives a Move object and moves a soldier in the board from the current location the soldier is located at to the target location.
         */
        public void MoveSoldier(Move i_Move)
        {
            CheckersCoordinates currentLocation = i_Move.CurrentLocation;
            CheckersCoordinates targetLocation = i_Move.TargetLocation;
            byte currentRowIndex = GetRowIndexByLetter(currentLocation.Row);
            byte currentColIndex = GetColumnIndexByLetter(currentLocation.Column);
            byte targetRowIndex = GetRowIndexByLetter(targetLocation.Row);
            byte targetColIndex = GetColumnIndexByLetter(targetLocation.Column);
            Soldier moveSoldier = m_Board[currentRowIndex, currentColIndex];

            moveSoldier.Location = targetLocation;
            m_Board[targetRowIndex, targetColIndex] = moveSoldier;
            RemoveSoldier(currentLocation);
        }

        /**
         * This method receives a Checkers Coordinates in the board and removes the soldier located at that position from the board.
         */
        public void RemoveSoldier(CheckersCoordinates i_Location)
        {
            byte rowIndex = GetRowIndexByLetter(i_Location.Row);
            byte colIndex = GetColumnIndexByLetter(i_Location.Column);

            m_Board[rowIndex, colIndex] = null;
        }

        /**
         * Getter for the board's size.
         */
        public byte Size
        {
            get
            {
                return m_Size;
            }
        }
    }
}