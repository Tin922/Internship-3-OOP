using Dump_3_domaci.Classes;

Dictionary<Contact, List<Call>> PhoneBook = new Dictionary<Contact, List<Call>>() { };

PhoneBook.Add(new Contact("Mate Matic", "0952134590", Preference.Normal), new List<Call> { new Call(new DateTime(2001, 11, 11), CallStatus.Missed) });
PhoneBook.Add(new Contact("Duje Dujic", "0922114585", Preference.Favorit), new List<Call> 
{
    { new Call(new DateTime(2001, 11, 11), CallStatus.Missed) },
    { new Call(new DateTime(2001,11,12) ,CallStatus.Completed)} 
}
);

while (true)
{
    Menu();
    int choice =int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            foreach (var item in PhoneBook)            
                Console.WriteLine($"{item.Key.nameSurname} {item.Key.phoneNumber} {item.Key.PreferenceStatus}");
            break;
        case 2:
            Console.WriteLine("Upisite ime kontakta");
            string contactName = GetString();
            Console.WriteLine("Unesite broj kontakta");
            string contactNumber = GetPhoneNumber(PhoneBook);
            Console.WriteLine("Unesite preferncu (Favorit, Normal, Blocked)");
            Preference contactPreference = GetPreference();           
                PhoneBook.Add(new Contact(contactName, contactNumber, contactPreference), new List <Call>());
            break;
        case 3:
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

            break;
        case 4:
            Console.WriteLine("Upisite ime kontakta kojem zelite promijeniti preferencu");
            string contactNameToChangePreference = GetString();
            foreach (var item in PhoneBook)
            {
                if (item.Key.nameSurname == contactNameToChangePreference)
                {
                    Console.WriteLine("Upisite novu preferencu");
                    Preference contactNewPreference = GetPreference();
                    item.Key.PreferenceStatus = contactNewPreference;
                    Console.WriteLine($"Preferenca je postavljena na {contactNewPreference} za kontakt {item.Key.nameSurname}");
                }
            }
            break;
        case 5:
            foreach(var item in PhoneBook)
            {
                Console.WriteLine("Ispis svih poziva za osobu "+ item.Key.nameSurname);
                foreach(var call in item.Value)
                    Console.WriteLine($"{call.Status} {call.CallEstablishmentTime}");
            }
            break;
        case 6:
            return;

        default:
            Console.WriteLine("krivi unos");
            break;

    }
}


static void Menu()
{
    Console.WriteLine("1 - Ispis svih kontakata");
    Console.WriteLine("2 - Dodavanje novih kontakata");
    Console.WriteLine("3 - Brisanje kontakta iz imenika");
    Console.WriteLine("4 - Editiranje preference kontakta");
    Console.WriteLine("5 - Editiranje preference kontakta");
    Console.WriteLine("5 - Ispis svih poziva");
    Console.WriteLine("5 - Izlaz iz aplikacije");
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
                Console.WriteLine("Taj broj vec postoji");
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
static void GetInt()
{

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
