using AutoMapper;
using GeekShopping.Product.API.Data.ValueObjects;
using GeekShopping.Product.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Product.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Model.Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Model.Product? product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            Model.Product product = _mapper.Map<Model.Product>(vo);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Model.Product? product = 
                    await _context.Products.Where(p=>p.Id == id)
                        .FirstOrDefaultAsync();
                if(product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}
