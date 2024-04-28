﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class ForwardWing : Player
    {
        private const double DefaultRating = 5.5;
        private const double IncreaseRatingIndex = 1.25;
        private const double DecreaseRatingIndex = 0.75;
        public ForwardWing(string name)
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
