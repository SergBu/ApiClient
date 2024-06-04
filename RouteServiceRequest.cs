using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient;

public class RouteServiceRequest
{
    public string Waypoints { get; }
    public DateTimeOffset DepartureTime { get; }

    public bool UseTolls { get; set; }
    public bool IgnoreJams { get; set; }
    public string AnswerMode { get; set; } = "Route";

    public RouteServiceRequest(string waypoints, DateTimeOffset departureTime)
    {
        Waypoints = waypoints;
        DepartureTime = departureTime;
    }
}
