using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double HenWeightMultiplier = 0.35;
        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiplier => HenWeightMultiplier;

        protected override IReadOnlyCollection<Type> PreferredFood 
            => new HashSet<Type>() {typeof(Fruit), typeof(Meat), typeof(Seeds), typeof(Vegetable)};

        public override string ProduceSound()
       => "Cluck";
    }
}
