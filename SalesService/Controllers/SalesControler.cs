using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Data.DbContexts;
using SalesService.Dtos;
using SalesService.Enitities;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesControler : ControllerBase
    {
        private readonly SalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public DbSet<Sale>  Entities => _context.Set<Sale>();

        public SalesControler(SalesDbContext context, IMapper mapper, 
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<SaleDto>>> GetAllSales(string date)
        {
            var query = Entities.OrderBy(x => x.Product.Brand).AsQueryable();

            if(!string.IsNullOrWhiteSpace(date))
            {
                query = query.Where(x => x.Updated.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            /*var sales = await Entities
                .Include(x => x.Product)
                .OrderBy(x => x.Product.Brand)
                .ToListAsync();

            return _mapper.Map<List<SaleDto>>(sales);

            // manually mapping
            var results = sales.Select(x => new SaleDto()
            {
                Id = x.Id,
            }).ToList();

            return results;*/
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSalesById(Guid id)
        {
            var sale = await Entities
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null) return NotFound();

            return _mapper.Map<SaleDto>(sale);
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSales(CreateSaleDto createSaleDto)
        {
            var sale = _mapper.Map<Sale>(createSaleDto);

            // TODO: add current user as seller
            sale.Seller = "test";

            Entities.Add(sale);

            var newSale = _mapper.Map<SaleDto>(sale);

            await _publishEndpoint.Publish(_mapper.Map<SalesCreated>(newSale));

            var result = await _context.SaveChangesAsync() > 0;

            if(!result)
            {
                return BadRequest("No se pudieron guardar los cambios en la base de datos");
            }

            return CreatedAtAction(nameof(GetSalesById), new {sale.Id}, newSale);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSales(Guid id, UpdateSaleDto updateSaleDto)
        {
            var sale = await Entities
                .Include(x => x.Product)
                .FirstOrDefaultAsync (x => x.Id == id);

            if (sale == null) 
                { 
                    return NotFound(); 
                }

            // TODO: Check seller == user

            sale.Product.Title = updateSaleDto.Title ?? sale.Product.Title;
            sale.Product.Description = updateSaleDto.Description ?? sale.Product.Description;
            sale.Product.Category = updateSaleDto.Category ?? sale.Product.Category;
            sale.Product.Brand = updateSaleDto.Brand ?? sale.Product.Brand;
            sale.Product.Quality = updateSaleDto.Quality ?? sale.Product.Quality;
            sale.Product.Image = updateSaleDto.Image ?? sale.Product.Image;
            sale.Price = updateSaleDto.Price ?? sale.Price;

            var result = await _context.SaveChangesAsync() > 0;

            if(!result)
            {
                return BadRequest("Problem updating the product");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSales(Guid id)
        {
            var sale = await Entities.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            // TODO: check selle == username

            Entities.Remove(sale);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return BadRequest("No se puedo eliminar el producto");
            }

            return Ok();
        }

    }
}
