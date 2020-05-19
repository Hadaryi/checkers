using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public class Tile : PictureBox
    {
        public const int k_SquareSize = 46;

        private byte m_Row;
        private byte m_Column;
        private bool m_IsClicked;
        private ButtonSoldier m_ButtonSoldier;

        public Tile()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            this.Size = new Size(k_SquareSize, k_SquareSize);
            this.Top = 50 + (m_Row * k_SquareSize);
            this.Left = 10 + (m_Column * k_SquareSize);
        }

        public bool HasSoldier()
        {
            return m_ButtonSoldier != null;
        }

        public byte Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
                this.Top = 50 + (m_Row * k_SquareSize);
            }
        }

        public byte Column
        {
            get
            {
                return m_Column;
            }

            set
            {
                m_Column = value;
                this.Left = 10 + (m_Column * k_SquareSize);
            }
        }

        public bool IsClicked
        {
            get
            {
                return m_IsClicked;
            }

            set
            {
                m_IsClicked = value;
            }
        }

        public ButtonSoldier ButtonSoldier
        {
            get
            {
                return m_ButtonSoldier;
            }

            set
            {
                m_ButtonSoldier = value;
            }
        }
    }
}