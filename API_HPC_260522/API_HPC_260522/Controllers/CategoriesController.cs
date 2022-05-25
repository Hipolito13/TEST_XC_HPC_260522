using API_HPC_260522.Common.Filters;
using API_HPC_260522.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_HPC_260522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authentication]
    public class CategoriesController : ControllerBase
    {

        private readonly IEntriesServices _entriesServices;

        public CategoriesController(IEntriesServices entriesServices)
        {
            _entriesServices = entriesServices;
        }


        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return _entriesServices.GetAllCategories();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{category}")]
        public IActionResult Get(string category)
        {
            return _entriesServices.SearchCategory(category);
        }
    }
}
