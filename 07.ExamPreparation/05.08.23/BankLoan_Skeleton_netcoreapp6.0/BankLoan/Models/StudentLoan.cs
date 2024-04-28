using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int StudentInterestRate = 1;
        private const double StudentAmount = 10000;
        public StudentLoan() 
            : base(StudentInterestRate, StudentAmount)
        {
        }
    }
}
