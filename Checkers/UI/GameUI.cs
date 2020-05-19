using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public class GameUI
    {
        public const string k_GameName = "Damka";

        private static readonly Color sr_InvalidSquareColor = Color.Black;
        private static readonly Color sr_ValidSquareColor = Color.White;
        private static readonly Color sr_PressedSquareColor = Color.LightGreen;

        private static GameUI s_Instance;

        private readonly GameHandler r_GameHandler = GameHandler.GetInstance();

        private FormGame m_FormGame;

        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private eBoardSize m_BoardSize;
        private Tile[,] m_Tiles;

        private Tile m_PressedTile;
        private bool m_MoveInProgress = false;

        private GameUI()
        {
            registerEventListeners();
        }

        public static GameUI GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new GameUI();
            }

            return s_Instance;
        }

        public void NewGame()
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();

            m_BoardSize = formSettings.BoardSize;
            m_PlayerOne = new Player(formSettings.PlayerOneName);
            m_PlayerTwo = new Player(formSettings.PlayerTwoName, formSettings.IsComputer);

            r_GameHandler.Init(m_PlayerOne, m_PlayerTwo, (byte)m_BoardSize);
            m_Tiles = new Tile[(byte)m_BoardSize, (byte)m_BoardSize];

            if (m_FormGame == null)
            {
                m_FormGame = new FormGame();
                initBoard();
                m_FormGame.ShowDialog();
            }
        }

        private void registerEventListeners()
        {
            r_GameHandler.GameEnded += gameUI_GameEnded;
            r_GameHandler.Moved += soldiers_Moved;
            r_GameHandler.SoldierRemoved += soldiers_SoldierRemoved;
        }

        private void newRound()
        {
            r_GameHandler.NewRound((byte)m_BoardSize);
            initBoard();
        }

        private void initBoard()
        {
            initBoardTiles();
        }

        private void initBoardTiles()
        {
            removeAllTiles();

            byte boardSize = (byte)m_BoardSize;

            for (byte i = 0; i < boardSize; i++)
            {
                for (byte j = 0; j < boardSize; j++)
                {
                    initTile(i, j);
                }
            }
        }

        private void initTile(byte i_Row, byte i_Col)
        {
            Tile tile = new Tile();

            if ((i_Row + i_Col) % 2 == 0)
            {
                tile.BackColor = sr_InvalidSquareColor;
                tile.Enabled = false;
            }
            else
            {
                tile.BackColor = sr_ValidSquareColor;
            }

            tile.Click += tiles_Click;
            tile.Row = i_Row;
            tile.Column = i_Col;

            m_FormGame.Controls.Add(tile);
            m_Tiles[i_Row, i_Col] = tile;

            addSoldierToTile(i_Row, i_Col);
        }

        private void removeAllTiles()
        {
            // Checking if the tiles matrix was initialized.
            if (m_Tiles[0, 0] != null)
            {
                foreach (Tile tile in m_Tiles)
                {
                    if (tile.ButtonSoldier != null)
                    {
                        m_FormGame.Controls.Remove(tile.ButtonSoldier);
                    }

                    m_FormGame.Controls.Remove(tile);
                }
            }
        }

        private void addSoldierToTile(byte i_Row, byte i_Column)
        {
            Soldier soldier = r_GameHandler.GetSoldierAt(i_Row, i_Column);

            if (soldier != null)
            {
                ButtonSoldier buttonSoldier = new ButtonSoldier();

                buttonSoldier.Soldier = soldier;
                buttonSoldier.Click += soldier_Click;
                m_FormGame.Controls.Add(buttonSoldier);

                buttonSoldier.Moved += buttonSoldiers_Moved;
                buttonSoldier.AnimateMoveToTile(m_Tiles[i_Row, i_Column]);
            }
        }

        private void soldiers_Moved(Move i_Move)
        {
            moveButtonSoldier(i_Move);
        }

        private void moveButtonSoldier(Move i_Move)
        {
            CheckersCoordinates sourceLocation = i_Move.CurrentLocation;
            CheckersCoordinates targetLocation = i_Move.TargetLocation;

            Tile sourceTile = getTileByLocation(sourceLocation);
            Tile targetTile = getTileByLocation(targetLocation);

            sourceTile.ButtonSoldier.AnimateMoveToTile(targetTile);
        }

        private void buttonSoldiers_Moved(ButtonSoldier i_Button)
        {
            makeMoveByComputer();
        }

        private void makeMoveByComputer()
        {
            Player currentPlayer = r_GameHandler.GetCurrentPlayer();

            if (currentPlayer.IsComputer)
            {
                Move computerMove = r_GameHandler.GetMoveFromComputer();

                if (computerMove != null)
                {
                    r_GameHandler.MakeMove(computerMove);
                }
            }
        }

        private Tile getTileByLocation(CheckersCoordinates i_Coordinates)
        {
            byte rowIndex = CheckersBoard.GetRowIndexByLetter(i_Coordinates.Row);
            byte colIndex = CheckersBoard.GetColumnIndexByLetter(i_Coordinates.Column);

            return m_Tiles[rowIndex, colIndex];
        }

        private void tiles_Click(object i_Sender, EventArgs i_EventArgs)
        {
            Tile tileClicked = i_Sender as Tile;

            tileClickHandler(tileClicked);
        }

        private void soldier_Click(object i_Sender, EventArgs i_EventArgs)
        {
            ButtonSoldier soldierClicked = i_Sender as ButtonSoldier;

            tileClickHandler(soldierClicked.Tile);
        }

        private void tileClickHandler(Tile i_Tile)
        {
            if (!m_MoveInProgress && isTileRelatedToCurrentPlayer(i_Tile))
            {
                if (i_Tile.HasSoldier())
                {
                    tilePressIndicationHandler(i_Tile);
                }
                else if (m_PressedTile != null)
                {
                    makePlayerMove(m_PressedTile, i_Tile);
                }
            }
        }

        private void tilePressIndicationHandler(Tile i_Tile)
        {
            if (m_PressedTile == i_Tile)
            {
                i_Tile.IsClicked = false;
                i_Tile.BackColor = sr_ValidSquareColor;
                m_PressedTile = null;
            }
            else if (m_PressedTile == null)
            {
                i_Tile.IsClicked = true;
                i_Tile.BackColor = sr_PressedSquareColor;
                m_PressedTile = i_Tile;
            }
        }

        private bool isTileRelatedToCurrentPlayer(Tile i_Tile)
        {
            Player currentPlayer = r_GameHandler.GetCurrentPlayer();

            return i_Tile.ButtonSoldier == null || i_Tile.ButtonSoldier.Soldier.Player == currentPlayer;
        }

        private void makePlayerMove(Tile i_SourceButton, Tile i_TargetButton)
        {
            char currentColumn = (char)(CheckersCoordinates.k_StartColumn + i_SourceButton.Column);
            char currentRow = (char)(CheckersCoordinates.k_StartRow + i_SourceButton.Row);
            char targetColumn = (char)(CheckersCoordinates.k_StartColumn + i_TargetButton.Column);
            char targetRow = (char)(CheckersCoordinates.k_StartRow + i_TargetButton.Row);

            CheckersCoordinates currentLocation = new CheckersCoordinates(currentColumn, currentRow);
            CheckersCoordinates targetLocation = new CheckersCoordinates(targetColumn, targetRow);
            Move soldierMove = new Move(currentLocation, targetLocation);

            r_GameHandler.MakeMove(soldierMove);

            m_PressedTile.BackColor = sr_ValidSquareColor;
            m_PressedTile = null;
        }

        private void gameUI_GameEnded(GameHandler i_GameHandler)
        {
            askForAnotherRound();
        }

        private void askForAnotherRound()
        {
            string winnerMessage = createWinnerMessage();

            DialogResult userChoice = MessageBox.Show(winnerMessage, k_GameName, MessageBoxButtons.YesNo);

            if (userChoice == DialogResult.Yes)
            {
                newRound();
            }
            else
            {
                m_FormGame.Close();
            }
        }

        private string createWinnerMessage()
        {
            bool playerOneHasMoves = r_GameHandler.PlayerHasMoves(m_PlayerOne);
            bool playerTwoHasMoves = r_GameHandler.PlayerHasMoves(m_PlayerTwo);
            Player winner;
            StringBuilder displayMessage = new StringBuilder();

            if (!playerOneHasMoves && !playerTwoHasMoves)
            {
                displayMessage.Append("Tie!");
            }
            else
            {
                if (playerOneHasMoves && !playerTwoHasMoves)
                {
                    winner = m_PlayerOne;
                }
                else
                {
                    winner = m_PlayerTwo;
                }

                displayMessage.AppendFormat("{0} Won!", winner.Name);
            }

            m_FormGame.PlayerOneScore = m_PlayerOne.Score;
            m_FormGame.PlayerTwoScore = m_PlayerTwo.Score;

            displayMessage.AppendFormat("{0}Another Round?", Environment.NewLine);

            return displayMessage.ToString();
        }

        private void soldiers_SoldierRemoved(CheckersCoordinates i_Coordinates)
        {
            byte rowIndex = CheckersBoard.GetRowIndexByLetter(i_Coordinates.Row);
            byte colIndex = CheckersBoard.GetColumnIndexByLetter(i_Coordinates.Column);

            // Removing the soldier button from the board.
            m_FormGame.Controls.Remove(m_Tiles[rowIndex, colIndex].ButtonSoldier);
            m_Tiles[rowIndex, colIndex].ButtonSoldier = null;
        }

        public string PlayerOneName
        {
            get
            {
                return m_PlayerOne.Name;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return m_PlayerTwo.Name;
            }
        }

        public eBoardSize BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public bool MoveInProgress
        {
            get
            {
                return m_MoveInProgress;
            }

            set
            {
                m_MoveInProgress = value;
            }
        }
    }
}