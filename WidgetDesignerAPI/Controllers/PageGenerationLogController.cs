﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.API.Data;
using WidgetDesignerAPI.Models;

namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PageGenerationLogController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;

        public PageGenerationLogController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPageGenerationLog()
        {
            var pageGenerationLog = await _widgetDesignerAPIDbContext.PageGenerationLog.ToListAsync();

            return Ok(pageGenerationLog);
        }

        [HttpGet]
        [Route("{pageid:int}")]
        public async Task<IActionResult> GetPageGenerationLogsByPageId(int pageid)
        {
            var pageGenerationLog = await _widgetDesignerAPIDbContext.PageGenerationLog.Where(a=>a.PageId==pageid).ToListAsync();

            return Ok(pageGenerationLog);
        }

        [HttpPost]
        public async Task<IActionResult> AddPageGenerationLog([FromBody] PageGenerationLog pageGenerationLog)
        {
            await _widgetDesignerAPIDbContext.PageGenerationLog.AddAsync(pageGenerationLog);
            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(pageGenerationLog);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPageGenerationLog(int id)
        {
            var pageGenerationLog = await _widgetDesignerAPIDbContext.PageGenerationLog.FirstOrDefaultAsync(x => x.Id == id);

            if(pageGenerationLog == null)
                return NotFound();

            return Ok(pageGenerationLog);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePageGenerationLog([FromRoute] int id, PageGenerationLog updatePageGenerationLog)
        {
            var pageGenerationLog = await _widgetDesignerAPIDbContext.PageGenerationLog.FindAsync(id);

            if (pageGenerationLog == null)
                return NotFound();

            pageGenerationLog.FullHTML = updatePageGenerationLog.FullHTML;
            pageGenerationLog.FileName = updatePageGenerationLog.FileName;
            pageGenerationLog.Status = updatePageGenerationLog.Status;
            pageGenerationLog.CreationTime = updatePageGenerationLog.CreationTime;

            await _widgetDesignerAPIDbContext.SaveChangesAsync();

            return Ok(pageGenerationLog);
        }

    }
}