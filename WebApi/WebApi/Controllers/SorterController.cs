using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;
using WebApi.Attributes;
using WebApi.Services.FileServices.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class SorterController : ControllerBase
    {
        private readonly IStorageService fileService;
        private readonly ILogger logger;

        public SorterController(ILogger<SorterController> logger, IStorageService fileService)
        {
            this.fileService = fileService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the last generated file
        /// </summary>
        /// <returns>Generated file content</returns>
        /// <response code="200">File data download success</response>
        /// <response code="404">File not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("GetLastSortedNumbersFileData")]
        public ActionResult GetLastSortedNumbersFileData()
        {
            try
            {
                var fileData = fileService.GetLastSortedNumbersData();
                if (fileData == null)
                {
                    return NotFound();
                }

                return File(fileData, MediaTypeNames.Text.Plain);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message, e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets stored data file by unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Generated file content</returns>
        /// <response code="200">File data download success</response>
        /// <response code="404">File not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("GetSortedNumbersFileDataById/{id}")]
        public ActionResult GetSortedNumbersFileDataById([GuidIsRequiredAndNotEmpty] Guid id)
        {
            try
            {
                var fileData = fileService.GetSortedNumbersDataById(id);
                if (fileData == null)
                {
                    return NotFound(id);
                }

                return File(fileData, MediaTypeNames.Text.Plain);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message, e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets list of all existing stored data ids
        /// </summary>
        /// <returns>List of existing sore data ids</returns>
        /// <response code="200">Request successful</response>
        /// <response code="404">No data is currently stored</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("GetExistingFileNames")]
        public ActionResult GetExistingIds()
        {
            try
            {
                var fileNames = fileService.GetExistingIds();
                if (!fileNames.Any())
                {
                    return NotFound("No files currently exist");
                }

                return Ok(fileNames);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message, e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Sorts a given number sequence and stores it on server
        /// </summary>
        /// <returns>Uri to get stored data</returns>
        /// <remarks>Example value [-5, -1.254, 5, 1, 9.5, 9.4, -1.7976931348623157E+308]</remarks>
        /// <response code="201">Data successfully stored</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("SortAndSaveNumbers")]
        public ActionResult SortAndStoreNumbers([ArrayMustContainAtLeastTwoValues] double[] numbers)
        {
            try
            {
                var id = fileService.SortAndStoreNumbers(numbers);
                return Created(new Uri("/Api/Sorter/GetSortedNumbersFileDataById/" + id.ToString(), UriKind.Relative), numbers);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message, e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}