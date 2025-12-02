using SharedModels.Models.EventParticipants;

namespace SharedModels.Services;

public class ParticipationWinnerSelector
{
    public void MarkParticipantsAsWinners(IReadOnlyList<Participant> participants, bool includeHosts, int numberOfWinnersToDraw)
    {
        SetWinnerState(participants, false);
        IReadOnlyList<Participant> possibleWinners = participants;
        if (!includeHosts)
        {
            possibleWinners = participants.Where(x => !x.EventHost).ToList();
        }

        if (numberOfWinnersToDraw > participants.Count)
        {
            SetWinnerState(possibleWinners, true); //Oprah Mode! Everyone is a winner :-)
            return;
        }

        List<Participant> winners = new List<Participant>();
        while (winners.Count < numberOfWinnersToDraw)
        {
            Participant newWinner = possibleWinners[Random.Shared.Next(possibleWinners.Count)];
            if (!winners.Contains(newWinner))
            {
                winners.Add(newWinner);
            }
        }

        SetWinnerState(winners, true);

        void SetWinnerState(IReadOnlyList<Participant> participantsToAffect, bool isWinner)
        {
            foreach (Participant participant in participantsToAffect)
            {
                participant.Winner = isWinner;
            }
        }
    }
}