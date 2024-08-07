using System;
using System.Collections.Generic;

namespace CMPG323_Project2_38436272.Models;

public partial class JobTelemetry
{

    public int ProjectId { get; set; } 
    public int ClientId { get; set; }  
    public DateTime Date { get; set; } 
    public double TimeSaved { get; set; }
    public double CostSaved { get; set; } //MY CODE ENDS HERE

    public int Id { get; set; }

    public string? ProccesId { get; set; }

    public string? JobId { get; set; }

    public string? QueueId { get; set; }

    public string? StepDescription { get; set; }

    public int? HumanTime { get; set; }

    public string? UniqueReference { get; set; }

    public string? UniqueReferenceType { get; set; }

    public string? BusinessFunction { get; set; }

    public string? Geography { get; set; }

    public bool? ExcludeFromTimeSaving { get; set; }

    public string? AdditionalInfo { get; set; }

    public DateTime EntryDate { get; set; }
}
