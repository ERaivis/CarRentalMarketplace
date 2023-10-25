using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data.SqlModels;

public class BestRentals : ICarRentalOffer
{
    public string uniqueId { get; set; }

    public decimal rentalCost { get; set; }

    public string rentalCostCurrency { get; set; }

    public string vehicle { get; set; }

    public string sipp { get; set; }

    public string imageLink { get; set; }

    public string logo { get; set; }


    public override async Task<List<CarRentalOffer>> FetchOffers()
    {
        try{
            var url = "https://suppliers-test.dev-dch.com/api/v1/BestRentals/AvailableOffers";
            var httpClient = new HttpClient();

            var jsonString = await httpClient.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<List<BestRentals>>(jsonString);

            var unifiedOffers = new List<CarRentalOffer>();

            foreach (var offer in json){
                unifiedOffers.Add(offer.ToOffer());
            }

            return unifiedOffers;

        }
        catch (HttpRequestException ex){
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); ;
        }
    }
    public override CarRentalOffer ToOffer()
    {
        try{
            var vehicleArr = vehicle.Split(" ");
            var unifiedOffer = new CarRentalOffer
            {
                VehicleId = uniqueId,
                RentalCost = rentalCost,
                Currency = rentalCostCurrency,
                Make = vehicleArr[0],
                Model = vehicleArr[1],
                Sipp = sipp,
                ImageLink = imageLink,
                LogoLink = logo,
                SupplierId = "BestRentals",
            };
            return unifiedOffer;
        }
        catch (JsonException ex){
            throw new Exception(ex.Message);
        }
        catch (Exception ex){
            throw new Exception(ex.Message); ;
        }

    }
}


