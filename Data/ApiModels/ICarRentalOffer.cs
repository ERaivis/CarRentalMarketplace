using Data.SqlModels;

public abstract class ICarRentalOffer
{
    public abstract CarRentalOffer ToOffer();
    public  abstract  Task<List<CarRentalOffer>> FetchOffers();
}