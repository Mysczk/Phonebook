using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phone_list.Models;

namespace Phone_list
{

    class Program
    {
        // Main menu
        static int MainMenu()
        {
            int choose = 0;
            bool entered = false;
            while (!entered)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Vítejte v telefonním seznamu.");
                Console.WriteLine("Přidaní kontaktu: {0,20}", 1);
                Console.WriteLine("Vypsání kontaktů dle jmena: {0,10}", 2);
                Console.WriteLine("Vypsání kontaktů dle čísla: {0,10}", 3);
                Console.WriteLine("Vyhledávání podle jmena: {0,13}", 4);
                Console.WriteLine("Vyhledávání podle čísla: {0,13}", 5);
                Console.WriteLine("Ukončit aplikaci: {0,20}", 0);
                Console.WriteLine("-------------------------------------------------");
                Console.Write("Vaše volba: ");
                entered = int.TryParse(Console.ReadLine(), out choose);
            }
            return choose;
        }

        // Adding new contact
        static Contact NewContact()
        {
            Contact contact = new Contact();
            Console.Write("Zadejte jméno kontaktu: ");
            string name = Console.ReadLine();
            Console.Write("Zadejte číslo kontaktu: ");
            string number = Console.ReadLine();
            if (name.Length > 0 && number.Length > 0) {
                contact.Name = name;
                contact.Number = number;
            } else
            {
                Console.WriteLine("Neplatný vstup");
            }
            return contact;
        }

        // Ordering contacts by name and number
        static void ListOfPhonebook(List<Contact> phonebook, string orderBy)
        {
            IEnumerable<Contact> ordered;
            switch (orderBy.ToLower())
            {
                case "number":
                    ordered = phonebook.OrderBy(contact => contact.Number);
                    break;
                default:
                    ordered = phonebook.OrderBy(contact => contact.Name);
                    break;
            }
            
            foreach(Contact contact in ordered)
            {
                Console.WriteLine("{0,-10} {1,-10}", contact.Name, contact.Number);
            }

            Console.WriteLine("Stisknutím klávesy esc se vrátíte do hlavního menu");
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Escape)
            {
                info = Console.ReadKey(true);
            }
        }

        // Searching for contacts by name or number
        static void Find(List<Contact> phonebook, string findBy)
        {
            if (findBy.ToLower() == "name")
            {
                Console.Write("Zadejte jméno kontaktu: ");
                string name = Console.ReadLine();
                List<Contact> results = phonebook.FindAll(contact => contact.Name.Contains(name));
                foreach (Contact result in results)
                {
                    Console.WriteLine("{0,-10} {1,-10}", result.Name, result.Number);
                }
            }
            else
            {
                Console.Write("Zadejte číslo kontaktu: ");
                string number = Console.ReadLine();
                List<Contact> results = phonebook.FindAll(contact => contact.Number.Contains(number));
                foreach (Contact result in results)
                {
                    Console.WriteLine("{0,-10} {1,-10}", result.Name, result.Number);
                }
            }

            Console.WriteLine("Stisknutím klávesy esc se vrátíte do hlavního menu");
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Escape)
            {
                info = Console.ReadKey(true);
            }
        }

        static void Main(string[] args)
        {
            // Init phonebook array of Contact model
            // Name
            // Number
            List<Contact> phonebook = new List<Contact>();

            // Show main menu and wait for selection
            int task = MainMenu();
            while (task != 0)
            {
                switch (task) {
                    case 1:
                        // Add new contact
                        Contact newContact = NewContact();
                        if (newContact.Name != null && newContact.Number != null)
                        {
                            phonebook.Add(newContact);
                        }
                        break;
                    case 2:
                        // List of phonebook by name
                        ListOfPhonebook(phonebook, "name");
                        break;
                    case 3:
                        // List of phonebook by number
                        ListOfPhonebook(phonebook, "number");
                        break;
                    case 4:
                        // Search by name
                        Find(phonebook, "name");
                        break;
                    case 5:
                        // Search by number
                        Find(phonebook, "number");
                        break;
                    default:
                        // Unknown operation
                        Console.WriteLine("Neplatný vstup.");
                        break;
                }
                task = MainMenu();
            }
        }
    }
}
