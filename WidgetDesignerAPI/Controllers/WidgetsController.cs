﻿﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.API.Data;
using WidgetDesignerAPI.Models;
using static System.Net.Mime.MediaTypeNames;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WidgetsController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IHostingEnvironment Environment;
        public WidgetsController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext, IWebHostEnvironment webHostEnvironment, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
            _webHostEnvironment = webHostEnvironment;
            Environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWidgets()
        {
            var widgets = await _widgetDesignerAPIDbContext.Widgets.ToListAsync();

            return Ok(widgets);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddWidget([FromBody] Widgets widget)
        //{
        //    //widget.Id = Guid.NewGuid();

        //    await _widgetDesignerAPIDbContext.Widgets.AddAsync(widget);
        //    await _widgetDesignerAPIDbContext.SaveChangesAsync();

        //    return Ok(widget);
        //}

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetWidget(int id)
        {
            var widget = await _widgetDesignerAPIDbContext.Widgets.FirstOrDefaultAsync(x => x.Id == id);

            if(widget == null)
                return NotFound();

            return Ok(widget);
        }

        [HttpGet]
        [Route("api/PageWidgets")]
       // [Route("{GetPageWidgets/id:int}")]
        public async Task<IActionResult> GetPageWidgets(int pageid)
        {
            var page =  _widgetDesignerAPIDbContext.PageWidgetsDetails.Where(x => x.PageId == pageid);

            if (page == null)
                return NotFound();

            return Ok(page);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateWidget([FromRoute] int id, [FromForm] WidgetModel updatewidgetRequest)
        {
            var widget = await _widgetDesignerAPIDbContext.Widgets.FindAsync(id);

            if (widget == null)
                return NotFound();
            if(updatewidgetRequest.WidgetIconUrl  != null)
            {
                string imagePath = UpdateImage(updatewidgetRequest.WidgetIconUrl, widget.WidgetIconUrl);
                widget.WidgetIconUrl = imagePath;
            }
           
            widget.WidgetName = updatewidgetRequest.WidgetName;
            widget.WidgetHtml = updatewidgetRequest.WidgetHtml;
            widget.DataSourceJson = updatewidgetRequest.DataSourceJson;
            widget.Description = updatewidgetRequest.Description;
            widget.Width = updatewidgetRequest.Width;
            widget.Height = updatewidgetRequest.Height;
            widget.StartRow = updatewidgetRequest.StartRow;
            widget.StartCol = updatewidgetRequest.StartCol;
            widget.DataBindingJsonNode = updatewidgetRequest.DataBindingJsonNode;
            widget.FontName = updatewidgetRequest.FontName;

            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(widget);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteWidget(int id)
        {
            var widget = await _widgetDesignerAPIDbContext.Widgets.FindAsync(id);

            if (widget == null)
                return NotFound();

            _widgetDesignerAPIDbContext.Widgets.Remove(widget);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();
            return Ok(widget);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] WidgetModel imageModel)
        {
            // Handle the image upload and store it in the project folder
            string imagePath = SaveImage(imageModel.WidgetIconUrl);

            // Save the image path and other information into the database
            var newImage = new Widgets
            {
                WidgetIconUrl = imagePath,
                WidgetName = imageModel.WidgetName,
                Description = imageModel.Description,
                WidgetHtml = imageModel.WidgetHtml,
                Width = imageModel.Width,
                Height = imageModel.Height,
                DataSourceJson = imageModel.DataSourceJson,
                DataBindingJsonNode = imageModel.DataBindingJsonNode,
                FontName = imageModel.FontName,
                StartCol = imageModel.StartCol,
                StartRow = imageModel.StartRow,
                
                // Set other fields as needed
            };

            await _widgetDesignerAPIDbContext.Widgets.AddAsync(newImage);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(newImage);
        }

        private string SaveImage(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return "";
            }
            // string uploadsFolder = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,"uploads");
            //// var uploadsFolder = Path.Combine(this.Environment.WebRootPath, "uploads");
            // if (!Directory.Exists(uploadsFolder))
            // {
            //     Directory.CreateDirectory(uploadsFolder);
            // }
            var imageName = Guid.NewGuid().ToString() + imageFile.FileName;// Path.GetExtension(imageFile.FileName);
            //var imagePath = Path.Combine(uploadsFolder, imageName);

            //using (var stream = new FileStream(imagePath, FileMode.Create))
            //{
            //    imageFile.CopyTo(stream);
            //}

            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string LogFilePath = "wwwroot/uploads";//builder.Build().GetSection("Path").GetSection("ImagesPath").Value;
            var imagePath = Path.Combine(LogFilePath, imageName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            var imageUrl = imageName;// Url.Content( Path.Combine(LogFilePath, imageName));
            return imageUrl;
        }

        private string UpdateImage(IFormFile imageFile,string updateUrl)
        {
            if (imageFile == null)
            {
                return "";
            }
            DeleteFile(updateUrl);
        
            var imageName = Guid.NewGuid().ToString() + imageFile.FileName;// Path.GetExtension(imageFile.FileName);
        
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string LogFilePath = "wwwroot/uploads";//builder.Build().GetSection("Path").GetSection("ImagesPath").Value;
            var imagePath = Path.Combine(LogFilePath, imageName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            var imageUrl = imageName;// Url.Content( Path.Combine(LogFilePath, imageName));
            return imageUrl;
        }
        private void DeleteFile(string filePath)
        {
            try
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                string fullPath = Path.Combine(wwwRootPath, filePath);
                
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    Console.WriteLine($"File {filePath} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"File {filePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}