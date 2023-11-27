using Dump_3_domaci.Classes;
using System.Linq;

Dictionary<Contact, List<Call>> PhoneBook = new Dictionary<Contact, List<Call>>() { };

PhoneBook.Add(new Contact("Mate Matic", "0952134590", Preference.Normal), new List<Call> { new Call(new DateTime(2001, 11, 11), CallStatus.Missed) });
PhoneBook.Add(new Contact("Duje Dujic", "0922114585", Preference.Favorit), new List<Call> 
{
    { new Call(new DateTime(2001, 11, 11), CallStatus.Missed) },
    { new Call(new DateTime(2001,11,12) ,CallStatus.Current)} 
}
);

while (true)
{
    Menu();
    int choice = GetInt();
    switch (choice)
    {
        case 1:
            PrintAllContacts(PhoneBook);
            break;
        case 2:
           AddNewContact(PhoneBook);
            break;
        case 3:
            DeleteContact(PhoneBook);
            break;
        case 4:
            ChangePreferenceOfContact(PhoneBook);
            break;
        case 5:
            CallManagement(PhoneBook);
            break;
        case 6:
            PrintAllCalls(PhoneBook);
            break;
        case 7:
            return;
        default:
            Console.WriteLine("Ne postoji ta opcija");
            break;

    }
}


static void Menu()
{
    Console.WriteLine("1 - Ispis svih kontakata");
    Console.WriteLine("2 - Dodavanje novih kontakata");
    Console.WriteLine("3 - Brisanje kontakta iz imenika");
    Console.WriteLine("4 - Editiranje preference kontakta");
    Console.WriteLine("5 - Upravljanje kontaktom");
    Console.WriteLine("6 - Ispis svih poziva");
    Console.WriteLine("7 - Izlaz iz aplikacije");
}
static void SubMenu()
{
    Console.WriteLine("1 - Ispis svih poziva sa tim konaktom");
    Console.WriteLine("2 - Kreiranje novog poziva");
    Console.WriteLine("3 - Preknite sadašnji aktivni poziv");
    Console.WriteLine("4 - Izlaz iz podmenua");
}

static string GetPhoneNumber(Dictionary<Contact, List<Call>> PhoneBook)
{
    string userInput;
    bool numberExists = false;
    do
    {
        numberExists = false;
        userInput = Console.ReadLine();

        foreach (var item in PhoneBook)
        {
            if (item.Key.phoneNumber == userInput)
            {
                Console.WriteLine("Taj broj vec postoji\nUnesite neki drugi broj");
                numberExists= true;
            }
        }
        
        if (userInput.Length < 9) Console.WriteLine("Broj treba imati minimalno 9 znamenki");
    } while (userInput.Length < 9 || numberExists);

    return userInput;
}
static string GetString()
{
    string userInput;

    do
    {

        userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput) || IsNumeric(userInput))
        {
            Console.WriteLine("Neispravan unos imena");
        }

    } while (string.IsNullOrWhiteSpace(userInput) || IsNumeric(userInput));

    return userInput;
}

