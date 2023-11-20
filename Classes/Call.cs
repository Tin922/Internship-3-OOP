
namespace Dump_3_domaci.Classes
{
    public enum CallStatus
    {
        Current,
        Missed,
        Completed,
    }
    internal class Call
    {
        public DateTime CallEstablishmentTime { get; set; }
        public CallStatus Status { get; set; }

        public Call(DateTime CallEstablishmentTime, CallStatus Status)
        {
            this.CallEstablishmentTime = CallEstablishmentTime;
            this.Status = Status;
        }
        public Call()
        {
            
        }
    };
}
