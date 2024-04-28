using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Models
{
    public class Topping
    {
        private const double DefaultCaloriesPerGram = 2;

        private Dictionary<string, double> toppingCalories;

        private string type;
        private double grams;

        public Topping(string type, double grams)
        {
            toppingCalories = 
                new Dictionary<string, double> {{"meat", 1.2}, {"veggies", 0.8}, {"cheese", 1.1}, {"sauce", 0.9}};
            Type = type;
            Grams = grams;
        }

        public string Type 
        {
            get => type; 
            private set
            {
                if (!toppingCalories.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                type = value;
            }
        }
        public double Grams
        {
            get => grams;
            private set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentException($"{Type} weight should be in the range [1..50].");
                }
                grams = value;
            }
        }
        public double Calories
        {
            get
            {
                double toppingTypeCaloriesModifier = toppingCalories[Type.ToLower()];

                return DefaultCaloriesPerGram * Grams * toppingTypeCaloriesModifier;
            }
        }
    }
}
