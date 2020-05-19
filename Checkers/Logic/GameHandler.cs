using System;
using System.Collections.Generic;

namespace Checkers
{
    public class GameHandler
    {
        private static GameHandler s_Instance;

        // Constants declarations.
        public const int k_NormalSoldierScore = 1;
        public const int k_KingScore = 4;

        private CheckersBoard m_CheckersBoard;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Move m_LastMove;

        public event Action<GameHandler> GameEnded;

        public event Action<Move> Moved;

        public event Action<CheckersCoordinates> SoldierRemoved;

        /**
         * Constructor.
         */
        private GameHandler()
        {
        }

        /**
         * This method returns the current game instance which is taking place.
         */
        public static GameHandler GetInstance()
        {
            if (s_Instance == null)
            {
                s_Instance = new GameHandler();
            }

            return s_Instance;
        }

        /**
         * This method initializes the game.
         */
        public void Init(Player i_PlayerOne, Player i_PlayerTwo, byte i_BoardSize)
        {
            i_PlayerOne.PlayerType = Player.eType.PlayerOne;
            i_PlayerTwo.PlayerType = Player.eType.PlayerTwo;

            m_PlayerOne = i_PlayerOne;
            m_PlayerTwo = i_PlayerTwo;

            NewRound(i_BoardSize);
        }

        /**
         * This method intializes a new round according to a given board size.
         */
        public void NewRound(byte i_BoardSize)
        {
            m_CheckersBoard = new CheckersBoard(i_BoardSize);

            m_CheckersBoard.Init();
            m_PlayerOne.Soldiers = m_CheckersBoard.GetPlayerSoldiers(m_PlayerOne);
            m_PlayerTwo.Soldiers = m_CheckersBoard.GetPlayerSoldiers(m_PlayerTwo);

            m_PlayerOne.CurrentGameScore = m_PlayerOne.Soldiers.Count;
            m_PlayerTwo.CurrentGameScore = m_PlayerTwo.Soldiers.Count;

            m_LastMove = null;
        }

        /**
         * This method returns the player which has the higher score in the current game.
         */
        public Player GetCurrentGameHigherScorePlayer()
        {
            Player winner;

            if (m_PlayerOne.CurrentGameScore > m_PlayerTwo.CurrentGameScore)
            {
                winner = m_PlayerOne;
            }
            else
            {
                winner = m_PlayerTwo;
            }

            return winner;
        }

        /**
         * This method receives a player and returns whether the player can quit the current game or not.
         */
        public bool CanPlayerQuit(Player i_Player)
        {
            bool canQuit = false;
            int playerOneScore = m_PlayerOne.CurrentGameScore;
            int playerTwoScore = m_PlayerTwo.CurrentGameScore;

            if (i_Player == m_PlayerTwo && playerTwoScore < playerOneScore)
            {
                canQuit = true;
            }
            else if (i_Player == m_PlayerOne && playerOneScore < playerTwoScore)
            {
                canQuit = true;
            }

            return canQuit;
        }

        /**
         * This method receives a Move object and if the move is a jump move then it removes the jumped soldier from the board
         * and changes the score accordingly.
         */
        private void validJumpMoveHandler(Move i_Move)
        {
            if (i_Move.IsJumpMove)
            {
                Player movePlayer = i_Move.Player != null ? i_Move.Player : GetCurrentPlayer();
                Player opponentPlayer = movePlayer == m_PlayerOne ? m_PlayerTwo : m_PlayerOne;
                Soldier jumpedSoldier = getJumpedSoldier(i_Move);
                int jumpedSoldierScore = jumpedSoldier.IsKing() ? k_KingScore : k_NormalSoldierScore;

                OnSoldierRemoved(jumpedSoldier.Location);

                opponentPlayer.CurrentGameScore -= jumpedSoldierScore;
                m_CheckersBoard.RemoveSoldier(jumpedSoldier.Location);
                opponentPlayer.Soldiers.Remove(jumpedSoldier);
                i_Move.HasDoubleJump = checkForDoubleJump(i_Move);
            }
        }

