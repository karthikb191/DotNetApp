using System;

namespace Domain;

//This is an entity class and is going to relate to an entry in the database
public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public DateTime Date { get; set; }
    public bool IsCancelled { get; set; }

    //location props
    public required string City { get; set; }
    public required string Venue { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
