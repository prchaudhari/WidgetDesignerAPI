using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using Serilog;
using System.Dynamic;
using System.Reflection;
using WidgetDesignerAPI.API.Data;


namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;

        public DocumentsController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
        }

        [HttpPost]
        [Route("{templateid:int}")]
        public async Task<IActionResult> CreatePDfs([FromRoute] int templateid, [FromBody] List<dynamic> jsonDataList)
        {
            try
            {
                foreach (var jsonData in jsonDataList)
                {

                    //var propertyNames = GetPropertyNames(jsonData);

                    //foreach (var propertyName in propertyNames)
                    //{
                    //    Log.Information($"Property Name: {propertyName}");
                    //}
                    ExtractPropertyNames(jsonData);
                    // Access dynamic properties using JsonElement
                    var profilePhoto = jsonData.GetProperty("ProfilePhoto");
                    var adharcardUniqueNo = jsonData.GetProperty("AdharcardUniqueNo");
                    var adharcardProfile = jsonData.GetProperty("AdharcardProfile");

                    // Perform your processing based on the dynamic data

                    // Access specific properties
                    var name = profilePhoto.GetProperty("name").GetString();
                    var dob = profilePhoto.GetProperty("dob").GetString();
                    var gender = profilePhoto.GetProperty("gender").GetString();

                    // Accessing nested properties
                    var address = profilePhoto.GetProperty("Address");
                    var line1 = address.GetProperty("Line1").GetString();
                    var line2 = address.GetProperty("Line2");
                    var city = line2.GetProperty("city").GetString();
                    var state = line2.GetProperty("state").GetString();

                    // Accessing src
                    var src = profilePhoto.GetProperty("src").GetString();

                    // Print or use the values as needed
                    //Console.WriteLine(name);
                    //Console.WriteLine(dob);
                    //Console.WriteLine(gender);
                    //Console.WriteLine(line1);
                    //Console.WriteLine(city);
                    //Console.WriteLine(state);
                    //Console.WriteLine(src);
                    //Console.Log.InformationOut.Flush();
                    Log.Information(name);
                    Log.Information(dob);
                    Log.Information(gender);
                    Log.Information(line1);
                    Log.Information(city);
                    Log.Information(state);
                    Log.Information(src);
                    Log.Information("Application starting up...");
                }

                // Your processing logic here...

                return Ok("Successfully processed JSON data.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            var htmlContent = "<html><body><h1>Hello, My first PDF file!</h1></body></html>"; // Replace with your HTML content
            var result = ConvertToPdf(htmlContent);
            return null;
        }

        private void ExtractPropertyNames(dynamic jsonObject)
        {
            if (jsonObject is IDictionary<string, object> dictionary)
            {
                foreach (var kvp in dictionary)
                {
                    var propertyName = kvp.Key;
                    var propertyValue = kvp.Value;

                    Log.Information($"Property Name: {propertyName}");

                    if (propertyValue is IDictionary<string, object>)
                    {
                        ExtractPropertyNames(propertyValue);
                    }
                }
            }
        }


        private IEnumerable<string> GetPropertyNames(dynamic jsonData)
        {
            if (jsonData is ExpandoObject expando)
            {
                return expando.Select(kv => kv.Key);
            }
            else
            {
                // Handle other cases if necessary
                return Enumerable.Empty<string>();
            }
        }

        private static string GetPropertyNameByIndex(dynamic jsonData, int index)
        {
            if (jsonData == null)
            {
                return null;
            }

            PropertyInfo[] properties = jsonData.GetType().GetProperties();

            if (index >= 0 && index < properties.Length)
            {
                return properties[index].Name;
            }

            return null;
        }

        private static object GetPropertyValue(dynamic jsonData, string propertyName)
        {
            if (jsonData == null)
            {
                return null;
            }

            Type type = jsonData.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(jsonData);
            }

            return null;
        }


        private async Task<bool> ConvertToPdf(string htmlContent)
        {
            // Specify the Chromium revision to use (you can check for the latest version)
            //string chromiumRevision = "123456"; // Replace with the actual revision number

            // Download Chromium
            await new BrowserFetcher().DownloadAsync(PuppeteerSharp.BrowserData.Chrome.DefaultBuildId);

            var launchOptions = new LaunchOptions
            {
                Headless = true
            };

            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {


                await page.SetContentAsync(htmlContent);

                // Generate a PDF buffer
                var pdfBuffer = await page.PdfStreamAsync();

                // Specify the output file path
                var outputPath = Path.Combine("wwwroot", "output", "output.pdf"); // Output file path, adjust as needed

                // Write the PDF buffer to the output file
                using (var fileStream = new FileStream(outputPath, FileMode.Create))
                {
                    await pdfBuffer.CopyToAsync(fileStream);
                }
            }

            return true;
        }



    }
}