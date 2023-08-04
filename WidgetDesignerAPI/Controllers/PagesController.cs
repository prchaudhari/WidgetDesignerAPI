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
        public async Task<IActionResult> AddPage([FromBody] Pages page)
        {
            //widget.Id = Guid.NewGuid();

            await _widgetDesignerAPIDbContext.Pages.AddAsync(page);
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
        public async Task<IActionResult> UpdatePage([FromRoute] int id, Pages updatepageRequest)
        {
            var page = await _widgetDesignerAPIDbContext.Pages.FindAsync(id);

            if (page == null)
                return NotFound();

            page.PageName = updatepageRequest.PageName;
            page.PageHtml = updatepageRequest.PageHtml;
            page.DataSourceJson = updatepageRequest.DataSourceJson;
            page.Description = updatepageRequest.Description;
            page.PageCSSUrl = updatepageRequest.PageCSSUrl;

            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(page);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var page = await _widgetDesignerAPIDbContext.Pages.FindAsync(id);

            if (page == null)
                return NotFound();

            _widgetDesignerAPIDbContext.Pages.Remove(page);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(page);
        }
    }
}