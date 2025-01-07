using GoldenRaspberryAwards.Application.Movies;
using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Application.Awards;

public class AwardService : IAwardService
{
    private readonly IMovieService _movieService;

    public AwardService(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<AwardIntervals> GetAwardIntervals()
    {
        var movies = await _movieService.GetAll();

        if (!movies.Any())
            return new AwardIntervals();

        var awardIntervals = GetAwardIntervals(movies);
        return awardIntervals;
    }

    private AwardIntervals GetAwardIntervals(IEnumerable<Movie> movies)
    {
        var producerWinners = GetProducerWinners(movies);

        var intervals = producerWinners!.Where(p => p.Value.Count >= 2).Select(p => new ProducerAwards
        {
            Producer = p.Key,
            Interval = p.Value[1] - p.Value[0],
            PreviousWin = p.Value[0],
            FollowingWin = p.Value[1],
        });

        var minInterval = intervals.OrderBy(p => p.Interval).Select(p => p.Interval).First();
        var maxInterval = intervals.OrderByDescending(p => p.Interval).Select(p => p.Interval).First();

        var minIntervals = intervals.Where(p => p.Interval == minInterval);
        var maxIntervals = intervals.Where(p => p.Interval == maxInterval);

        return new AwardIntervals()
        {
            Min = minIntervals,
            Max = maxIntervals
        };
    }

    private Dictionary<string, List<int>> GetProducerWinners(IEnumerable<Movie> movies)
    {
        var moviesWins = movies.Where(m => m.Winner);

        var separators = new string[] { ",", " and ", ", and " };

        return moviesWins.SelectMany(m => m.Producers!.Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => (Producer: p.Trim(), m.Year)))
            .GroupBy(p => p.Producer)
            .ToDictionary(g => g.Key, g => g.Select(p => p.Year).OrderBy(y => y).ToList());
    }
}
