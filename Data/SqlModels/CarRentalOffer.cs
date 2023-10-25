using System;
using System.Collections.Generic;

namespace Data.SqlModels;

public partial class CarRentalOffer
{
    public int Id { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string VehicleId { get; set; } = null!;

    public string Sipp { get; set; } = null!;

    public decimal RentalCost { get; set; }

    public string Currency { get; set; } = null!;

    public string ImageLink { get; set; } = null!;

    public string LogoLink { get; set; } = null!;

    public string SupplierId { get; set; } = null!;
}
