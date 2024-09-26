
using System.Text.Json;

namespace arbeidskrav1_emne5
{
    public class Phonebook
    {
        private Contact[] contacts = new Contact[100];
        private int contactCount = 0;

        public void AddContact(Contact contact)
        {
            if (contactCount < 100)
            {
                contacts[contactCount++] = contact;
            }
        }

        public Contact[] Search(string searchString)
        {
            return contacts
                .Where(c => c != null && (c.FirstName.Contains(searchString) || c.LastName.Contains(searchString) || c.MobileNumber.Contains(searchString)))
                .ToArray();
        }

        public Contact[] Sort(string criterion, string order)
        {
            Contact[] sortedContacts = contacts.Where(c => c != null).ToArray();

            switch (criterion.ToLower())
            {
                case "name":
                    sortedContacts = order.ToLower() == "desc"
                        ? sortedContacts.OrderByDescending(c => c.LastName).ThenByDescending(c => c.FirstName).ToArray()
                        : sortedContacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToArray();
                    break;
                case "birthday":
                    sortedContacts = order.ToLower() == "desc"
                        ? sortedContacts.OrderByDescending(c => c.Birthday).ToArray()
                        : sortedContacts.OrderBy(c => c.Birthday).ToArray();
                    break;
            }

            return sortedContacts;
        }

        public static string Display(Contact c)
        {
            return $"Name: {c.FirstName} {c.LastName}\nPhone: {c.MobileNumber}\nAddress: {c.Street}, {c.City}\nBirthday: {c.Birthday.ToShortDateString()}";
        }
        
        public void LoadContactsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                contacts = JsonSerializer.Deserialize<Contact[]>(jsonString);
                contactCount = contacts.Length;
            }
            else
            {
                Console.WriteLine($"File does not exist at: {filePath}");
            }
        }
        public Contact GetContactByIndex(int index)
        {
            if (index >= 0 && index < contactCount)
            {
                return contacts[index];
            }
            return null;
        }

    }
}
