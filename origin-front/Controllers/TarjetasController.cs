using Microsoft.AspNetCore.Mvc;
using origin_front.Aplication;
using origin_front.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace origin_front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly AppTarjetas _AppTarjetas;

        public TarjetasController(
            AppTarjetas appTarjetas
        ) {
           this._AppTarjetas = appTarjetas;
        }

        // GET api/<TarjetasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var card = this._AppTarjetas.searchCard(id);
                card.Pin = null;
                if(card.Lock.HasValue && card.Lock.Value)
                {
                    return ValidationProblem();
                }
                return Ok(card);
            }
            catch (NotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }            
        }

        // POST api/<TarjetasController>
        [HttpPost]
        public IActionResult Post([FromBody] Tarjeta value)
        {
            try
            {

                switch (this._AppTarjetas.validatePin(value.Id, value.Pin.Value))
                {
                    case AppTarjetas.CardStatus.Ok:
                        return Ok();
                        
                    case AppTarjetas.CardStatus.Blocked:
                        return ValidationProblem();
                        
                    case AppTarjetas.CardStatus.Unauthorized:
                        return Unauthorized();
                        
                    default:
                        return Problem();
                        
                }
            }
            catch (NotFoundException)
            {
                return NotFound(value);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }            
        }
    }
}
