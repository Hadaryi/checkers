using System;
using System.Collections.Generic;

namespace Checkers
{
    public class AI
    {
        /**
         * This method receives a Player and returns a valid random move that the player can make.
         */
        public static Move GetMove(Player i_Player)
        {
            List<Soldier> soldiers = i_Player.Soldiers;
            List<Move> possibleMoves;
            GameHandler gameInstance = GameHandler.GetInstance();
            Player currentPlayer = gameInstance.GetCurrentPlayer();

            possibleMoves = getPossibleMoves(soldiers, Move.k_JumpMovesOffset);

            // If the player doesn't have jump moves, then we should check if it has a normal move.
            if (possibleMoves.Count == 0)
            {
                possibleMoves = getPossibleMoves(soldiers, Move.k_NormalMovesOffset);
            }

            return getRandomMove(possibleMoves);
        }

        /**
         * This method receives a player's list of soldiers and a move offset and returns all the available moves the soldiers have with the given move offset.
         * A move offset is either 1 - for normal move, or 2 - for jump move.
         */
        private static List<Move> getPossibleMoves(List<Soldier> i_Soldiers, byte i_MovesOffset)
        {
            List<Move> possibleMoves = new List<Move>();
            GameHandler gameInstance = GameHandler.GetInstance();

            foreach (Soldier soldier in i_Soldiers)
            {
                List<Move> soldierMoves = gameInstance.GetPossibleSoldierMoves(soldier, i_MovesOffset);

                possibleMoves.AddRange(soldierMoves);
            }

            return possibleMoves;
        }

        /**
         * This method receives a list of moves and returns a random move from the list.
         */
        private static Move getRandomMove(List<Move> i_Moves)
        {
            Random randGenerator = new Random();
            int randomResult = randGenerator.Next(i_Moves.Count);
            Move randomMove = null;

            if (i_Moves.Count > 0)
            {
                randomMove = i_Moves[randomResult];
            }

            return randomMove;
        }
    }
}