
using System.Runtime.CompilerServices;

namespace Dump_3_domaci.Classes
{
    public enum CallStatus
    {
        Current,
        Missed,
        Completed,
    }
    public static class EnumExtensions
    {
        public static CallStatus GetRandomEnumValue(this Type t)
        {
            return CallStatus.GetValues(t)
                .OfType<CallStatus>()
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();
        }
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
