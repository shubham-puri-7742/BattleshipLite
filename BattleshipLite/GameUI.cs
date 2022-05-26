using System;
using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

namespace BattleshipLite
{
    internal class GameUI
    {
        internal static void DeclareWinner(PlayerInfo winner)
        {
            Console.WriteLine($"Congratulations { winner.UserName }! You won the game!");
            Console.WriteLine($"{ winner.UserName } took { GameLogic.GetShotCount(winner) } shots.");
        }

        internal static void RecordPlayerShot(PlayerInfo activePlayer, PlayerInfo opponent)
        {
            bool validShot = false;
            string row = "";
            int col = 0;
            
            do
            {
                // Get the player's shot (Format: A4, B2, etc.)
                string shot = GetShot(activePlayer);
                
                try
                {
                    // Parse the input
                    (row, col) = GameLogic.SplitShotInput(shot);
                    // Determine if valid - start again if invalid
                    validShot = GameLogic.ValidateShot(activePlayer, row, col);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    validShot = false;
                }

                if (!validShot)
                {
                    Console.WriteLine("Invalid shot. Please try again.");
                }
            } while (!validShot);

            // Execute the shot and record results
            bool hit = GameLogic.GetShotResult(opponent, row, col);
            GameLogic.MarkShot(activePlayer, row, col, hit);
        }

        internal static string GetShot(PlayerInfo player)
        {
            Console.Write($"{ player.UserName }: Please enter your shot (Format: A4, B2, etc.): ");
            string output = Console.ReadLine();
            return output;
        }

        // Consider adding grid row and column headings
        internal static void DisplayShotGrid(PlayerInfo activePlayer)
        {
            // row = letter
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;
            
            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                // Break rows when moving to a new letter
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }
                
                // Display unhit
                if (gridSpot.Status == SpotStatus.Empty)
                {
                    Console.Write($" { gridSpot.SpotLetter } { gridSpot.SpotNumber } ");
                }
                // Display hits
                else if (gridSpot.Status == SpotStatus.Hit)
                {
                    Console.Write("  X  ");
                }
                // Display misses
                else if (gridSpot.Status == SpotStatus.Miss)
                {
                    Console.Write("  O  ");
                }
                // anything else (? = Cheap placeholder for an exception)
                else
                {
                    Console.Write("  ?  ");
                }
            }
            
            // end with a newline
            Console.WriteLine();
        }

        // Show the welcome message
        internal static void WelcomeMessage()
        {
            // the title and a blank line
            Console.WriteLine("BATTLESHIP LITE");
            Console.WriteLine();
        }

        internal static PlayerInfo CreatePlayer(string playerName)
        {
            PlayerInfo info = new PlayerInfo();

            Console.WriteLine($"Player information for { playerName }:");

            // player name
            info.UserName = GetPlayerName();

            // load the grid
            GameLogic.InitialiseGrid(info);

            // ship placements
            PlaceShips(info);

            // clear the screen
            Console.Clear();

            return info;
        }

        internal static string GetPlayerName()
        {
            Console.Write("Enter your name: ");
            string output = Console.ReadLine();
            return output;
        }

        internal static void PlaceShips(PlayerInfo info)
        {
            do
            {
                Console.WriteLine($"Where do you want to place ship # { info.ShipLocations.Count + 1 }?: ");
                string location = Console.ReadLine();

                bool isValidLocation = false;

                try
                {
                    isValidLocation = GameLogic.PlaceShip(info, location);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                if (!isValidLocation)
                {
                    Console.WriteLine("Invalid location: Please try again.");
                }

            } while (info.ShipLocations.Count < 5);
        }
    }
}