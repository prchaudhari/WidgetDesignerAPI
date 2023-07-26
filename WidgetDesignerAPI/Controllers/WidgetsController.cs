﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.API.Data;
using WidgetDesignerAPI.Models;

namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WidgetsController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;

        public WidgetsController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWidgets()
        {
            var widgets = await _widgetDesignerAPIDbContext.Widgets.ToListAsync();

            return Ok(widgets);
        }

        [HttpPost]
        public async Task<IActionResult> AddWidget([FromBody] Widgets widget)
        {
            //widget.Id = Guid.NewGuid();

            await _widgetDesignerAPIDbContext.Widgets.AddAsync(widget);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(widget);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetWidget(int id)
        {
            var widget = await _widgetDesignerAPIDbContext.Widgets.FirstOrDefaultAsync(x => x.Id == id);

            if(widget == null)
                return NotFound();

            return Ok(widget);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, Widgets updatewidgetRequest)
        {
            var widget = await _widgetDesignerAPIDbContext.Widgets.FindAsync(id);

            if (widget == null)
                return NotFound();

            widget.WidgetName = updatewidgetRequest.WidgetName;
            widget.WidgetHtml = updatewidgetRequest.WidgetHtml;
            widget.DataSourceJson = updatewidgetRequest.DataSourceJson;
            widget.Width = updatewidgetRequest.Width;
            widget.Height = updatewidgetRequest.Height;

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
    }
}