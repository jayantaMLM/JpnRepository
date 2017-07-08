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
using BtcProApp.Models;

namespace BtcProApp.Controllers
{
    public class PayoutProcessesController : ApiController
    {
        private BtcProDB db = new BtcProDB();

        // GET: api/PayoutProcesses
        public IQueryable<PayoutProcess> GetPayoutProcesses()
        {
            return db.PayoutProcesses;
        }

        // GET: api/PayoutProcesses/5
        [ResponseType(typeof(PayoutProcess))]
        public async Task<IHttpActionResult> GetPayoutProcess(int id)
        {
            PayoutProcess payoutProcess = await db.PayoutProcesses.FindAsync(id);
            if (payoutProcess == null)
            {
                return NotFound();
            }

            return Ok(payoutProcess);
        }

        // PUT: api/PayoutProcesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPayoutProcess(int id, PayoutProcess payoutProcess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payoutProcess.Id)
            {
                return BadRequest();
            }

            db.Entry(payoutProcess).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayoutProcessExists(id))
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

        // POST: api/PayoutProcesses
        [ResponseType(typeof(PayoutProcess))]
        public async Task<IHttpActionResult> PostPayoutProcess(PayoutProcess payoutProcess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PayoutProcesses.Add(payoutProcess);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = payoutProcess.Id }, payoutProcess);
        }

        // DELETE: api/PayoutProcesses/5
        [ResponseType(typeof(PayoutProcess))]
        public async Task<IHttpActionResult> DeletePayoutProcess(int id)
        {
            PayoutProcess payoutProcess = await db.PayoutProcesses.FindAsync(id);
            if (payoutProcess == null)
            {
                return NotFound();
            }

            db.PayoutProcesses.Remove(payoutProcess);
            await db.SaveChangesAsync();

            return Ok(payoutProcess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayoutProcessExists(int id)
        {
            return db.PayoutProcesses.Count(e => e.Id == id) > 0;
        }
    }
}