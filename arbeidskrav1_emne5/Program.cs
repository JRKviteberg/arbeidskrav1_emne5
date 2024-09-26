namespace arbeidskrav1_emne5
{
    class Program
    {
        static void Main(string[] args)
        {
            Phonebook phonebook = new Phonebook();

            // Laster kontakter fra JSON-fil
            phonebook.LoadContactsFromFile("contacts.json");

            Console.WriteLine("Welcome to my phone book!");

            bool running = true;
            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Display a contact");
                Console.WriteLine("2. Search for a contact");
                Console.WriteLine("3. Sort contacts");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter the index of the contact to display:");
                        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < 100)
                        {
                            Contact contact = phonebook.GetContactByIndex(index);
                            if (contact != null)
                            {
                                Console.WriteLine(Phonebook.Display(contact));
                            }
                            else
                            {
                                Console.WriteLine("No contact found at this index.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid index. no work");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter a search string thingie:");
                        string searchString = Console.ReadLine();
                        Contact[] results = phonebook.Search(searchString);
                        for (int i = 0; i < results.Length; i++)
                        {
                            Console.WriteLine($"{i}. {results[i].FirstName} {results[i].LastName}");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Sort by 'name' or 'birthday'? (asc/desc)");
                        string criterion = Console.ReadLine();
                        string order = Console.ReadLine();
                        Contact[] sortedContacts = phonebook.Sort(criterion, order);
                        foreach (var contact in sortedContacts)
                        {
                            Console.WriteLine(Phonebook.Display(contact));
                        }
                        break;

                    case "4":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. try again");
                        break;
                }
            }
        }
    }
}
