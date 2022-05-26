using System.Collections.Generic;

namespace BattleshipLiteLibrary.Models
{
    public class PlayerInfo
    {
        public string UserName { get; set; }
        public List<GridSpot> ShipLocations { get; set; } = new List<GridSpot>();
        public List<GridSpot> ShotGrid { get; set; } = new List<GridSpot>();
    }
}