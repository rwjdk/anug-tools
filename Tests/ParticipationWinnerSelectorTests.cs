using SharedModels.Models.EventParticipants;
using SharedModels.Services;

namespace Tests;

public class ParticipationWinnerSelectorTests
{
    [Fact]
    public void IsRandom()
    {
        for (int x = 0; x < 1000; x++)
        {
            Dictionary<string, int> winners = new Dictionary<string, int>();
            ParticipationWinnerSelector selector = new ParticipationWinnerSelector();
            const int draws = 10000;
            const int participantCount = 20;
            for (int i = 0; i < draws; i++)
            {
                List<Participant> list = new List<Participant>();
                for (int count = 0; count < participantCount; count++)
                {
                    list.Add(new Participant($"Participant {count}", false, string.Empty));
                }

                list.Add(new Participant("Host 1", true, string.Empty));
                list.Add(new Participant("Host 2", true, string.Empty));
                list.Add(new Participant("Host 3", true, string.Empty));
                selector.MarkParticipantsAsWinners(list, false, 1);

                Participant winner = list.First(x => x.Winner);
                Assert.False(winner.EventHost);

                if (!winners.TryAdd(winner.Name, 1))
                {
                    winners[winner.Name]++;
                }
            }

            const decimal averageNumberOfWins = (decimal)draws / (decimal)participantCount * 1.0M;

            Assert.Equal(participantCount, winners.Count); //Statistically this could fail but very very unlikely
            foreach (KeyValuePair<string, int> winner in winners)
            {
                Assert.InRange(winner.Value, averageNumberOfWins - 100, averageNumberOfWins + 100); //+/- 100 wins Statistically this could fail but quite unlikely
            }
        }
    }
}