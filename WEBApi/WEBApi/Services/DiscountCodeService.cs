using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using WEBApi.Models;
using WEBApi.Repositories.DiscountCodes;


namespace WEBApi.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDiscountCodesRepository _discountCodesRepository;

        public DiscountCodeService(IDiscountCodesRepository discountCodesRepository)
        {
            _discountCodesRepository = discountCodesRepository;
        }
        public List<DiscountCode> GetAll() =>
            _discountCodesRepository.GetAll();

        public DiscountCode GetById(string id) =>
            _discountCodesRepository.GetById(id);

        public void Create(DiscountCode discountCode)
        {
            _discountCodesRepository.Create(discountCode);
        }

        public void Update(string id, DiscountCode newDiscountCode) =>
            _discountCodesRepository.Update(id, newDiscountCode);

        public void Remove(string id) =>
            _discountCodesRepository.Remove(id);
    }
}
