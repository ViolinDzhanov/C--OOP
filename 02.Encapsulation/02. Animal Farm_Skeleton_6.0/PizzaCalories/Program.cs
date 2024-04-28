namespace PizzaCalories
{
    using PizzaCalories.Models;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] pizzaTokens = Console.ReadLine().Split();
                string[] doughTokens = Console.ReadLine().Split();

                Dough dough = new(doughTokens[1], doughTokens[2], double.Parse(doughTokens[3]));
                Pizza pizza = new(pizzaTokens[1], dough);

                string input;

                while((input = Console.ReadLine()) != "END")
                {
                    string[] toppingTokens = input.Split();

                    Topping topping = new(toppingTokens[1], double.Parse(toppingTokens[2]));

                    pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza.ToString());
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
             
            }
        }
    }
}