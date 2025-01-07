using GoldenRaspberryAwards.API.Domain.Entities;

namespace GoldenRaspberryAwards.API.Application.Awards;

public interface IAwardService
{
    Task<AwardIntervals> GetAwardIntervals();
}
