using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Binding5  .Models
{
    public class Person : INotifyPropertyChanged
    {
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                if (city != value)
                {
                    city = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int num;

        public int Num
        {
            get => num; 
            set
            {
                if (num != value)
                {
                    num = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private static readonly string[] firstNames = { "Adam", "Bob", "Carl", "David", "Edgar", "Frank", "George", "Harry", "Isaac", "Jesse", "Ken", "Larry" };
        private static readonly string[] lastNames =
            { "Aaronson", "Bobson", "Carlson", "Davidson", "Enstwhile", "Ferguson", "Harrison", "Isaacson", "Jackson", "Kennelworth", "Levine" };
        private static readonly string[] cities = { "Boston", "New York", "LA", "San Francisco", "Phoenix", "San Jose", "Cincinnati", "Bellevue" };

        public static ObservableCollection<Person> CreatePeople(int count)
        {
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                Person p = new Person()
                {
                    FirstName = firstNames[r.Next(firstNames.Length)],
                    LastName = lastNames[r.Next(lastNames.Length)],
                    City = cities[r.Next(cities.Length)]
                };
                people.Add(p);
            }
            return people;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string caller = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
