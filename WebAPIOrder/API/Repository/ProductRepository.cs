using API.Infrastructure.Context;
using API.Repository.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetAll()
        {
            var list = new List<ProductDTO>();
            var products = _context.Products.ToList();
            products.ForEach(item =>
            {
                list.Add(new ProductDTO()
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Value = item.Value,
                    Name = item.Name
                });
            });

            return list;
        }
        public ProductDTO GetProductById(Guid Id)
        {
            var product = _context.Products.Find(Id);
            return new ProductDTO()
            {
                Id = product!.Id,
                Name = product!.Name,
                Value = product!.Value,
                Quantity = product!.Quantity
            };
        }
        public void Create(ProductModel product)
        {
            var response = _context.Products.Add(product);
            _context.SaveChanges();
        }
        public String Update(ProductModel oldProduct, ProductDTO newProduct)
        {
            _context.Entry(oldProduct).CurrentValues.SetValues(
                new ProductModel()
                {
                    Id = oldProduct.Id,
                    Quantity = newProduct.Quantity,
                    Name = newProduct.Name,
                    Value = newProduct.Value
                }
            );
            _context.SaveChanges();

            return "product updated successfully.";
        }
        public void Delete(ProductModel product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public ProductModel FindById(Guid Id)
        {
            var product = _context.Products.Find(Id);

            if (product is not null)
            {
                return product;
            }
            return null!;
        }
    }
}