namespace Checkers
{
    public struct CheckersCoordinates
    {
        // Constants declarations.
        public const char k_StartRow = 'a';
        public const char k_StartColumn = 'A';

        private char m_Column;
        private char m_Row;

        /**
         * Constructor - initializes the row and column that the coordinates represent in the checkers board.
         */
        public CheckersCoordinates(char i_Column, char i_Row)
        {
            m_Column = i_Column;
            m_Row = i_Row;
        }

        /**
         * This method returns whether the coordinates are valid for a given board size.
         */
        public bool IsValid(byte i_BoardSize)
        {
            bool isValid = true;

            // Checking that the column does not exceed the board's size.
            if (m_Column < k_StartColumn || m_Column >= k_StartColumn + i_BoardSize)
            {
                isValid = false;
            }

            // Checking that the row does not exceed the board's size.
            if (m_Row < k_StartRow || m_Row >= k_StartRow + i_BoardSize)
            {
                isValid = false;
            }

            return isValid;
        }

        /**
         * Getter and setter for the coordinates' column.
         */
        public char Column
        {
            get
            {
                return m_Column;
            }

            set
            {
                m_Column = value;
            }
        }

        /**
         * Getter and setter for the coordinates' row.
         */
        public char Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }
    }
}