using API_HPC_260522.Common.Filters;
using API_HPC_260522.Models.Requests;
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
    [ServiceFilter(typeof(LoggerFilter))]
    public class EntriesController : ControllerBase
    {

        private readonly IEntriesServices _entriesServices;

        public EntriesController(IEntriesServices entriesServices)
        {
            _entriesServices = entriesServices;
        } 

        /// <summary>
        /// Obtiene las Entradas con Https
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] EntryHttpRequest request)
        {
            return _entriesServices.SearchEntryByProtocol(request.IsHttp);
        }

        /// <summary>
        /// Obtiene las Entradas con todas las categorias sin duplicarlas
        /// </summary>
        /// <returns></returns>
        [HttpGet("/distinct/{Category}")]
        public IActionResult Get(string Category)
        {
            return _entriesServices.SearchEntriesByCategory(Category,true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/GroupBy")]
        public IActionResult Get()
        {
            return _entriesServices.GetEntriesGroupByCategory();
        }

        [HttpGet("/Search/{Category}")]
        public IActionResult Search(string Category)
        {
            return _entriesServices.SearchEntriesByCategory(Category);
        }
    }
}
