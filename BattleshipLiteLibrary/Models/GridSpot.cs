namespace BattleshipLiteLibrary.Models
{
    public class GridSpot
    {
        // Represent a spot in the grid as A4, D3 etc. (letter-number pair)
        public string SpotLetter { get; set; }
        public int SpotNumber { get; set; }
        // initialise every spot to empty
        public SpotStatus Status { get; set; } = SpotStatus.Empty;
    }
}