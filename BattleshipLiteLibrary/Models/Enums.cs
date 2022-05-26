namespace BattleshipLiteLibrary.Models
{
    public enum SpotStatus
    {
        Empty, // 0 water
        Ship, // 1 ship placed
        Miss, // 3 missed
        Hit, // 4 ship hit
        Sunk // 4 ship sunk
    }
}