using System.Data.Common;
using System.Text;
using System.Text.Json;
using Data.SqlModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Car_Rental_Marketplace.Services
{

    public class ApiService
    {

        private readonly CarMarketplaceContext _db;

        public ApiService(CarMarketplaceContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
        }


        public async Task<bool> FetchCarData()
        {
            try
            {

                var httpClient = new HttpClient();
                var unifiedOffers = new List<CarRentalOffer>();

                var north = new NorthernRentals();
                var best = new BestRentals();
                var south = new SouthRentals();


                var northOffers = await north.FetchOffers();
                var bestOffers = await best.FetchOffers();
                var southOffers = await south.FetchOffers();

            
                unifiedOffers.AddRange(northOffers);
                unifiedOffers.AddRange(bestOffers);
                unifiedOffers.AddRange(southOffers);

                _db.CarRentalOffers.AddRange(unifiedOffers);
                _db.SaveChanges();


            }
           
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }
    }
}