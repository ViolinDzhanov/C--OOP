using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Repositories
{
    public class BoothRepository : IRepository<IBooth>
    {
        private readonly List<IBooth> booths;
        public IReadOnlyCollection<IBooth> Models => booths.AsReadOnly();

        public void AddModel(IBooth model)
        => booths.Add(model);
    }
}
