namespace GoldenRaspberryAwards.API.Domain.Entities;

public class ProducerAwards
{
    public string? Producer { get; set; }
    public int Interval { get; set; }
    public int PreviousWin { get; set; }
    public int FollowingWin { get; set; }

}
