using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PromotionController : ControllerBase
    {
        private readonly PromotionsContext _context;

        public PromotionController(PromotionsContext promotionsContext)
        {
            _context = promotionsContext;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public IEnumerable<Coupon> GetCoupons()
        {
            return _context.Coupons.AsNoTracking().ToArray();
        }
    }
}