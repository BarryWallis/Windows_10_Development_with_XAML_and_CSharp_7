using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private string firstName;
        public string FirstName
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return firstName; }
#pragma warning restore IDE0027 // Use expression body for accessors
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string lastName;
        public string LastName
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return lastName; }
#pragma warning restore IDE0027 // Use expression body for accessors
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
    }
}
