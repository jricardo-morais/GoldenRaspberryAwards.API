namespace GoldenRaspberryAwards.Domain.Entities;

public class AwardIntervals
{
    public IEnumerable<ProducerAwards>? Min { get; set; }
    public IEnumerable<ProducerAwards>? Max { get; set; }
}
