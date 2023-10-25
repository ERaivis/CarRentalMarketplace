using Newtonsoft.Json;


namespace Data.SqlModels;

public partial class NorthernRentals : ICarRentalOffer
{
    public string id { get; set; }

    public decimal price { get; set; }

    public string currency { get; set; }

    public string vehicleName { get; set; }

    public string sippCode { get; set; }

    public string image { get; set; }

    public string supplierLogo { get; set; }



    public override async Task<List<CarRentalOffer>> FetchOffers()
    {
        try{

            var url = "https://suppliers-test.dev-dch.com/api/v1/NorthernRentals/GetRates";
            var httpClient = new HttpClient();

            var jsonString = await httpClient.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<List<NorthernRentals>>(jsonString);

            var unifiedOffers = new List<CarRentalOffer>();

            foreach (var offer in json)
            {
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
        try{

            var vehicleArr = vehicleName.Split(" ");
            var unifiedOffer = new CarRentalOffer
            {
                VehicleId = id,
                RentalCost = price,
                Currency = currency,
                Make = vehicleArr[0],
                Model = vehicleArr[1],
                Sipp = sippCode,
                ImageLink = image,
                LogoLink = supplierLogo,
                SupplierId = "NorthernRentals"
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


