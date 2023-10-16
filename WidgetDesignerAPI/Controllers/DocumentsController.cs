using CliWrap;
using CliWrap.Buffered;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using PuppeteerSharp;
using Serilog;
using System;
using System.Diagnostics;
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
        public async Task<bool> CreatePDfs([FromRoute] int templateid, [FromBody] List<dynamic> jsonDataList)
        {
            //try
            //{
            //    foreach (var jsonData in jsonDataList)
            //    {

            //        //var propertyNames = GetPropertyNames(jsonData);

            //        //foreach (var propertyName in propertyNames)
            //        //{
            //        //    Log.Information($"Property Name: {propertyName}");
            //        //}
            //        ExtractPropertyNames(jsonData);
            //        // Access dynamic properties using JsonElement
            //        var profilePhoto = jsonData.GetProperty("ProfilePhoto");
            //        var adharcardUniqueNo = jsonData.GetProperty("AdharcardUniqueNo");
            //        var adharcardProfile = jsonData.GetProperty("AdharcardProfile");

            //        // Perform your processing based on the dynamic data

            //        // Access specific properties
            //        var name = profilePhoto.GetProperty("name").GetString();
            //        var dob = profilePhoto.GetProperty("dob").GetString();
            //        var gender = profilePhoto.GetProperty("gender").GetString();

            //        // Accessing nested properties
            //        var address = profilePhoto.GetProperty("Address");
            //        var line1 = address.GetProperty("Line1").GetString();
            //        var line2 = address.GetProperty("Line2");
            //        var city = line2.GetProperty("city").GetString();
            //        var state = line2.GetProperty("state").GetString();

            //        // Accessing src
            //        var src = profilePhoto.GetProperty("src").GetString();

            //        // Print or use the values as needed
            //        //Console.WriteLine(name);
            //        //Console.WriteLine(dob);
            //        //Console.WriteLine(gender);
            //        //Console.WriteLine(line1);
            //        //Console.WriteLine(city);
            //        //Console.WriteLine(state);
            //        //Console.WriteLine(src);
            //        //Console.Log.InformationOut.Flush();
            //        Log.Information(name);
            //        Log.Information(dob);
            //        Log.Information(gender);
            //        Log.Information(line1);
            //        Log.Information(city);
            //        Log.Information(state);
            //        Log.Information(src);
            //        Log.Information("Application starting up...");
            //    }

            //    // Your processing logic here...

            //    return Ok("Successfully processed JSON data.");
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            //}
            //var htmlContent = "<html><body><h1>Hello, My first PDF file!</h1></body></html>"; // Replace with your HTML content
            //var htmlContent = "<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js\" integrity=\"sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM\" crossorigin=\"anonymous\"></script><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"><script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" integrity=\"sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI\" crossorigin=\"anonymous\"></script><link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\"><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.js\" integrity=\"sha512-pkNvyLwxqzI8F2twP8wOamoh34GlKQ+tyJblGUUshzmzlhx8pH8MLVWiSHaGtjhmDzFEyYKLBYJh6LB8SjqhQg==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-base-impl.min.js\" integrity=\"sha512-mhZn+o/RSWCNMh6NpFW4pTDzGcqwhL2xH2RdRHOQ5dnj5HDrlPwMuHZdAwB8ZOXZcIhR6IgzXUh4DuG13PsPDg==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-draggable.min.js\" integrity=\"sha512-sSFfSy2GIiKi4gnsGPMqDR9LtB9zpzfIrt0dZPvIwGfRT9+X96drxBHgVeKAnd2amnxUAfl53PVaCmzwC2EYLg==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-droppable.min.js\" integrity=\"sha512-sMgq5dN52XzHWsjhNiX5fFyulEdQBTE8o1bgz2NdOwWPtyyVhjl1vSqRdURE4Z46YQP9zPAuiBkKc3Eoxjx6dg==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-element.min.js\" integrity=\"sha512-zvWjOI2JNrhq7K/mfZu0zg6wHRd7iWDF5g7cc4QC6ZbGpIXlc6C1bFoDcj9KqQkU2G56x81qq+NQKRVNbDc2LQ==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-gridstack.min.js\" integrity=\"sha512-oW+gSIipurBmLwpZ118oDGrct/TRfAFn1xG5Si7/W5p3d7uQfkKMhWQFaZ4J3n4azI4weY7oHUirB+my7fk5oQ==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-manager.min.js\" integrity=\"sha512-7c2iOKYLmwWFiZ0bdYJ1p8EvDwJFbFBvKTfVgLBCpPlLiXlITMumR/9aymNLEGqfVNzQjg+hJKcATL3PvZhY9A==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable-handle.min.js\" integrity=\"sha512-39RxJSWIR/PeGV0VjWr2o04jZ2sO0r4epUc83D1dgmxeSq9DDG6oXpTNjBn7VYsjcKj5f62+48d3HrRIwoQ4Sw==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable.min.js\" integrity=\"sha512-70En4rJtciVQgKTXmsbWJB3oXifFA34mmM4NxdSmhZ5D1LHfcwwv8rnilzEPt5Pl6NWX0jywMvOCwojjlAiM9g==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-touch.min.js\" integrity=\"sha512-vjVJaLxpyUUIglJ57re2ma0FHFNgBylFQsScF927K2bYR/Jda2IusJlUz6b09EXCv198/jgywbA6iNLs6pkMpA==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-all.min.js\" integrity=\"sha512-d4yEeh4uaVGevmpY/WeT1O8D87goNW909xq3Wo234+wYkKY5CbG5UPtI0BopeJjUgadwtm63eMePpACIvbX05Q==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-engine.min.js\" integrity=\"sha512-r4mQQ0nN5Sr5XzAyv7bZA12Hl5XNFkVxFtsT1FyKswEDNxM2G9SmzGW7Io77T3sQV/LGzipg2kGV4krobVDZrQ==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-extra.min.css\" integrity=\"sha512-287EQpO1sItRDNvuCUARDlhpQs3qLRCMaidpOKp5BFu6EgcX3XxB92jmTvdXWW57Q9ImHcYqIHKx12EATT3sPA==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\" /><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.css\" integrity=\"sha512-pn/nPcd3DeaEfwRkSD6DFdXrLFoiGhUZ9LjVwzxmaY1LXRG9yooFrWbAzDBidAuN30rw9LmFI8/RMGiD4/1xsA==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\" /><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/types.min.js\" integrity=\"sha512-FqFjO4sevT7ilpbvbinXvm1xj1u5qmUclAmocAsOHbPxHob5D2fR3oTYTsSKdjlWBkyiv59qvtyQGNoz1HgYnQ==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><script src=\"https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/utils.min.js\" integrity=\"sha512-RR3u9WaZXGE5ajHDwcYoOhz2no/+hpxuA/nZVxuvmOkh7E1XjdH8neykDfow0Ag7IW5GU//3i7Vs13Q2nKX+pw==\" crossorigin=\"anonymous\" referrerpolicy=\"no-referrer\"></script><link href=\"cssTheme1.css\" rel = \"stylesheet\" /><link href=\"app.component.css\" rel=\"stylesheet\" /><link href=\"add-page.component.css\" rel=\"stylesheet\" /><body><div class=\"col-sm-12 col-md-10\" style = \"padding:0px;\"><div class=\"grid-container\"><div class=\"grid-stack\" id = \"advanced-grid\" style = \"width: 1050px;height: 1497.59px; min-height: 1497.6px;\"></div></div></div><div id = \"advanced-grid\"></div></body><script type=\"text/javascript\">;var serializedFull={{'margin':0,'column':400,'acceptWidgets':!0,'removable':'#trash','float':!0,'disableOneColumnMode':!0,'children':[{{'x':126,'y':7,'w':185,'h':35,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'}},{{'x':104,'y':46,'w':68,'h':114,'content':'<div><img class=\"imgcss\" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'}},{{'x':301,'y':102,'w':73,'h':14,'content':'<div class=\"adharnumbercss\">0000 1111 2222</div>','id':'1020'}},{{'x':225,'y':122,'w':76,'h':128,'content':'<div><img class=\"imgcss\" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'}},{{'x':42,'y':160,'w':73,'h':120,'content':'<div><img class=\"imgcss\" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'}},{{'x':134,'y':160,'w':59,'h':59,'content':'<div><img src=http://localhost:4200/assets/sampleAdharQr.jpg></img></div>','id':'1021'}},{{'x':43,'y':280,'w':185,'h':40,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'}}]}};grid=GridStack.addGrid(document.querySelector('#advanced-grid'),serializedFull)</script>";
            //var result = ConvertToPdf(htmlContent);
            //"C:\Program Files\Google\Chrome\Application\chrome.exe"--headless--disable - gpu--print - to - pdf 
              //  = "E:/QuikSyncProjects/Dynamic Html creation/output.pdf" "E:/QuikSyncProjects/Dynamic Html creation/tt.html"
            //string chromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"; // Replace with the actual path.
            //string inputHtmlFile = "E:/QuikSyncProjects/Dynamic Html creation/tt.html"; // Replace with the HTML file path.
            //string outputPdfFile = "E:/QuikSyncProjects/Dynamic Html creation/output.pdf"; // Replace with the output PDF file path.

            //string command = $"\"{chromePath}\" --headless --disable-gpu --print-to-pdf=\"{outputPdfFile}\" \"{inputHtmlFile}\"";
            string chromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            string outputPath = "E:\\QuikSyncProjects\\Dynamic Html creation\\output.pdf";
            string webpageUrl = $"E:\\QuikSyncProjects\\Dynamic Html creation\\tt.html";
            string command = $"\"{chromePath}\"";
            // var command = $@"""{chromePath}""";
            string printcmd = $"--print-to-pdf={outputPath}";

            //  var command = $@" --headless --disable-gpu --print-to-pdf=""{outputPath}"" {webpageUrl}";
            //  var cmd = $@""{chromePath}"";
            //   return Ok(command);
            var result = await Cli.Wrap(command)
                .WithArguments(new [] { "--headless", "--disable-gpu", printcmd, webpageUrl })
                .WithWorkingDirectory(AppDomain.CurrentDomain.BaseDirectory)
                  .ExecuteBufferedAsync();

            Log.Information(result.StandardOutput);
            Log.Information(result.StandardError);
            
            //using (Process process = new Process())
            //{
            //    process.StartInfo.FileName = "cmd.exe";
            //    process.StartInfo.Arguments = $"/C {command}";
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.Start();
            //    process.WaitForExit();
            //}
            //CreateHTMLFile(htmlContent);
            //try
            //{
            //    using (Process process = new Process())
            //    {
            //        process.StartInfo.FileName = "cmd.exe";
            //        process.StartInfo.Arguments = $"/C {command}";
            //        process.StartInfo.UseShellExecute = false;
            //        process.StartInfo.RedirectStandardOutput = true;
            //        process.StartInfo.RedirectStandardError = true;
            //        process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;  // Set the working directory if needed

            //        process.Start();
            //        string output = process.StandardOutput.ReadToEnd();
            //        string errorOutput = process.StandardError.ReadToEnd();

            //        process.WaitForExit();

            //        // You can handle the output and errorOutput as needed
            //        Console.WriteLine("Command Output:");
            //        Console.WriteLine(output);
            //        Console.WriteLine("Error Output:");
            //        Console.WriteLine(errorOutput);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            return true;
        }

        [HttpPost]
        [Route("GetPDF")]
        public async Task<bool> GetPDF([FromBody] string[] filepath)
        {
            string chromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";

            for (int i=0; i < filepath.Length; i++)
            {
                await CopyHTML(filepath[i]);
                var outFileName = filepath[i].Replace(".html", ".pdf");
                string outputPath = $"E:\\QuikSyncProjects\\Dynamic Html creation\\" + outFileName;         //"E:\\QuikSyncProjects\\Dynamic Html creation\\output.pdf";
                string webpageUrl = $"E:\\QuikSyncProjects\\Dynamic Html creation\\" + filepath[i];    //$"E:\\QuikSyncProjects\\Dynamic Html creation\\tt.html";
                string command = $"\"{chromePath}\"";
                // var command = $@"""{chromePath}""";
                string printcmd = $"--print-to-pdf={outputPath}";

                //  var command = $@" --headless --disable-gpu --print-to-pdf=""{outputPath}"" {webpageUrl}";
                //  var cmd = $@""{chromePath}"";
                //   return Ok(command);
                var result = await Cli.Wrap(command)
                    .WithArguments(new[] { "--headless", "--disable-gpu", printcmd, webpageUrl })
                    .WithWorkingDirectory(AppDomain.CurrentDomain.BaseDirectory)
                      .ExecuteBufferedAsync();

                Log.Information(result.StandardOutput);
                Log.Information(result.StandardError);
            }
            //using (Process process = new Process())
            //{
            //    process.StartInfo.FileName = "cmd.exe";
            //    process.StartInfo.Arguments = $"/C {command}";
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.Start();
            //    process.WaitForExit();
            //}
            //CreateHTMLFile(htmlContent);
            //try
            //{
            //    using (Process process = new Process())
            //    {
            //        process.StartInfo.FileName = "cmd.exe";
            //        process.StartInfo.Arguments = $"/C {command}";
            //        process.StartInfo.UseShellExecute = false;
            //        process.StartInfo.RedirectStandardOutput = true;
            //        process.StartInfo.RedirectStandardError = true;
            //        process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;  // Set the working directory if needed

            //        process.Start();
            //        string output = process.StandardOutput.ReadToEnd();
            //        string errorOutput = process.StandardError.ReadToEnd();

            //        process.WaitForExit();

            //        // You can handle the output and errorOutput as needed
            //        Console.WriteLine("Command Output:");
            //        Console.WriteLine(output);
            //        Console.WriteLine("Error Output:");
            //        Console.WriteLine(errorOutput);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            return true;
        }

            // [Route("{GetPageWidgets/id:int}")]
        private async Task<bool> CopyHTML(string filepath)
        {
            try
            {
               
                var fullpath = $"C:\\Users\\chaud\\Downloads\\" + filepath; 
                string fileName =  Path.GetFileName(fullpath); // Get the file name from the source file path
                string destinationPath = Path.Combine("E:\\QuikSyncProjects\\Dynamic Html creation\\", filepath); // Combine the destination folder and file name

                // Check if the source file exists
                if (System.IO.File.Exists(fullpath))
                {
                    // Copy the file to the destination folder
                    System.IO.File.Copy(fullpath, destinationPath, true); // The 'true' parameter overwrites the file if it already exists in the destination
                }
                else
                {
                    // Handle the case where the source file doesn't exist
                    Console.WriteLine("Source file does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the file copy process
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

            return true;
        }
        [HttpGet]
        [Route("CreateHTMLFile")]
        public void CreateHTMLFile(string htmlContent)
        {
            // Your HTML content as a string
            //   string htmlContent = "<html><head></head><body><h1>Hello, World!</h1></body></html>";
             //string htmlContent1 = $"https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script><link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous"><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.js" integrity="sha512-pkNvyLwxqzI8F2twP8wOamoh34GlKQ+tyJblGUUshzmzlhx8pH8MLVWiSHaGtjhmDzFEyYKLBYJh6LB8SjqhQg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-base-impl.min.js" integrity="sha512-mhZn+o/RSWCNMh6NpFW4pTDzGcqwhL2xH2RdRHOQ5dnj5HDrlPwMuHZdAwB8ZOXZcIhR6IgzXUh4DuG13PsPDg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-draggable.min.js" integrity="sha512-sSFfSy2GIiKi4gnsGPMqDR9LtB9zpzfIrt0dZPvIwGfRT9+X96drxBHgVeKAnd2amnxUAfl53PVaCmzwC2EYLg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-droppable.min.js" integrity="sha512-sMgq5dN52XzHWsjhNiX5fFyulEdQBTE8o1bgz2NdOwWPtyyVhjl1vSqRdURE4Z46YQP9zPAuiBkKc3Eoxjx6dg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-element.min.js" integrity="sha512-zvWjOI2JNrhq7K/mfZu0zg6wHRd7iWDF5g7cc4QC6ZbGpIXlc6C1bFoDcj9KqQkU2G56x81qq+NQKRVNbDc2LQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-gridstack.min.js" integrity="sha512-oW+gSIipurBmLwpZ118oDGrct/TRfAFn1xG5Si7/W5p3d7uQfkKMhWQFaZ4J3n4azI4weY7oHUirB+my7fk5oQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-manager.min.js" integrity="sha512-7c2iOKYLmwWFiZ0bdYJ1p8EvDwJFbFBvKTfVgLBCpPlLiXlITMumR/9aymNLEGqfVNzQjg+hJKcATL3PvZhY9A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable-handle.min.js" integrity="sha512-39RxJSWIR/PeGV0VjWr2o04jZ2sO0r4epUc83D1dgmxeSq9DDG6oXpTNjBn7VYsjcKj5f62+48d3HrRIwoQ4Sw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable.min.js" integrity="sha512-70En4rJtciVQgKTXmsbWJB3oXifFA34mmM4NxdSmhZ5D1LHfcwwv8rnilzEPt5Pl6NWX0jywMvOCwojjlAiM9g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-touch.min.js" integrity="sha512-vjVJaLxpyUUIglJ57re2ma0FHFNgBylFQsScF927K2bYR/Jda2IusJlUz6b09EXCv198/jgywbA6iNLs6pkMpA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-all.min.js" integrity="sha512-d4yEeh4uaVGevmpY/WeT1O8D87goNW909xq3Wo234+wYkKY5CbG5UPtI0BopeJjUgadwtm63eMePpACIvbX05Q==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-engine.min.js" integrity="sha512-r4mQQ0nN5Sr5XzAyv7bZA12Hl5XNFkVxFtsT1FyKswEDNxM2G9SmzGW7Io77T3sQV/LGzipg2kGV4krobVDZrQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-extra.min.css" integrity="sha512-287EQpO1sItRDNvuCUARDlhpQs3qLRCMaidpOKp5BFu6EgcX3XxB92jmTvdXWW57Q9ImHcYqIHKx12EATT3sPA==" crossorigin="anonymous" referrerpolicy="no-referrer" /><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.css" integrity="sha512-pn/nPcd3DeaEfwRkSD6DFdXrLFoiGhUZ9LjVwzxmaY1LXRG9yooFrWbAzDBidAuN30rw9LmFI8/RMGiD4/1xsA==" crossorigin="anonymous" referrerpolicy="no-referrer" /><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/types.min.js" integrity="sha512-FqFjO4sevT7ilpbvbinXvm1xj1u5qmUclAmocAsOHbPxHob5D2fR3oTYTsSKdjlWBkyiv59qvtyQGNoz1HgYnQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/utils.min.js" integrity="sha512-RR3u9WaZXGE5ajHDwcYoOhz2no/+hpxuA/nZVxuvmOkh7E1XjdH8neykDfow0Ag7IW5GU//3i7Vs13Q2nKX+pw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><link href="cssTheme1.css" rel = "stylesheet" /><link href="app.component.css" rel="stylesheet" /><link href="add-page.component.css" rel="stylesheet" /><body><div class="col-sm-12 col-md-10" style = "padding:0px;"><div class="grid-container"><div class="grid-stack" id = "advanced-grid" style = "width: 1050px;height: 1497.59px; min-height: 1497.6px;"></div></div></div><div id = "advanced-grid"></div></body><script type="text/javascript">;var serializedFull={'margin':0,'column':400,'acceptWidgets':!0,'removable':'#trash','float':!0,'disableOneColumnMode':!0,'children':[{'x':126,'y':7,'w':185,'h':35,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'},{'x':104,'y':46,'w':68,'h':114,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':301,'y':102,'w':73,'h':14,'content':'<div class="adharnumbercss">0000 1111 2222</div>','id':'1020'},{'x':225,'y':122,'w':76,'h':128,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':42,'y':160,'w':73,'h':120,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':134,'y':160,'w':59,'h':59,'content':'<div><img src=http://localhost:4200/assets/sampleAdharQr.jpg></img></div>','id':'1021'},{'x':43,'y':280,'w':185,'h':40,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'}]};grid=GridStack.addGrid(document.querySelector('#advanced-grid'),serializedFull)</script><script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script><link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous"><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.js" integrity="sha512-pkNvyLwxqzI8F2twP8wOamoh34GlKQ+tyJblGUUshzmzlhx8pH8MLVWiSHaGtjhmDzFEyYKLBYJh6LB8SjqhQg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-base-impl.min.js" integrity="sha512-mhZn+o/RSWCNMh6NpFW4pTDzGcqwhL2xH2RdRHOQ5dnj5HDrlPwMuHZdAwB8ZOXZcIhR6IgzXUh4DuG13PsPDg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-draggable.min.js" integrity="sha512-sSFfSy2GIiKi4gnsGPMqDR9LtB9zpzfIrt0dZPvIwGfRT9+X96drxBHgVeKAnd2amnxUAfl53PVaCmzwC2EYLg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-droppable.min.js" integrity="sha512-sMgq5dN52XzHWsjhNiX5fFyulEdQBTE8o1bgz2NdOwWPtyyVhjl1vSqRdURE4Z46YQP9zPAuiBkKc3Eoxjx6dg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-element.min.js" integrity="sha512-zvWjOI2JNrhq7K/mfZu0zg6wHRd7iWDF5g7cc4QC6ZbGpIXlc6C1bFoDcj9KqQkU2G56x81qq+NQKRVNbDc2LQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-gridstack.min.js" integrity="sha512-oW+gSIipurBmLwpZ118oDGrct/TRfAFn1xG5Si7/W5p3d7uQfkKMhWQFaZ4J3n4azI4weY7oHUirB+my7fk5oQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-manager.min.js" integrity="sha512-7c2iOKYLmwWFiZ0bdYJ1p8EvDwJFbFBvKTfVgLBCpPlLiXlITMumR/9aymNLEGqfVNzQjg+hJKcATL3PvZhY9A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable-handle.min.js" integrity="sha512-39RxJSWIR/PeGV0VjWr2o04jZ2sO0r4epUc83D1dgmxeSq9DDG6oXpTNjBn7VYsjcKj5f62+48d3HrRIwoQ4Sw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-resizable.min.js" integrity="sha512-70En4rJtciVQgKTXmsbWJB3oXifFA34mmM4NxdSmhZ5D1LHfcwwv8rnilzEPt5Pl6NWX0jywMvOCwojjlAiM9g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/dd-touch.min.js" integrity="sha512-vjVJaLxpyUUIglJ57re2ma0FHFNgBylFQsScF927K2bYR/Jda2IusJlUz6b09EXCv198/jgywbA6iNLs6pkMpA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-all.min.js" integrity="sha512-d4yEeh4uaVGevmpY/WeT1O8D87goNW909xq3Wo234+wYkKY5CbG5UPtI0BopeJjUgadwtm63eMePpACIvbX05Q==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-engine.min.js" integrity="sha512-r4mQQ0nN5Sr5XzAyv7bZA12Hl5XNFkVxFtsT1FyKswEDNxM2G9SmzGW7Io77T3sQV/LGzipg2kGV4krobVDZrQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack-extra.min.css" integrity="sha512-287EQpO1sItRDNvuCUARDlhpQs3qLRCMaidpOKp5BFu6EgcX3XxB92jmTvdXWW57Q9ImHcYqIHKx12EATT3sPA==" crossorigin="anonymous" referrerpolicy="no-referrer" /><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/gridstack.min.css" integrity="sha512-pn/nPcd3DeaEfwRkSD6DFdXrLFoiGhUZ9LjVwzxmaY1LXRG9yooFrWbAzDBidAuN30rw9LmFI8/RMGiD4/1xsA==" crossorigin="anonymous" referrerpolicy="no-referrer" /><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/types.min.js" integrity="sha512-FqFjO4sevT7ilpbvbinXvm1xj1u5qmUclAmocAsOHbPxHob5D2fR3oTYTsSKdjlWBkyiv59qvtyQGNoz1HgYnQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/gridstack.js/8.4.0/utils.min.js" integrity="sha512-RR3u9WaZXGE5ajHDwcYoOhz2no/+hpxuA/nZVxuvmOkh7E1XjdH8neykDfow0Ag7IW5GU//3i7Vs13Q2nKX+pw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><link href="cssTheme1.css" rel = "stylesheet" /><link href="app.component.css" rel="stylesheet" /><link href="add-page.component.css" rel="stylesheet" /><body><div class="col-sm-12 col-md-10" style = "padding:0px;"><div class="grid-container"><div class="grid-stack" id = "advanced-grid" style = "width: 1050px;height: 1497.59px; min-height: 1497.6px;"></div></div></div><div id = "advanced-grid"></div></body><script type="text/javascript">;var serializedFull={'margin':0,'column':400,'acceptWidgets':!0,'removable':'#trash','float':!0,'disableOneColumnMode':!0,'children':[{'x':126,'y':7,'w':185,'h':35,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'},{'x':104,'y':46,'w':68,'h':114,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':301,'y':102,'w':73,'h':14,'content':'<div class="adharnumbercss">0000 1111 2222</div>','id':'1020'},{'x':225,'y':122,'w':76,'h':128,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':42,'y':160,'w':73,'h':120,'content':'<div><img class="imgcss" src=http://localhost:4200/assets/logosatya.jpg></img></div>','id':'1016'},{'x':134,'y':160,'w':59,'h':59,'content':'<div><img src=http://localhost:4200/assets/sampleAdharQr.jpg></img></div>','id':'1021'},{'x':43,'y':280,'w':185,'h':40,'content':'<div><img src=http://localhost:4200/assets/sampleHeader.jpg></img></div>','id':'1017'}]};grid=GridStack.addGrid(document.querySelector('#advanced-grid'),serializedFull)</script>";

            // Define the file path and name where you want to save the HTML file
            string filePath = Path.Combine("E:/QuikSyncProjects/Dynamic Html creation", "myFile.html");

            try
            {
                // Save the HTML content to a file
                System.IO.File.WriteAllText(filePath, htmlContent);


            }
            catch (Exception ex)
            {

            }
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