﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.API.Data;
using WidgetDesignerAPI.Models;

namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PagesController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;

        public PagesController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPages()
        {
            var pages = await _widgetDesignerAPIDbContext.Pages.ToListAsync();
            return Ok(pages);
        }

        [HttpPost]
        public async Task<IActionResult> AddPage([FromBody] PageModel page)
        {
            //widget.Id = Guid.NewGuid(); sd
            var newImage = new Pages
            {
                PageName = page.PageName,
                Description = page.Description,
                PageHtml = page.PageHtml,
                DataSourceJson = page.DataSourceJson,
                PageCSSUrl = page.PageCSSUrl,
                PageWidth = page.PageWidth,
                PageHeight = page.PageHeight,
                // Set other fields as needed
            };

            await _widgetDesignerAPIDbContext.Pages.AddAsync(newImage);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();
            //var newWidgets = page.Widgets.ToList();
            //var id = newImage.Id;
            page.Widgets.ToList().ForEach(item => item.PageId = newImage.Id);
            List<PageWidgetsDetails> details = new List<PageWidgetsDetails>();
            foreach (var item in page.Widgets)
            {
                var newWidget = new PageWidgetsDetails
                {
                    PageId = item.PageId,
                    WidgetId = item.WidgetId,
                    Width = item.Width,
                    Height = item.Height,
                    StartRow = item.StartRow,
                    StartCol = item.StartCol
                };
                details.Add(newWidget);
            }
           
            await _widgetDesignerAPIDbContext.PageWidgetsDetails.AddRangeAsync(details);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();
            return Ok(page);
        }

      
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPage(int id)
        {
            var page = await _widgetDesignerAPIDbContext.Pages.FirstOrDefaultAsync(x => x.Id == id);

            if(page == null)
                return NotFound();

            return Ok(page);
        }       

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePage([FromRoute] int id, PageModel updatepageRequest)
        {
            var page = await _widgetDesignerAPIDbContext.Pages.FindAsync(id);

            if (page == null)
                return NotFound();

            page.PageName = updatepageRequest.PageName;
            page.PageHtml = updatepageRequest.PageHtml;
            page.DataSourceJson = updatepageRequest.DataSourceJson;
            page.Description = updatepageRequest.Description;
            page.PageCSSUrl = updatepageRequest.PageCSSUrl;
            page.PageWidth = updatepageRequest.PageWidth;   
            page.PageHeight = updatepageRequest.PageHeight;
            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            //********************Details table********
            //Delete previous records for the given page
            var pageWidgets = await _widgetDesignerAPIDbContext.PageWidgetsDetails.Where(item=>item.PageId == id).ToListAsync();

            if (pageWidgets.Count() != null)
            {
                _widgetDesignerAPIDbContext.PageWidgetsDetails.Where(a=>a.PageId==id).ExecuteDelete();
                //await _widgetDesignerAPIDbContext.SaveChangesAsync();
            }

            //Add new records for widgets for the page
            List<PageWidgetsDetails> details = new List<PageWidgetsDetails>();
            foreach (var item in updatepageRequest.Widgets)
            {
                var newWidget = new PageWidgetsDetails
                {
                    PageId = item.PageId,
                    WidgetId = item.WidgetId,
                    Width = item.Width,
                    Height = item.Height,
                    StartRow = item.StartRow,
                    StartCol = item.StartCol
                };
                details.Add(newWidget);
            }

            await _widgetDesignerAPIDbContext.PageWidgetsDetails.AddRangeAsync(details);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();
            return Ok(page);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            //var page = await _widgetDesignerAPIDbContext.Pages.FindAsync(id);         

            //********************Details table********
            //Delete all widgets related to this page
            var pageWidgets = _widgetDesignerAPIDbContext.PageWidgetsDetails.Where(item => item.PageId == id).ToList();

            if (pageWidgets.Count > 0 )
            {
                _widgetDesignerAPIDbContext.PageWidgetsDetails.RemoveRange(pageWidgets);
                await _widgetDesignerAPIDbContext.SaveChangesAsync();
            }
            var page = _widgetDesignerAPIDbContext.Pages.AsTracking().FirstOrDefault(x => x.Id == id);
            if (page == null)
                return NotFound();
          //  _widgetDesignerAPIDbContext.Entry(page).State = EntityState.Deleted;
            _widgetDesignerAPIDbContext.Pages.Remove(page);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();
          
            return Ok(page);
        }
    }
}