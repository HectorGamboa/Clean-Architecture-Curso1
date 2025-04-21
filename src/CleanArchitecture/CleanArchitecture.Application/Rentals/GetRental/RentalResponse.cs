namespace CleanArchitecture.Application.Rentals.GetRental
{
    public sealed class RentalResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        public int Status { get; set; }
        public decimal RentalPrice { get; set; }
        public string ? RentalCurrency { get; set; }
        public decimal MaintenancePrice { get; set; }
        public string? MaintenanceCurrency { get; set; }
        public string? AccessoriesPrice { get; set; }
        public string? AccessoriesCurrency { get; set; }
        public decimal TotalPrice { get; set; }
        public string? TotalCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
