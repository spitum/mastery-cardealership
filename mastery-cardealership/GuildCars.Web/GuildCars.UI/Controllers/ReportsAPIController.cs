using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        ApplicationDbContext _context;
        IPurchasesRepository _purchasesRepository;
        IVehiclesRepository _vehiclesRepository;
        public ReportsAPIController(IPurchasesRepository purchasesRepository, IVehiclesRepository vehiclesRepository)
        {
            _context = new ApplicationDbContext();
            _purchasesRepository = purchasesRepository;
            _vehiclesRepository = vehiclesRepository;
        }

        [Route("api/Reports/Sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Sales(string user,DateTime? fromDate, DateTime? toDate )
        {

            try
            {
                var parameters = new SalesReportSearchParameters()
                {
                    UserName = user,
                    FromDate = fromDate,
                    ToDate = toDate
            };


                var result = _purchasesRepository.GetSales(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Reports/Inventory/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Inventory(int id)
        {

            try
            { 
                var result = _vehiclesRepository.GetInventory(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
