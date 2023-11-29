using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestAPITask2;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserData(int userId)
    {
        try
        {
            var apiUrl = $"https://jsonplaceholder.typicode.com/posts?userId={userId}";

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) => true
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(apiUrl);

                var userData = JsonConvert.DeserializeObject<ICollection<Post>>(response);

                return Ok(userData);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