        /**
         * This method receives a Move object and if the move is valid then it moves the soldier from the current location to the target location
         * of the move, if the soldier is supposed to be a king then it turns the soldier into a king, handles jumping of the soldier over another soldier
         * and finally updates the winner's score if the opponent does not have any moves left.
         */
        public void MakeMove(Move i_Move)
        {
            if (IsMoveValid(i_Move))
            {
                Soldier soldier = m_CheckersBoard.GetSoldierAt(i_Move.CurrentLocation);

                m_CheckersBoard.MoveSoldier(i_Move); // Moving the soldier to the target location in the board.

                // If the move object's player is null, then we set that the move was played by the current player (relevant to the next move validation).
                if (i_Move.Player == null)
                {
                    i_Move.Player = GetCurrentPlayer();
                }

                if (ShouldBecomeKing(soldier))
                {
                    soldier.MakeKing();
                    i_Move.Player.CurrentGameScore += k_KingScore - k_NormalSoldierScore;
                }

                m_LastMove = i_Move; // Setting the last played move to be the current move.
                validJumpMoveHandler(i_Move); // If the move was a jump move then we should handle everything that is related to it.
                OnMoved(i_Move);

                Player opponentPlayer = i_Move.Player == m_PlayerOne ? m_PlayerTwo : m_PlayerOne;

                // If the opponent player doesn't have moves left then the game has ended.
                if (!PlayerHasMoves(opponentPlayer))
                {
                    // If the current player has moves, then it means that the current player has won.
                    if (PlayerHasMoves(i_Move.Player))
                    {
                        UpdateWinnerScore(i_Move.Player);
                    }
                    
                    OnGameEnded();
                }
            }
        }

        /**
         * This method receives a player and adds the current game score to the final score of the player.
         */
        public void UpdateWinnerScore(Player i_Player)
        {
            i_Player.Score += GetCurrentGameScoreDifference();
        }

        /**
         * This method receives a soldier and returns whether the soldier should become a king according to the soldier location in the board.
         */
        public bool ShouldBecomeKing(Soldier i_Soldier)
        {
            bool makeKing = false;
            char playerOneKingsRow = CheckersBoard.k_PlayerTwoStartRow;
            char playerTwoKingsRow = (char)('a' + m_CheckersBoard.Size - 1);

            if (i_Soldier.Location.Row == playerOneKingsRow && i_Soldier.SoldierType == Soldier.eType.PlayerOne)
            {
                makeKing = true;
            }
            else if (i_Soldier.Location.Row == playerTwoKingsRow && i_Soldier.SoldierType == Soldier.eType.PlayerTwo)
            {
                makeKing = true;
            }

            return makeKing;
        }

        /**
         * This method receives a move object and returns the soldier that was jumped over (in case the move was a jump move).
         */
        private Soldier getJumpedSoldier(Move i_Move)
        {
            CheckersCoordinates jumpedSoldierLocation = getJumpedSoldierLocation(i_Move);

            return m_CheckersBoard.GetSoldierAt(jumpedSoldierLocation);
        }

        /**
         * This method receives a move object that represents a jump move and returns the jumped soldier location.
         */
        public CheckersCoordinates getJumpedSoldierLocation(Move i_Move)
        {
            CheckersCoordinates targetLocation = i_Move.TargetLocation;
            CheckersCoordinates currentLocation = i_Move.CurrentLocation;
            char middleColumn = (char)((currentLocation.Column + targetLocation.Column) / 2);
            char middleRow = (char)((currentLocation.Row + targetLocation.Row) / 2);

            return new CheckersCoordinates(middleColumn, middleRow);
        }

