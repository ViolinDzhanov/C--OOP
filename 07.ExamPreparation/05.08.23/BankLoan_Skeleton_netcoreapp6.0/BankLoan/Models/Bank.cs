using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Bank name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get => capacity; private set => capacity = value; }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if (clients.Count == Capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan) => loans.Add(loan);
       

        public string GetStatistics()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append("Clients: ");

            if(!clients.Any())
            {
                sb.AppendLine("none");
            }
            else
            {
               var names = clients.Select(x => x.Name).ToArray();
                foreach (var client in clients) 
               sb.AppendLine(string.Join(", ", names));
            }

            sb.AppendLine($"Loans: { loans.Count}, Sum of Rates: {SumRates()}");
           
            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client) => clients.Remove(Client);
      

        public double SumRates()
        {
            if (loans.Count == 0)
            {
                return 0;
            }
            return double.Parse(loans.Select(l => l.InterestRate).Sum().ToString());
        }
       
    }
}
