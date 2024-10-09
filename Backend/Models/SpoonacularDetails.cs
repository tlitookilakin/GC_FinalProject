namespace FinalProjectBackend.Models;

public class SearchResults
{
    public Results[] results { get; set; }
    public int offset { get; set; }
    public int number { get; set; }
    public int totalResults { get; set; }
}

public class Results
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
}