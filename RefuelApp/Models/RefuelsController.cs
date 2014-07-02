using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;

namespace RefuelApp.Models
{
    [Authorize]
    public class RefuelsController : ApiController
    {
        private FuelCostEntities db = new FuelCostEntities();

        // GET: api/Refuels
        public IQueryable<Refuels> GetRefuels()
        {
            return db.Refuels;
        }

        // GET: api/Refuels/5
        [ResponseType(typeof(Refuels))]
        public async Task<IHttpActionResult> GetRefuels(int id)
        {
            Refuels refuels = await db.Refuels.FindAsync(id);
            if (refuels == null)
            {
                return NotFound();
            }

            return Ok(refuels);
        }

        // PUT: api/Refuels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRefuels(int id, RefuelViewModel refuelsvm)
        {
            Refuels refuels = new Refuels();
            refuels.DateOf = refuelsvm.DateOf;
            refuels.TotalDistanceTraveled = refuelsvm.TotalDistanceTraveled;
            refuels.TotalFuelCostInEuros = refuelsvm.TotalFuelCostInEuros;
            refuels.UserFK = User.Identity.GetUserId();
            refuels.InsertDate = DateTime.Now;
            refuels.ModifiedDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != refuels.Id)
            {
                return BadRequest();
            }

            db.Entry(refuels).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefuelsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Refuels
        [ResponseType(typeof(Refuels))]
        public async Task<IHttpActionResult> PostRefuels(RefuelViewModel refuelsvm)
        {
            Refuels refuels = new Refuels();
            refuels.DateOf = refuelsvm.DateOf;
            refuels.TotalDistanceTraveled = refuelsvm.TotalDistanceTraveled;
            refuels.TotalFuelCostInEuros = refuelsvm.TotalFuelCostInEuros;
            refuels.UserFK = User.Identity.GetUserId();
            refuels.InsertDate = DateTime.Now;
            refuels.ModifiedDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Refuels.Add(refuels);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = refuels.Id }, refuels);
        }

        // DELETE: api/Refuels/5
        [ResponseType(typeof(Refuels))]
        public async Task<IHttpActionResult> DeleteRefuels(int id)
        {
            Refuels refuels = await db.Refuels.FindAsync(id);
            if (refuels == null)
            {
                return NotFound();
            }

            db.Refuels.Remove(refuels);
            await db.SaveChangesAsync();

            return Ok(refuels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RefuelsExists(int id)
        {
            return db.Refuels.Count(e => e.Id == id) > 0;
        }
    }
}