        /**
         * This method returns the current player who should make the move.
         */
        public Player GetCurrentPlayer()
        {
            Player currentPlayer = m_PlayerOne;
            Player lastPlayer = m_LastMove == null ? m_PlayerOne : m_LastMove.Player;

            if (m_LastMove != null)
            {
                if (m_LastMove.HasDoubleJump)
                {
                    currentPlayer = lastPlayer;
                }
                else if (lastPlayer == m_PlayerOne)
                {
                    currentPlayer = m_PlayerTwo;
                }
            }

            return currentPlayer;
        }

        /**
         * This method returns the current opponent to the player who's making the move.
         */
        public Player GetCurrentOpponentPlayer()
        {
            Player currentPlayer = GetCurrentPlayer();

            return currentPlayer == m_PlayerOne ? m_PlayerTwo : m_PlayerOne;
        }

        /**
         * This method receives a move object and returns whether the move is a legit jumpable move or not.
         */
        public bool IsJumpableMove(Move i_Move)
        {
            bool jumpableMove = true;
            CheckersCoordinates currentLocation = i_Move.CurrentLocation;
            CheckersCoordinates targetLocation = i_Move.TargetLocation;
            int columnsDelta = targetLocation.Column - currentLocation.Column;
            int rowsDelta = targetLocation.Row - currentLocation.Row;
            Player movePlayer = i_Move.Player != null ? i_Move.Player : GetCurrentPlayer();

            if (Math.Abs(columnsDelta) != Move.k_JumpMovesOffset || Math.Abs(rowsDelta) != Move.k_JumpMovesOffset)
            {
                jumpableMove = false;
            }
            else
            {
                Soldier jumpableSoldier = getJumpedSoldier(i_Move);

                // Checking that there is a jumpable soldier to jump over and that it's the opponent's soldier.
                if (jumpableSoldier == null || movePlayer.PlayerType == jumpableSoldier.PlayerType)
                {
                    jumpableMove = false;
                }
                else if (m_LastMove != null && m_LastMove.HasDoubleJump && !currentLocation.Equals(m_LastMove.TargetLocation))
                {
                    // Checking that if the last move is a double jump move, then the last soldier played is the current soldier.
                    jumpableMove = false;
                }
            }

            return jumpableMove;
        }

        /**
         * This method receives a Move object and returns whether the soldier making the move is going in the right direction.
         */
        private bool soldierMovingInRightDirection(Move i_Move)
        {
            CheckersCoordinates currentLocation = i_Move.CurrentLocation;
            int rowsDelta = i_Move.TargetLocation.Row - currentLocation.Row; // Calculating the rows delta which tells us if the direction is up or down.
            bool validRowDelta = rowsDelta != 0; // If the rows delta is 0 then the soldier is moving horizontally which is invalid.
            Player movePlayer = i_Move.Player != null ? i_Move.Player : GetCurrentPlayer();
            Soldier soldier = m_CheckersBoard.GetSoldierAt(currentLocation);
            bool playerMovingInRightDirection;

            // If the rows delta is greater than zero then the soldier is moving down (which should be done by a king or by player two and vice versa).
            if (rowsDelta > 0)
            {
                playerMovingInRightDirection = movePlayer == m_PlayerOne;
            }
            else
            {
                playerMovingInRightDirection = movePlayer == m_PlayerTwo;
            }

            return validRowDelta && (!playerMovingInRightDirection || soldier.IsKing());
        }

        /**
         * This method receives a Move and returns whether it's a valid diagonal move or not.
         */
        private bool isValidDiagonalMove(Move i_Move)
        {
            bool isValidMove = true;
            int columnsDelta = i_Move.TargetLocation.Column - i_Move.CurrentLocation.Column;
            int rowsDelta = i_Move.TargetLocation.Row - i_Move.CurrentLocation.Row;
            byte absoluteColumnsDelta = (byte)Math.Abs(columnsDelta);
            byte absoluteRowsDelta = (byte)Math.Abs(rowsDelta);

            if (rowsDelta == 0 || columnsDelta == 0 || absoluteRowsDelta > Move.k_JumpMovesOffset || absoluteColumnsDelta > Move.k_JumpMovesOffset || absoluteColumnsDelta != absoluteRowsDelta)
            {
                isValidMove = false;
            }
            else if (!soldierMovingInRightDirection(i_Move))
            {
                isValidMove = false;
            }

            return isValidMove;
        }

