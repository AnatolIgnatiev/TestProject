using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.Services;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateIncedent(Incedent incedentModel)
        {
            try
            {
                var model = _appService.SaveIncedent(incedentModel);
                if (model.IsSuccess == true)
                {
                    return Ok(model);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateAccount(Account accountModel)
        {
            try
            {
                var model = _appService.SaveAccount(accountModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateContact(Contact contactModel)
        {
            try
            {
                var model = _appService.SaveContact(contactModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
