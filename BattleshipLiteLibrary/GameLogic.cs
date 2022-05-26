using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLiteLibrary.Models;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        public static void InitialiseGrid(PlayerInfo player)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D", "E" };
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            foreach (string l in letters)
            {
                foreach (int n in numbers)
                {
                    AddGridSpot(player, l, n);
                }
            }
        }

        private static void AddGridSpot(PlayerInfo player, string l, int n)
        {
            GridSpot spot = new GridSpot
            {
                SpotLetter = l,
                SpotNumber = n,
                Status = SpotStatus.Empty
            };

            player.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerInfo player, string location)
        {
            bool result = false;
            (string row, int col) = SplitShotInput(location);

            bool validLocation = ValidateGridLocation(player, row, col);
            bool spotOpen = ValidateShipLocation(player, row, col);

            if (validLocation && spotOpen)
            {
                player.ShipLocations.Add(new GridSpot
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = col,
                    Status = SpotStatus.Ship
                });

                result = true;
            }

            return result;
        }

        private static bool ValidateShipLocation(PlayerInfo player, string row, int col)
        {
            bool valid = true;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == col)
                {
                    valid = false;
                }
            }

            return valid;
        }

        private static bool ValidateGridLocation(PlayerInfo player, string row, int col)
        {
            bool valid = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == col)
                {
                    valid = true;
                }
            }

            return valid;
        }

        public static bool PlayerStillActive(PlayerInfo player)
        {
            bool active = false;
            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != SpotStatus.Sunk)
                {
                    active = true;
                }
            }

            return active;
        }

        public static int GetShotCount(PlayerInfo player)
        {
            int shotCount = 0;

            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != SpotStatus.Empty)
                {
                    ++shotCount;
                }
            }

            return shotCount;
        }

        public static (string row, int col) SplitShotInput(string shot)
        {
            string row = "";
            int col = 0;

            if (shot.Length != 2)
            {
                throw new ArgumentException("Invalid shot type.", "shot");
            }
            
            char[] inputArray = shot.ToArray();

            row = inputArray[0].ToString();
            col = int.Parse(inputArray[1].ToString());
            
            return (row, col);
        }

        public static bool ValidateShot(PlayerInfo player, string row, int col)
        {
            bool valid = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == col)
                {
                    if (gridSpot.Status == SpotStatus.Empty)
                    {
                        valid = true;
                    }
                }
            }

            return valid;
        }

        public static bool GetShotResult(PlayerInfo opponent, string row, int col)
        {
            bool hit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == col)
                {
                    hit = true;
                    ship.Status = SpotStatus.Sunk;
                }
            }

            return hit;
        }

        public static void MarkShot(PlayerInfo player, string row, int col, bool hit)
        {
            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == col)
                {
                    if (hit)
                    {
                        gridSpot.Status = SpotStatus.Hit;
                    }
                    else
                    {
                        gridSpot.Status = SpotStatus.Miss;
                    }
                }
            }
        }
    }
}
