using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Application.Awards;

public interface IAwardService
{
    Task<AwardIntervals> GetAwardIntervals();
}
