using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.API.Data;
using WidgetDesignerAPI.Models;

namespace WidgetDesignerAPI.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FontsController : Controller
    {
        private readonly WidgetDesignerAPIDbContext _widgetDesignerAPIDbContext;
        public FontsController(WidgetDesignerAPIDbContext widgetDesignerAPIDbContext)
        {
            this._widgetDesignerAPIDbContext = widgetDesignerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFonts()
        {
            var fonts = await _widgetDesignerAPIDbContext.Fonts.ToListAsync();

            return Ok(fonts);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetFont(int id)
        {
            var font = await _widgetDesignerAPIDbContext.Fonts.FirstOrDefaultAsync(x => x.Id == id);

            if(font == null)
                return NotFound();

            return Ok(font);
        }  

    }
}