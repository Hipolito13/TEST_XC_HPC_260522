using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models.Dtos;
using API_HPC_260522.Models.Responses;
using API_HPC_260522.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Services
{
    public class EntriesServices : BaseService, IEntriesServices
    {
        private readonly ILogger<EntriesServices> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EntriesServices(ILogger<EntriesServices> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult GetAllCategories()
        {
            try
            {
                DtoCategories categories = GetCategories();
                if (categories.Count == 0)
                {
                    return OnError<CategoriesResponse>(new CategoriesResponse { IsValid = false, Errors = GetErrors($"The categories is not exist", HttpStatusCode.NotFound) }, HttpStatusCode.NotFound);
                }
                List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
                categories.Categories.ForEach(x =>
                {
                    categoryResponses.Add(new CategoryResponse 
                    {
                      CategoryName = x
                    });
                });
                return OnSuccess<CategoriesResponse>(new CategoriesResponse { Categories  = categoryResponses });
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error in NetworkRequestSampleAsync, error message: {0}, Stack Trace: {1}", e.Message, e.StackTrace);
                throw e;
            }
        }

        public DtoCategories GetCategories()
        {
            DtoCategories categoriesDto = _unitOfWork.EntriesRepository.GetCategories<DtoCategories>();
            return categoriesDto;
        }

        public DtoEntries GetEntries()
        {
            DtoEntries entriesDto = _unitOfWork.EntriesRepository.GetEntries<DtoEntries>();
            return entriesDto;
        }

        public IActionResult GetEntriesGroupByCategory()
        {
            try
            {
                DtoEntries entriesDto = GetEntries();
                if (!IsExistEntries(entriesDto))
                {
                    return GetNotFoundEntries();
                }
                List<DtoEntry> listEntriesDto = entriesDto.Entries.GroupBy(x => x.Category).Select(y => y.FirstOrDefault()).ToList();
                var entriesResponse = _mapper.Map<List<EntryResponse>>(listEntriesDto);
                return OnSuccess<EntriesResponse>(new EntriesResponse { Entries = entriesResponse });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in NetworkRequestSampleAsync, error message: {0}, Stack Trace: {1}", e.Message, e.StackTrace);
                throw e;
            }
        }

        public IActionResult SearchCategory(string CategoryName)
        {
            try
            {
                DtoCategories categories = GetCategories();
                if (categories.Count == 0)
                {
                    return OnError<BaseResponse>(new BaseResponse { IsValid = false, Errors = GetErrors($"The categories is not exist", HttpStatusCode.NotFound) }, HttpStatusCode.NotFound);
                }
                string category = categories.Categories.FirstOrDefault(x => x.Equals(CategoryName));
                if (!category.HasValue())
                {
                    return OnError<BaseResponse>(new BaseResponse { IsValid = false, Errors = GetErrors($"The category {CategoryName} is not exist", HttpStatusCode.NotFound) }, HttpStatusCode.NotFound);
                }
                return OnSuccess<CategoryResponse>(new CategoryResponse { CategoryName = category });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in NetworkRequestSampleAsync, error message: {0}, Stack Trace: {1}", e.Message, e.StackTrace);
                throw e;
            }
        }

        public IActionResult SearchEntriesByCategory(string CategoryName, bool IsDistinct = false)
        {
            try
            {

                if (!CategoryName.HasValue())
                {
                    return OnError<EntriesResponse>(new EntriesResponse { IsValid = false, Errors = GetErrors("The CategoryName is required", HttpStatusCode.BadRequest) }, HttpStatusCode.BadRequest);
                }

                DtoCategories categories = GetCategories();
                if (categories.Count == 0)
                {
                    return OnError<EntriesResponse>(new EntriesResponse { IsValid = false, Errors = GetErrors($"The {CategoryName} is not exist", HttpStatusCode.NotFound) }, HttpStatusCode.NotFound);
                }

                string categorySearch = categories.Categories.FirstOrDefault(c => c.Equals(CategoryName));
                if (!categorySearch.HasValue())
                {
                    return OnError<EntriesResponse>(new EntriesResponse { IsValid = false, Errors = GetErrors($"The {CategoryName} is not exist", HttpStatusCode.NotFound) }, HttpStatusCode.NotFound);
                }

                DtoEntries entriesDto = GetEntries();
                if (!IsExistEntries(entriesDto))
                {
                    return GetNotFoundEntries();
                }

                List<DtoEntry> listEntriesDto = new List<DtoEntry>();
                if (IsDistinct)
                {
                    listEntriesDto = entriesDto.Entries.Where(x => !x.Category.Equals(categorySearch)).ToList();
                }
                else
                {
                    listEntriesDto = entriesDto.Entries.Where(x => x.Category.Equals(categorySearch)).ToList();
                }
                var entriesResponse = _mapper.Map<List<EntryResponse>>(listEntriesDto);
                return OnSuccess<EntriesResponse>(new EntriesResponse { Entries = entriesResponse });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in NetworkRequestSampleAsync, error message: {0}, Stack Trace: {1}", e.Message, e.StackTrace);
                throw e;
            }
        }

        public IActionResult SearchEntryByProtocol(bool IsHttp)
        {
            try
            {
                DtoEntries entriesDto = GetEntries();
                if (!IsExistEntries(entriesDto))
                {
                    return GetNotFoundEntries();
                }
                var listEntriesDto = entriesDto.Entries.Where(c => c.HTTPS == IsHttp).ToList();
                var entriesResponse = _mapper.Map<List<EntryResponse>>(listEntriesDto);
                return OnSuccess<EntriesResponse>(new EntriesResponse { Entries = entriesResponse });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in NetworkRequestSampleAsync, error message: {0}, Stack Trace: {1}", e.Message, e.StackTrace);
                throw e;
            }
        }
    }
}
