using System.Collections.Generic;
using WEBApi.Models;

namespace WEBApi.Services
{
    public interface IDiscountCodeService
    {
        public List<DiscountCode> GetAll();

        public DiscountCode GetById(string id);

        public void Create(DiscountCode item);

        public void Update(string id, DiscountCode item);

        public void Remove(string id);
    }
}