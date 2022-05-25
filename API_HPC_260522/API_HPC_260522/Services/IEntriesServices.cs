using API_HPC_260522.Models.Dtos;
using API_HPC_260522.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Services
{
    public interface IEntriesServices
    {
        DtoEntries GetEntries();
        DtoCategories GetCategories();
        IActionResult SearchEntryByProtocol(bool IsHttp);
        IActionResult GetEntriesGroupByCategory();
        IActionResult GetAllCategories();
        IActionResult SearchEntriesByCategory(string CategoryName, bool IsDistinct = false);
        IActionResult SearchCategory(string CategoryName);
    }
}
