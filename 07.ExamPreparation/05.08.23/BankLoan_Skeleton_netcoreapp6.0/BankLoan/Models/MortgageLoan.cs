using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int MortgageInterestRate = 3;
        private const double MortgageAmount = 50000;
        public MortgageLoan() 
            : base(MortgageInterestRate, MortgageAmount)
        {
        }
    }
}
