namespace FlightBooking.Configurations;

/*
Class model for configuration of the Trip Advisor Search API Service

rapidApiKey - The API Key for a user of rapid API
rapidApiHost - The hostname used to access the TA API via rapidApi
ApiName - the name of the API to use from Rapid API
ApiUri - the full URI to use when calling the API

*/
public class TripAdvisorSearchConfiguration
{
    public string rapidApiKey { get; set; }
    public string rapidApiHost { get; set; }
    public string ApiName { get; set; }
    public string ApiUri { get; set; }
}
