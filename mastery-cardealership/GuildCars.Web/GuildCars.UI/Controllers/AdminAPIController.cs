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
    public class AdminAPIController : ApiController
    {
        IVehiclesRepository _vehiclesRepository;
        IModelsRepository _modelsRepository;
        IMakesRepository _makesRepository;
        ISpecialsRepository _specialsRepository;
        public AdminAPIController(IVehiclesRepository vehiclesRepository,IModelsRepository modelsRepository,IMakesRepository makesRepository, ISpecialsRepository specialsRepository)
        {
            _vehiclesRepository = vehiclesRepository;
            _modelsRepository = modelsRepository;
            _makesRepository = makesRepository;
            _specialsRepository = specialsRepository;
        }


        [Route("api/admin/vehicles")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetInventory(decimal? minPrice, decimal? maxPrice, string quickSearch, int? maxYear, int? minYear)
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
                    TypeName = null

                };


                var result = _vehiclesRepository.SelectInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/model/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModels(int id)
        {

            try
            {

                var result = _modelsRepository.GetModels(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/makes")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMakes()
        {
            try
            {

                var result = _makesRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/models")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModels()
        {
            try
            {

                var result = _modelsRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/specials")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSpecials()
        {
            try
            {

                var result = _specialsRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/specials/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int id)
        {
            try
            {

                _specialsRepository.RemoveSpecial(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
