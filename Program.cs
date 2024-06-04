// See https://aka.ms/new-console-template for more information

using ApiClient;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Net.Http.Headers;

var routeServiceRequest = new RouteServiceRequest("345345", DateTimeOffset.Parse("30"));

var message = CreateMessage(routeServiceRequest);
try
{
    using var httpClient = HttpClientFactory.Create();
    var response = await httpClient.SendAsync(message);
    var content = await response.Content.ReadAsStringAsync();
    var responseObject = content.Json<RouteServiceResponse>();
    return (int)responseObject.RouteId;
}
catch (InvalidCastException castException)
{
    //_logger.LogError(castException, "Route service returned invalid data");
    throw;
}
catch (Exception e)
{
    //_logger.LogError(e, "Route service return exception");
    throw;
}

HttpRequestMessage CreateMessage(RouteServiceRequest routeServiceRequest)
{
    var queryParams = new Dictionary<string, string>
            {
                {"waypoints", routeServiceRequest.Waypoints},
                {"departuretime", routeServiceRequest.DepartureTime.ToString()},
                {"answermode", routeServiceRequest.AnswerMode},
            };

    var url = QueryHelpers.AddQueryString(@$"/cargorouter/route", queryParams);

    var message = new HttpRequestMessage(HttpMethod.Get, url);

    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "_tokenStorage.Token");

    return message;
}
