
namespace Umsetzung_III.Interface
{
    public interface TimeDeliverer
    {
        int GetActualSpielMinute();
        int GetActualSpielSecond();
        int GetDurationOfHalfTime();
        int GetRemainingTimeInSeconds();
        bool GetIsTimeRunning();
    }
}
