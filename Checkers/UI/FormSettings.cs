using System;
using System.Windows.Forms;

namespace Checkers
{
    public partial class FormSettings : Form
    {
        public const int k_MaxNameLength = 20;

        private string m_PlayerOneName = "Player 1";
        private string m_PlayerTwoName = "Player 2";
        private bool m_IsComputer = true;
        private eBoardSize m_BoardSize = eBoardSize.Size6;
        private bool m_DoneButtonPressed = false;

        public FormSettings()
        {
            InitializeComponent();

            radioButtonSize6.Click += radioSize_Changed;
            radioButtonSize8.Click += radioSize_Changed;
            radioButtonSize10.Click += radioSize_Changed;
        }

        private void checkBoxPlayerTwo_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            CheckBox checkBoxPlayerTwo = i_Sender as CheckBox;

            if (checkBoxPlayerTwo.Checked)
            {
                textBoxPlayerTwo.Enabled = true;
            }
            else
            {
                textBoxPlayerTwo.Enabled = false;
            }
        }

        private void buttonDone_Click(object i_Sender, EventArgs i_EventArgs)
        {
            startNewGame();
        }

        private void startNewGame()
        {
            const bool v_AcceptDigits = true;
            const bool v_AcceptSpecialChars = true;

            m_PlayerOneName = textBoxPlayerOne.Text;
            m_PlayerTwoName = textBoxPlayerTwo.Text;
            m_IsComputer = !checkBoxPlayerTwo.Checked;

            if (!Utils.StringUtils.IsNameValid(m_PlayerOneName, k_MaxNameLength, v_AcceptDigits, v_AcceptSpecialChars))
            {
                MessageBox.Show("Player 1's name is invalid.");
            }
            else if (!Utils.StringUtils.IsNameValid(m_PlayerTwoName, k_MaxNameLength, v_AcceptDigits, v_AcceptSpecialChars))
            {
                MessageBox.Show("Player 2's name is invalid.");
            }
            else
            {
                m_DoneButtonPressed = true;
                this.Close();
            }
        }

        private void radioSize_Changed(object i_Sender, EventArgs i_EventArgs)
        {
            if (radioButtonSize6.Checked)
            {
                m_BoardSize = eBoardSize.Size6;
            }
            else if (radioButtonSize8.Checked)
            {
                m_BoardSize = eBoardSize.Size8;
            }
            else
            {
                m_BoardSize = eBoardSize.Size10;
            }
        }

        private void FormSettings_FormClosing(object i_Sender, FormClosingEventArgs i_EventArgs)
        {
            // Setting the default parameters if the exit button was pressed.
            if (!m_DoneButtonPressed)
            {
                m_BoardSize = eBoardSize.Size6;
                m_PlayerOneName = "Player 1";
                m_PlayerTwoName = "Player 2";
                m_IsComputer = true;
            }
        }

        public eBoardSize BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string PlayerOneName
        {
            get
            {
                return m_PlayerOneName;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return m_PlayerTwoName;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }
    }
}