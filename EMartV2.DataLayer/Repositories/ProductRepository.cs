using Dapper;
using EMartV2.DataLayer.Database;
using EMartV2.DataLayer.Interfaces;
using EMartV2.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMartV2.DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IDbConnectionFactory _dbConnectionFactory;
        //private readonly EMartContext _context;
        public ProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            //_context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {

                var connection = await _dbConnectionFactory.CreateConnectionAsync();
                await connection.ExecuteAsync("Insert into Products (Id, Name) VALUES (@id, @name)", new {id = product.Id, name = product.Name });

                return product;


                //await _context.Products.AddAsync(product);
                //var result = await _context.SaveChangesAsync();

                //if (result > 0)
                //    return product;
                //else
                //    return null;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var connection = await _dbConnectionFactory.CreateConnectionAsync();
                return await connection.QuerySingleOrDefaultAsync<Product>("select * from Products where Id=@id", new { id = id });


                //var prdouct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                //return prdouct;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                var connection = await _dbConnectionFactory.CreateConnectionAsync();
                //return await connection.QuerySingleOrDefaultAsync<List<Product>>("select * from Products");

                return await connection.QueryAsync<Product>("select * from Products");


                //var prdouct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                //return prdouct;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
