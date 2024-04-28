namespace ShoppingSpree
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new();
            List<Product> products = new();

            try
            {
                string[] peopleTokens = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);

                foreach (var peopleToken in peopleTokens)
                {
                    string[] nameMoneyPair = peopleToken.Split('=', StringSplitOptions.RemoveEmptyEntries);

                    Person person = new(nameMoneyPair[0], decimal.Parse(nameMoneyPair[1]));

                    people.Add(person);
                }

                string[] productTokens = Console.ReadLine().Split(';');

                foreach (var productToken in productTokens)
                {
                    string[] productPricePair = productToken.Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Product product = new(productPricePair[0], decimal.Parse(productPricePair[1]));

                    products.Add(product);
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }

            string comand;

            while ((comand = Console.ReadLine()) != "END")
            {
                string[] personProductPair = comand.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string personName = personProductPair[0];
                string productName = personProductPair[1];

                Person person = people.FirstOrDefault(p => p.Name == personName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (person is not null && product is not null)
                {
                    Console.WriteLine(person.Add(product));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, people));
        }
    }
}