using System.Security.Cryptography.X509Certificates;
using System.Text;
using Car_Rental_Marketplace.Services;
using Data.SqlModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Car_Rental_Marketplace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Vehicle : ControllerBase
    {

        private readonly CarMarketplaceContext _db;
        private readonly ApiService _api;
        private readonly IMemoryCache _cache;

        public Vehicle(CarMarketplaceContext db, ApiService api, IMemoryCache cache)
        {
            _db = db;
            _api = api;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            try{
                await _api.FetchCarData();
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
            return Ok();
        }
        
        [HttpGet("search")]
        public IActionResult SearchOffer(string make = "", string model = "", string supplierId = "", decimal? minPrice = null, decimal? maxPrice = null)
        {

            var cacheKey = $"{make}-{model}-{supplierId}-{minPrice}-{maxPrice}";

            try
            {

                if (_cache.TryGetValue(cacheKey, out List<CarRentalOffer> cachedResult)){
                    return Ok(cachedResult);
                }

                var offers = _db.CarRentalOffers.AsQueryable();

                if (!string.IsNullOrEmpty(make)){
                    offers = offers.Where(x => x.Make.Contains(make));
                }
                if (!string.IsNullOrEmpty(model)){
                    offers = offers.Where(x => x.Model.Contains(model));
                }
                if (!string.IsNullOrEmpty(supplierId)){
                    offers = offers.Where(x => x.SupplierId.Contains(supplierId));
                }
                if (minPrice.HasValue && maxPrice.HasValue){
                    offers = offers.Where(x => x.RentalCost >= minPrice.Value && x.RentalCost <= maxPrice.Value);
                }
                if (minPrice.HasValue){
                    offers = offers.Where(x => x.RentalCost >= minPrice.Value);
                }
                if (maxPrice.HasValue){
                    offers = offers.Where(x => x.RentalCost <= maxPrice.Value);
                }

                var query = offers.OrderBy(x => x.RentalCost).ThenBy(x => x.SupplierId).ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions{
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                };
                _cache.Set(cacheKey, query, cacheEntryOptions);

                return Ok(query);

            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }
    }
}