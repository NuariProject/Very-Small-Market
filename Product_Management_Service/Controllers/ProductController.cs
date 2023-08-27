using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_Service.Context;
using Product_Management_Service.Models;
using Product_Management_Service.Models.DTO;
using Product_Management_Service.Repository.IRepository;
using System.Net;

namespace Product_Management_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _product;
        private readonly IMapper _mapping;
        public ProductController(IProductRepository product, IMapper mapping)
        {
            _product = product;
            this._response = new();
            _mapping = mapping;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetCategories()
        {
            try
            {
                IEnumerable<ProductDTO> categories = await _product.GetList();
                _response.Result = categories;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var product = await _product.GetProduct(ss => ss.ProductId == id && !ss.IsDelete);


                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = product;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode=HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] ProductCreatedDTO createDTO)
        {
            try
            {

                // custom error with modelstate
                if (await _product.GetProduct(ss => ss.Name.ToLower() == createDTO.Name.ToLower() && !ss.IsDelete) != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = new List<string>() { "Product already exists" };

                    return _response;
                }

                if (createDTO == null)
                {

                    return BadRequest(createDTO);
                }

                var product = _mapping.Map<ProductDTO>(createDTO);
                await _product.Create(product);

                _response.Result = product;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, _response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }


        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductUpdatedDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.ProductId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                ProductDTO model = _mapping.Map<ProductDTO>(updateDTO);
                await _product.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                // custom error with modelstate
                if (await _product.GetProduct(ss => ss.ProductId == id && !ss.IsDelete) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages = new List<string>() { "Category not already exists !" };

                    return BadRequest(_response);
                }

                await _product.Delete(id);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

    }
}
