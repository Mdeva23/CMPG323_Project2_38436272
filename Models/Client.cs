using System;
using System.Collections.Generic;

namespace CMPG323_Project2_38436272.Models;

public partial class Client
{
    public Guid ClientId { get; set; }

    public string? ClientName { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public DateTime? DateOnboarded { get; set; }

    //GET-SET FOR TOTALTIMESAVED AND TOTALCOSTSAVED
    public class SavingsResult
    {
        public double TotalTimeSaved { get; set; }
        public double TotalCostSaved { get; set; }
    }
}