static bool IsNumeric(string input)
{
    return double.TryParse(input, out _);
}
static int GetInt()
{
    while (true)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int result))
        {
            return result;
        }
        else
        {
            Console.WriteLine("Unesite broj");
        }
    }
}
    static Preference GetPreference()
{
    string userInput;
    Preference userInput2;

    do
    {
        userInput = Console.ReadLine();
        if (!Enum.TryParse(userInput, true, out userInput2) || (userInput2 != Preference.Favorit && userInput2 != Preference.Blocked && userInput2 != Preference.Normal))
        {
            Console.WriteLine("Preferenca mora biti Favorit, Normal ili Blocked");
        }
    } while (!Enum.TryParse(userInput, true, out userInput2) || (userInput2 != Preference.Favorit && userInput2 != Preference.Blocked && userInput2 != Preference.Normal));

    return userInput2;
}
static void PrintAllContacts(Dictionary<Contact, List<Call>> PhoneBook)
{
    foreach (var item in PhoneBook)
        Console.WriteLine($"{item.Key.nameSurname} {item.Key.phoneNumber} {item.Key.PreferenceStatus}");
}
static void AddNewContact(Dictionary<Contact, List<Call>> PhoneBook)
{
    Console.WriteLine("Upisite ime kontakta");
    string contactName = GetString();
    Console.WriteLine("Unesite broj kontakta");
    string contactNumber = GetPhoneNumber(PhoneBook);
    Console.WriteLine("Unesite preferncu (Favorit, Normal, Blocked)");
    Preference contactPreference = GetPreference();
    PhoneBook.Add(new Contact(contactName, contactNumber, contactPreference), new List<Call>());
    Console.WriteLine("Kontakt je uspjeno dodan");
}
static void DeleteContact(Dictionary<Contact, List<Call>> PhoneBook)
{
    Console.WriteLine("Upisite ime konakta kojeg zelite izbrisati");
    string contactNameToDelete = GetString();
    foreach (var item in PhoneBook)
    {
        if (item.Key.nameSurname.ToLower() == contactNameToDelete.ToLower())
        {
            Console.WriteLine($"Kontakt po imenu {item.Key.nameSurname} ce se izbrisati");
            PhoneBook.Remove(item.Key);
            return;
        }
    }
    Console.WriteLine($"Kontakt s imenom {contactNameToDelete} ne postoji ");
}
static void ChangePreferenceOfContact(Dictionary<Contact, List<Call>> PhoneBook)
{
    Console.WriteLine("Upisite ime kontakta kojem zelite promijeniti preferencu");
    string contactNameToChangePreference = GetString();
    foreach (var item in PhoneBook)
    {
        if (item.Key.nameSurname == contactNameToChangePreference)
        {
            Console.WriteLine("Upisite novu preferencu (Favorit, Blocked ili Normal)");
            Preference contactNewPreference = GetPreference();
            item.Key.PreferenceStatus = contactNewPreference;
            Console.WriteLine($"Preferenca je postavljena na {contactNewPreference} za kontakt {item.Key.nameSurname}");
            return;
        }
    }
    Console.WriteLine($"Kontakt s imenom {contactNameToChangePreference} ne postoji");
}
static void PrintAllCalls(Dictionary<Contact, List<Call>> PhoneBook)
{
    foreach (var item in PhoneBook)
    {
        Console.WriteLine("Ispis svih poziva za osobu " + item.Key.nameSurname);
        foreach (var call in item.Value)
            Console.WriteLine($"{call.Status} {call.CallEstablishmentTime}");
    }
}
static void CallManagement(Dictionary<Contact, List<Call>> PhoneBook)
{
    Console.WriteLine("Upisite ime kontakta s kojim zelite upravljati");
    string contactToView = GetString();
    bool foundContact = false;
    foreach (var item in PhoneBook)
    {
        if (item.Key.nameSurname.ToLower() == contactToView.ToLower())
        {
            
            while (true) 
            {
                foundContact = true;
                SubMenu();
                int choiceForSubMenu = GetInt();
                switch (choiceForSubMenu)
                {
                    case 1:
                        var sortedCalls = item.Value.OrderByDescending(o => o.CallEstablishmentTime);
                        foreach (var call in sortedCalls)
                            Console.WriteLine($"{call.CallEstablishmentTime} {call.Status}");
                        break;
                    case 2:
                        bool hasActiveCall = false;
                        bool isBlocked = false;
                        foreach (var call in PhoneBook)
                        {
                            hasActiveCall = call.Value.Any(o => o.Status == CallStatus.Current);
                            isBlocked = call.Key.PreferenceStatus == Preference.Blocked;
                        }
                        if (hasActiveCall)
                        {
                            Console.WriteLine("Postoji vec aktivan poziv.\nNe mozete uspostaviti novi poziv dok ne prekinete sadašnji");
                            break;
                        }
                        if (isBlocked)
                        {
                            Console.WriteLine("Kontakt je blokiran\nNe mozete uspostaviti poziv s njim");
                            break;
                        }
                        var response = typeof(CallStatus).GetRandomEnumValue();
                        if (response == CallStatus.Completed)
                        {
                            var radnom = new Random();
                            var durationOfCall = radnom.Next(1, 21);
                            Console.WriteLine("Poziv je u tijeku...");
                            Thread.Sleep(durationOfCall * 1000);
                            item.Value.Add(new Call(DateTime.Now, response));
                            Console.WriteLine("Poziv je završio");
                        }
                        else if (response == CallStatus.Missed)
                        {
                            Console.WriteLine("Poziv je propusten"); 
                            item.Value.Add(new Call(DateTime.Now, response));
                        }
                        else
                        {
                            Console.WriteLine("Poziv je aktivan");
                            item.Value.Add(new Call(DateTime.Now, response));

                        }
                        break;

                    case 3:
                        EndPhoneCall(PhoneBook);
                        break;

                    case 4:
                        Console.Clear();
                        return;
                    
                }
            }
        }

    }
    if (!foundContact)
        Console.WriteLine($"kontakt s imenom {contactToView} ne postoji");


}
static void EndPhoneCall(Dictionary<Contact, List<Call>> PhoneBook)
{
    foreach(var contact in PhoneBook)
    {
        foreach(var call in contact.Value)
        {
            if(call.Status == CallStatus.Current)
            {
                call.Status = CallStatus.Completed;
                Console.WriteLine($"Poziv s osobom {contact.Key.nameSurname} je prekinut");
                return;
            }
        }
    }
    Console.WriteLine("Nema poziva koji je u tijeku");
}