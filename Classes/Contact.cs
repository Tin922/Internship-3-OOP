

namespace Dump_3_domaci.Classes
{
    public enum Preference
    {
        Favorit,
        Normal,
        Blocked
    }
    internal class Contact
    {
        public string nameSurname { get; set; }
        public string phoneNumber { get; set; }
        public Preference PreferenceStatus { get; set; }

        public Contact(string nameSurname, string phoneNumber, Preference preferenceStatus)
        {
            this.nameSurname = nameSurname;
            this.phoneNumber = phoneNumber;
            PreferenceStatus = preferenceStatus;
        }
    }
}
