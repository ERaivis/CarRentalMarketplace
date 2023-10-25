using Newtonsoft.Json;

namespace Data.SqlModels;

public partial class SouthRentals : ICarRentalOffer
{
    public string quoteNumber { get; set; }

    public decimal price { get; set; }

    public string currency { get; set; }

    public string vehicleName { get; set; }

    public string acrissCode { get; set; }

    public string imageLink { get; set; }

    public string logoLink { get; set; }


    public override async Task<List<CarRentalOffer>> FetchOffers()
    {
        try{

            var url = "https://suppliers-test.dev-dch.com/api/v1/SouthRentals/Quotes";
            var httpClient = new HttpClient();

            var jsonString = await httpClient.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<List<SouthRentals>>(jsonString);

            var unifiedOffers = new List<CarRentalOffer>();

            foreach (var offer in json){
                unifiedOffers.Add(offer.ToOffer());
            }

            return unifiedOffers;
        }
        catch (HttpRequestException ex){
            throw new Exception(ex.Message);

        }
        catch (Exception ex){
            throw new Exception(ex.Message); ;
        }

    }

    public override CarRentalOffer ToOffer()
    {
        try
        {

            var vehicle = vehicleName.Split(" ");
            var unifiedOffer = new CarRentalOffer{
                VehicleId = quoteNumber,
                RentalCost = price,
                Currency = currency,
                Make = vehicle[0],
                Model = vehicle[1],
                Sipp = acrissCode,
                ImageLink = imageLink,
                LogoLink = logoLink,
                SupplierId = "SouthRentals",
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
