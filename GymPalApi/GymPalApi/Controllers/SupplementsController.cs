namespace GymPalApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GymPalApi.Data;
    using GymPalApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/supplements")]
    [ApiController]
    public class SupplementsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SupplementsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplement>>> GetSupplements()
        {

            var suppliments = await this.context.Supplements.ToListAsync();

            if (suppliments == null)
            {
                return this.NotFound(); 
            }

            return this.Ok(suppliments);
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost, Route(nameof(CreateSupplement))]
        public async Task<IActionResult> CreateSupplement([FromForm] CreateSupplementInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supplement = new Supplement
                {
                    SupplementName = model.SupplementName,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl,
                };

                await this.context.AddAsync(supplement);
                await this.context.SaveChangesAsync(); 
                return this.Ok(supplement);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
