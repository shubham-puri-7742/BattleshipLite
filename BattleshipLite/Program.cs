using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BattleshipLiteLibrary;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameUI.WelcomeMessage();

            PlayerInfo activePlayer = GameUI.CreatePlayer("Player 1");
            PlayerInfo opponent = GameUI.CreatePlayer("Player 2");
            PlayerInfo winner = null;

            do
            {
                // Display grid from the current player and where s/he has fired
                GameUI.DisplayShotGrid(activePlayer);

                // Get the player's shot, determine if valid, and execute it
                GameUI.RecordPlayerShot(activePlayer, opponent);

                // Check if the game should continue
                bool continueGame = GameLogic.PlayerStillActive(opponent);

                // If yes, switch turns
                if (continueGame)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                // Else, declare the winner
                else
                {
                    winner = activePlayer;
                }
            } while (winner == null);

            GameUI.DeclareWinner(winner);

            Console.ReadLine();
        }
    }
}
