using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Product.API.Domain.Data.Repositories;
using Product.API.ViewModel.Responses;

namespace Product.API.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;


    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = new ProductResponse (
            products: await _productRepository.GetProductsAsync(),
            startRequest: DateTime.Now
        );
        return Ok(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productRepository.GetProductAsync(id);
        if (product is null) return NoContent();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Domain.Entities.Product product)
    {
        var model = await _productRepository.AddProductAsync(product);
        return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Domain.Entities.Product product)
    {
        var model = await _productRepository.UpdateProductAsync(product);
        return Ok(model);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productRepository.DeleteProductAsync(id);
        return Ok(result);
    }

}
