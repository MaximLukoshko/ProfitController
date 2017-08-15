namespace Tree.Interfaces
{
    public interface IOrderLine
    {
        int Year { get; set; }
        MonthEn Month { get; set; }
        int Day { get; set; }
        string DeviceName { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string JobType { get; set; }
        string InstalledDetails { get; set; }
        double Outgo { get; set; }
        double Income { get; set; }
        double Profit { get; }
    }
}
