namespace API;
internal class WebReader
{
  private static readonly HttpClient httpClient = new();

  /// <summary>
  /// Sends a synchronous GET request at `webPageAddress` and reads the
  /// response's content
  /// </summary>
  public static string Get(Uri webPageAddress)
  {
    // Need to create the request in order to use `Send()`
    using var request = new HttpRequestMessage(HttpMethod.Get, webPageAddress);

    // For a POST request, you could set `request.Content` for the body

    // `Send()` is the blocking (and more general) version of `GetAsync()`,
    // `PostAsync()` and other such methods
    using HttpResponseMessage response = httpClient.Send(request);

    // Similarly here, need to use `ReadAsStream()` as it seems to be the only
    // blocking way to get the response's string content
    using var streamReader = new StreamReader(response.Content.ReadAsStream());

    // If your API returns JSON data, you can use helpers inside the
    // System.Text.Json namespace to parse it (or deserialize, as they call it)
    // learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
    return streamReader.ReadToEnd();
  }
}