        /**
         * This method receives a Move and returns whether it is a valid move.
         * A valid move must be a diagonal move which doesn't overlap another soldier, it must move an actual soldier, it must not exceed the board's boundaries,
         * if it's a jump move then it must be jumping over the opponent's soldier and if the move isn't a jump move but the player has jump moves available, then
         * the move isn't valid as well.
         */
        public bool IsMoveValid(Move i_Move)
        {
            bool validMove = true;
            CheckersCoordinates targetLocation = i_Move.TargetLocation;
            CheckersCoordinates currentLocation = i_Move.CurrentLocation;
            Player movePlayer = i_Move.Player != null ? i_Move.Player : GetCurrentPlayer();

            if (!targetLocation.IsValid(m_CheckersBoard.Size) || !currentLocation.IsValid(m_CheckersBoard.Size))
            {
                validMove = false;
            }
            else
            {
                Soldier soldier = m_CheckersBoard.GetSoldierAt(currentLocation);
                Soldier targetLocationSoldier = m_CheckersBoard.GetSoldierAt(targetLocation);

                // Checking that the soldier actually exists and that we're not trying to move the soldier to an existing soldier.
                if (soldier == null || targetLocationSoldier != null)
                {
                    validMove = false;
                }
                else if (!isValidDiagonalMove(i_Move))
                {
                    validMove = false;
                }
                else if (i_Move.IsJumpMove && !IsJumpableMove(i_Move))
                {
                    validMove = false;
                }
                else if (!i_Move.IsJumpMove && PlayerHasJumpMove(movePlayer))
                {
                    validMove = false;
                }
            }

            return validMove;
        }

        /**
         * This method receives a soldier and a move offset and returns all the available moves that the soldier possibly has with the given
         * move offset.
         */
        public List<Move> GetPossibleSoldierMoves(Soldier i_Soldier, byte i_MoveOffset)
        {
            CheckersCoordinates soldierLocation = i_Soldier.Location;
            char targetRightColumn = (char)(soldierLocation.Column + i_MoveOffset);
            char targetLeftColumn = (char)(soldierLocation.Column - i_MoveOffset);
            char targetTopRow = (char)(soldierLocation.Row - i_MoveOffset);
            char targetBottomRow = (char)(soldierLocation.Row + i_MoveOffset);
            List<Move> possibleMoves = new List<Move>();
            CheckersCoordinates[] coordinates = new CheckersCoordinates[Move.k_PossibleDiagonalMoves];

            // Creating the coordinates as we need them for the Move objects.
            coordinates[0] = new CheckersCoordinates(targetRightColumn, targetTopRow);
            coordinates[1] = new CheckersCoordinates(targetLeftColumn, targetTopRow);
            coordinates[2] = new CheckersCoordinates(targetRightColumn, targetBottomRow);
            coordinates[3] = new CheckersCoordinates(targetLeftColumn, targetBottomRow);

            foreach (CheckersCoordinates coordinate in coordinates)
            {
                Move generatedMove = new Move(soldierLocation, coordinate);

                generatedMove.Player = i_Soldier.Player;

                if (IsMoveValid(generatedMove))
                {
                    possibleMoves.Add(generatedMove);
                }
            }

            return possibleMoves;
        }

        /**
         * This method receives a Soldier and returns whether the soldier has valid moves according to their offset.
         * Offset 1 means it can move 1 step diagonally, offset 2 means it can move 2 steps diagonally (only valid for jumpable moves).
         */
        public bool SoldierHasMoves(Soldier i_Soldier, byte i_MoveOffset)
        {
            CheckersCoordinates soldierLocation = i_Soldier.Location;
            List<Move> possibleMoves = GetPossibleSoldierMoves(i_Soldier, i_MoveOffset);

            return possibleMoves.Count > 0;
        }

