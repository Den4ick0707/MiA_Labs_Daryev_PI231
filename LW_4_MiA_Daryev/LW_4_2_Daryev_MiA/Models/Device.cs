namespace LW_4_2_Daryev_MiA.Models
{
    public enum DeviceType
    {
        PersonalComputer,
        Console,
        PortativeConsole
    }
    public class Device
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DeviceType Type { get; set; }
        public int LandlordID { get; set; }
    }
}
