using GuildCars.Data;
using GuildCars.Data.Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{   
    public class VehiclesAPIController : ApiController
    {
        IVehiclesRepository _vehiclesRepository;
        public VehiclesAPIController(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        [Route("api/Vehicles/New")]
        [AcceptVerbs("GET")]
        public IHttpActionResult InventoryNew(decimal? minPrice, decimal? maxPrice, string quickSearch, int? maxYear, int? minYear )
        {

            try
            {
                var parameters = new InventorySearchParameters()
                {
                    QuickSearch = quickSearch,
                    MaxPrice = maxPrice,
                    MinPrice = minPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    TypeName = "New"

                };


                var result = _vehiclesRepository.SelectInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Vehicles/Used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult InventoryUsed(decimal? minPrice, decimal? maxPrice, string quickSearch, int? maxYear, int? minYear)
        {

            try
            {
                var parameters = new InventorySearchParameters()
                {
                    QuickSearch = quickSearch,
                    MaxPrice = maxPrice,
                    MinPrice = minPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    TypeName = "Used"

                };


                var result = _vehiclesRepository.SelectInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
