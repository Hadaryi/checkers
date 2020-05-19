using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public class ButtonSoldier : PictureBox
    {
        private Soldier m_Soldier;
        private Tile m_Tile;
        private Timer m_MoveTimer;

        public event Action<ButtonSoldier> Moved;

        public ButtonSoldier()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = Tile.k_SquareSize / 2;
            this.Height = Tile.k_SquareSize / 2;
            this.BackColor = Color.Transparent;
        }

        public void MoveToTile(Tile i_Tile)
        {
            attachButtonToTile(i_Tile);

            this.Left = m_Tile.Left + (this.Width / 2);
            this.Top = m_Tile.Top + (this.Height / 2);

            OnMoved();
        }

        public void AnimateMoveToTile(Tile i_Tile)
        {
            attachButtonToTile(i_Tile);

            m_MoveTimer = new Timer();
            m_MoveTimer.Interval = 10;
            m_MoveTimer.Tick += moveTimer_Tick;
            m_MoveTimer.Start();
        }

        private void attachButtonToTile(Tile i_Tile)
        {
            if (m_Tile != null)
            {
                m_Tile.ButtonSoldier = null;
            }

            this.BringToFront();
            i_Tile.ButtonSoldier = this;
            m_Tile = i_Tile;
        }

        private void moveTimer_Tick(object i_Sender, EventArgs i_EventArgs)
        {
            moveButtonCloserToTile();
        }

        private void moveButtonCloserToTile()
        {
            GameUI gameUIInstance = GameUI.GetInstance();
            gameUIInstance.MoveInProgress = true;

            int targetLeft = m_Tile.Left + (this.Width / 2);
            int targetTop = m_Tile.Top + (this.Height / 2);

            if (this.Left == targetLeft && this.Top == targetTop)
            {
                m_MoveTimer.Stop();
                gameUIInstance.MoveInProgress = false;
                OnMoved();
            }
            else
            {
                if (targetLeft < this.Left)
                {
                    this.Left -= 1;
                }
                else
                {
                    this.Left += 1;
                }

                if (targetTop < this.Top)
                {
                    this.Top -= 1;
                }
                else
                {
                    this.Top += 1;
                }
            }
        }

        public void MakeKing()
        {
            if (m_Soldier.SoldierType == Soldier.eType.PlayerOneKing)
            {
                setWhiteKing();
            }
            else if (m_Soldier.SoldierType == Soldier.eType.PlayerTwoKing)
            {
                setBlackKing();
            }
        }

        private void setWhiteKing()
        {
            this.ImageLocation = @"..\..\..\Resources\soldier_king_x.png";
        }

        private void setBlackKing()
        {
            this.ImageLocation = @"..\..\..\Resources\soldier_king_o.png";
        }

        private void soldier_BecameKing(Soldier i_Soldier)
        {
            MakeKing();
        }

        public Soldier Soldier
        {
            get
            {
                return m_Soldier;
            }

            set
            {
                // Removing the event became king from the previous soldier.
                if (m_Soldier != null)
                {
                    m_Soldier.BecameKing -= soldier_BecameKing;
                }

                m_Soldier = value;

                // Setting the image for the button soldier according to the soldier.
                if (m_Soldier != null)
                {
                    m_Soldier.BecameKing += soldier_BecameKing;

                    switch (m_Soldier.SoldierType)
                    {
                        case Soldier.eType.PlayerOne:
                            this.ImageLocation = @"..\..\..\Resources\soldier_x.png";
                            break;
                        case Soldier.eType.PlayerTwo:
                            this.ImageLocation = @"..\..\..\Resources\soldier_o.png";
                            break;
                        case Soldier.eType.PlayerOneKing:
                            this.ImageLocation = @"..\..\..\Resources\soldier_king_x.png";
                            break;
                        case Soldier.eType.PlayerTwoKing:
                            this.ImageLocation = @"..\..\..\Resources\soldier_king_o.png";
                            break;
                    }
                }
            }
        }

        private void OnMoved()
        {
            if (Moved != null)
            {
                Moved.Invoke(this);
            }
        }

        public Tile Tile
        {
            get
            {
                return m_Tile;
            }
        }
    }
}