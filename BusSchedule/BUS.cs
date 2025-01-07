using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusSchedule;


public class BusArrival
{
    public string route_code { get; set; }
    public string veh_code { get; set; }
    public string btime2 { get; set; }
}

public class Route
{
    public string RouteCode { get; set; }
    public string LineID { get; set; }
    public string LineDescr { get; set; }
}

public class BusDataFetcher
{
    public BusDataFetcher(string? busStop)
    {
        if (busStop is not null)
            this.BusStop = busStop;
    }
    public string BusStop { get; set; } = "030019";
    private string _stopArrivalsUrl = $"http://telematics.oasa.gr/api/?act=getStopArrivals&p1=";
    private string _routesForStopUrl = $"http://telematics.oasa.gr/api/?act=webRoutesForStop&p1=";

    private readonly HttpClient _httpClient = new HttpClient();
    public readonly Dictionary<string, Route> _storedRoutes = new Dictionary<string, Route>();

    public async Task<List<BusArrival>> GetArrivalsAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(_stopArrivalsUrl + BusStop);
            return JsonConvert.DeserializeObject<List<BusArrival>>(response);
        }
        catch
        {
            return null;
        }
    }

    public async Task FetchRoutesForStopAsync(string routeCode)
    {
        try
        {
            if (!_storedRoutes.ContainsKey(routeCode))
            {
                var response = await _httpClient.GetStringAsync(_routesForStopUrl + BusStop);
                var routes = JsonConvert.DeserializeObject<List<Route>>(response);

                foreach (var route in routes)
                {
                    _storedRoutes.TryAdd(route.RouteCode, route);
                }
            }
        }
        catch
        {

        }
    }

    public StringBuilder GetArrivalsAsStringBuilder(List<BusArrival> arrivals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Line\t" + "Destination".PadRight(55) + "\tTime\tBus Code");
        foreach (var arrival in arrivals)
        {
            if (_storedRoutes.TryGetValue(arrival.route_code, out var route))
            {
                sb.AppendLine($"{route.LineID}\t{route.LineDescr.PadRight(55)}\t{arrival.btime2}\t{arrival.veh_code}");
            }
        }
        return sb;
    }

    public List<string> GetArrivalsAsList(List<BusArrival> arrivals)
    {
        var list = new List<string>();
        foreach (var arrival in arrivals)
        {
            if (_storedRoutes.TryGetValue(arrival.route_code, out var route))
            {
                list.Add($"{route.LineID} - {route.LineDescr} - {arrival.btime2} mins - Bus {arrival.veh_code}");
            }
        }
        return list;
    }

    public string GetColoredArrivals(List<BusArrival> arrivals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("\x1b[1mLine\t" + "Destination".PadRight(55) + "\tTime\tBus Code\x1b[0m");
        foreach (var arrival in arrivals)
        {
            if (_storedRoutes.TryGetValue(arrival.route_code, out var route))
            {
                string colorCode = arrival.btime2 switch
                {
                    var time when int.TryParse(time, out int minutes) && minutes <= 5 => "\x1b[31m", // Red for <= 5 mins
                    var time when int.TryParse(time, out int minutes) && minutes <= 15 => "\x1b[33m", // Yellow for <= 15 mins
                    _ => "\x1b[32m" // Green for > 15 mins
                };
                sb.AppendLine($"{colorCode}{route.LineID}\t{route.LineDescr.PadRight(55)}\t{arrival.btime2}\t{arrival.veh_code}\x1b[0m");
            }
        }
        return sb.ToString();
    }
    public string GetRichTextBoxColoredArrivals(List<BusArrival> arrivals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Line\t" + "Destination".PadRight(55) + "\tTime\tBus Code");
        foreach (var arrival in arrivals)
        {
            if (_storedRoutes.TryGetValue(arrival.route_code, out var route))
            {
                string colorCode = arrival.btime2 switch
                {
                    var time when int.TryParse(time, out int minutes) && minutes <= 5 => "Red",
                    var time when int.TryParse(time, out int minutes) && minutes is <= 12 and >= 6 => "Green",
                    _ => "White"
                };
                sb.AppendLine($"[{colorCode}]{route.LineID}\t{route.LineDescr.PadRight(55)}\t{arrival.btime2}\t{arrival.veh_code}[/{colorCode}]");
            }
        }
        return sb.ToString();
    }
}