        /**
         * This method returns whetehr a given player has any moves available or not.
         */
        public bool PlayerHasMoves(Player i_Player, bool i_CheckJumpMovesOnly = false)
        {
            bool hasMoves = false;
            List<Soldier> soldiers = i_Player.Soldiers;

            // Checking if there's at least one soldier in the player's soldiers that has a valid jump move.
            foreach (Soldier soldier in soldiers)
            {
                if (SoldierHasMoves(soldier, Move.k_JumpMovesOffset))
                {
                    hasMoves = true;
                    break;
                }
            }

            // Checking if there's at least one soldier in the player's soldiers that has a valid normal move if we didn't find a jump move.
            if (!hasMoves && !i_CheckJumpMovesOnly)
            {
                foreach (Soldier soldier in soldiers)
                {
                    if (SoldierHasMoves(soldier, Move.k_NormalMovesOffset))
                    {
                        hasMoves = true;
                        break;
                    }
                }
            }

            return hasMoves;
        }

        /**
         * This method returns whether both of the players have moves available.
         */
        public bool PlayersHaveMoves()
        {
            return PlayerHasMoves(m_PlayerOne) && PlayerHasMoves(m_PlayerTwo);
        }

        /**
         * This method receives a player and returns whether the player has jump moves available.
         */
        public bool PlayerHasJumpMove(Player i_Player)
        {
            const bool v_CheckJumpMovesOnly = true;

            return PlayerHasMoves(i_Player, v_CheckJumpMovesOnly);
        }

        /**
         * This method receives a move object *that was already played* and returns whether the soldier made a jump move and it has another jump move available.
         */
        private bool checkForDoubleJump(Move i_Move)
        {
            bool hasDoubleJump = false;
            Soldier lastMoveSoldier = m_CheckersBoard.GetSoldierAt(i_Move.TargetLocation);

            hasDoubleJump = i_Move.IsJumpMove && SoldierHasMoves(lastMoveSoldier, Move.k_JumpMovesOffset);

            return hasDoubleJump;
        }

        /**
         * This method receives a row index and column index in the board's matrix and returns the soldier located in that position.
         * This method is a bridge between the UI and the CheckersBoard because we don't want to expose the CheckersBoard itself to the UI.
         */
        public Soldier GetSoldierAt(byte i_RowIndex, byte i_ColIndex)
        {
            return m_CheckersBoard.GetSoldierAt(i_RowIndex, i_ColIndex);
        }

        /**
         * This method receives a Checkers Coordinates in the board and returns the soldier located in that position.
         * This method is a bridge between the UI and the CheckersBoard because we don't want to expose the CheckersBoard itself to the UI.
         */
        public Soldier GetSoldierAt(CheckersCoordinates i_Location)
        {
            return m_CheckersBoard.GetSoldierAt(i_Location);
        }

        /**
         * This method returns a move for the computer player which is generated by the AI class.
         */
        public Move GetMoveFromComputer()
        {
            Player currentPlayer = GetCurrentPlayer();
            Move computerMove = null;

            if (currentPlayer.IsComputer)
            {
                computerMove = AI.GetMove(currentPlayer);
            }

            return computerMove;
        }

        /**
         * This method returns the absolute value of the difference between the players' current game score.
         */
        public int GetCurrentGameScoreDifference()
        {
            return Math.Abs(m_PlayerOne.CurrentGameScore - m_PlayerTwo.CurrentGameScore);
        }

        private void OnGameEnded()
        {
            if (GameEnded != null)
            {
                GameEnded.Invoke(this);
            }
        }

        private void OnMoved(Move i_Move)
        {
            if (Moved != null)
            {
                Moved.Invoke(i_Move);
            }
        }

        private void OnSoldierRemoved(CheckersCoordinates i_Coordinates)
        {
            if (SoldierRemoved != null)
            {
                SoldierRemoved.Invoke(i_Coordinates);
            }
        }
    }
}