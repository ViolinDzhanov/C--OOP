using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double DefaultRating = 2.5;
        private const double IncreaseRatingIndex = 0.75;
        private const double DecreaseRatingIndex = 1.25;
        public Goalkeeper(string name) 
            : base(name, DefaultRating)
        {
        }

        public override void DecreaseRating()
        {
            base.Rating -= DecreaseRatingIndex;

            if (base.Rating < 1)
            {
                base.Rating = 1;
            }
        }

        public override void IncreaseRating()
        {
            base.Rating += IncreaseRatingIndex;

            if (base.Rating > 10)
            {
                base.Rating = 10;
            }
        }
    }
}
