using Microsoft.AspNetCore.Mvc;
using API.Repository.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using API.services.interfaces;
using API.services;

namespace API.Controllers;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] CustomerModel customer)
    {
        var customerDb = _customerService.Register(customer);

        if (customer.Password!.Length < 8)
        {
            return NotFound(new
            {
                Error = true,
                Message = "password less than 8 characters"
            });
        }

        if (customerDb is not null)
        {
            return Ok(new
            {
                error = false,
                message = "request made successfully.",
                customer = customerDb,
                token = TokenService.GenerateToken(customer)
            });
        }

        return NotFound(new
        {
            Error = true,
            Message = $"a user with name already exists: {customer.Name}"
        });

    }
    [HttpPost("login")]
    public ActionResult Login([FromBody] CustomerModel customer)
    {
        var customerDb = _customerService.Login(customer);
        if (customerDb.Error)
        {
            return NotFound(new { 
                Error = customerDb.Error,
                Message = customerDb.Message
            });
        }

        return Ok(new
        {
            error = false,
            message = "request made successfully.",
            customer = customerDb.Customer,
            token = TokenService.GenerateToken(customer)
        });
    }
    [HttpPost("all")]
    [Authorize]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll();
        return Ok(new
        {
            customers,
            length = customers.Count
        });
    }

    [HttpPut()]
    [Authorize]
    public IActionResult ChangePassword([FromBody] CustomerDTO dto)
    {
        var customer = _customerService.ChangePassword(dto);
        return Ok(customer);
    }
}