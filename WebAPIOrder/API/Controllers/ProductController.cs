using API.Domain;
using API.Domain.DTOs;
using API.Repository.interfaces;
using API.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost()]
        // [Authorize]
        public IActionResult Create([FromBody] ProductDTO product)
        {

            var procutNew = _productService.Create(product);
            if (procutNew.Error is not true)
            {

                return Ok(new
                {
                    Error = false,
                    Message = "product successfully registered.",
                    Product = procutNew
                });
            }
            return NotFound(new
            {
                Error = true,
                Message = "Erro ao criar o produto"
            });
        }
        [HttpPost("byId")]
        // [Authorize]

        public IActionResult FindById([FromBody] GetGuidDTO dto)
        {
            var res = _productService.FindById(dto.Id);
            if (res.Error is true)
            {
                return NotFound(new
                {
                    Error = true,
                    Message = res.Message
                });
            }
            return Ok(new
            {
                Error = false,
                Message = res.Message,
                Product = res.Product
            });
        }

        [HttpPost("all")]
        // [Authorize]
        public IActionResult GetAll()
        {
            var response = _productService.FindProductAll();
            return Ok(new
            {
                products = response,
                length = response.Count
            });
        }
        [HttpPut()]
        // [Authorize]
        public IActionResult Update([FromBody] ProductDTO dto)
        {
            var response = _productService.FindAndUpdate(dto);

            if (response is not null)
            {
                return Ok(
                    new
                    {
                        Error = false,
                        Message = "product updated successfully"
                    });
            }
            return NotFound(
                new
                {
                    Error = true,
                    Message = $"no product with ID: {dto.Id} was found."
                });
        }
        [HttpDelete()]
        // [Authorize]
        public IActionResult Delete([FromBody] GetGuidDTO dto)
        {
            var res = _productService.FindAndDelete(dto);
            if (res.Error is true)
            {
                return NotFound(
                    new
                    {
                        Error = true,
                        Message = res.Message
                    });
            }
            return Ok(new
            {
                Error = false,
                Message = res.Message
            });
        }
    }
}