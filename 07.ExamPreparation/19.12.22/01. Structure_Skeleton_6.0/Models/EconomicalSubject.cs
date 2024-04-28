using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        public EconomicalSubject(int id, string name) 
            : base(id, name, 1.0)
        {
        }
    }
}
