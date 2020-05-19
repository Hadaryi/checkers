using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public class FormGame : Form
    {
        private readonly GameUI r_GameUI = GameUI.GetInstance();
        private byte m_BoardSize;

        private Label labelPlayerOne;
        private Label labelPlayerOneScore;
        private Label labelPlayerTwo;
        private Label labelPlayerTwoScore;

        public FormGame()
        {
            initBoardSize();
            initializeComponent();
        }

        private void initBoardSize()
        {
            m_BoardSize = (byte)r_GameUI.BoardSize;
        }

        private void initializeComponent()
        {
            this.Text = GameUI.k_GameName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Height = (m_BoardSize * Tile.k_SquareSize) + 100;
            this.Width = (m_BoardSize * Tile.k_SquareSize) + 40;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            labelPlayerOne = new Label();
            labelPlayerOne.Text = string.Format("{0}:", r_GameUI.PlayerOneName);
            labelPlayerOne.Top = 20;
            labelPlayerOne.Left = Tile.k_SquareSize + 10;
            labelPlayerOne.AutoSize = true;
            this.Controls.Add(labelPlayerOne);

            labelPlayerOneScore = new Label();
            labelPlayerOneScore.Text = "0";
            labelPlayerOneScore.Top = labelPlayerOne.Top;
            labelPlayerOneScore.Left = labelPlayerOne.Left + labelPlayerOne.Width;
            labelPlayerOneScore.AutoSize = true;
            this.Controls.Add(labelPlayerOneScore);

            labelPlayerTwo = new Label();
            labelPlayerTwo.Text = string.Format("{0}:", r_GameUI.PlayerTwoName);
            labelPlayerTwo.Top = 20;
            labelPlayerTwo.Left = this.ClientSize.Width - (Tile.k_SquareSize * 3) - 10;
            labelPlayerTwo.AutoSize = true;
            this.Controls.Add(labelPlayerTwo);

            labelPlayerTwoScore = new Label();
            labelPlayerTwoScore.Text = "0";
            labelPlayerTwoScore.Top = labelPlayerTwo.Top;
            labelPlayerTwoScore.Left = labelPlayerTwo.Left + labelPlayerTwo.Width;
            labelPlayerTwoScore.AutoSize = true;
            this.Controls.Add(labelPlayerTwoScore);
        }

        public int PlayerOneScore
        {
            get
            {
                return int.Parse(labelPlayerOneScore.Text);
            }

            set
            {
                labelPlayerOneScore.Text = value.ToString();
            }
        }

        public int PlayerTwoScore
        {
            get
            {
                return int.Parse(labelPlayerTwoScore.Text);
            }

            set
            {
                labelPlayerTwoScore.Text = value.ToString();
            }
        }
    }
}