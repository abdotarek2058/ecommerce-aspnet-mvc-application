using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProcedures = await _service.GetAllAsync();
            return View(allProcedures);
        }
        //get: Procedures/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var procedureDetails = await _service.GetByIdAsync(id);
            if (procedureDetails == null) return View("NotFound");
            return View(procedureDetails);
        }
        //get: Procedures/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction("Index");
        }
        //get: Procedures/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var procedureDetails = await _service.GetByIdAsync(id);
            if (procedureDetails == null) return View("NotFound");
            return View(procedureDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction("Index");
        }
        //get: Procedures/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var procedureDetails = await _service.GetByIdAsync(id);
            if (procedureDetails == null) return View("NotFound");
            return View(procedureDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedureDetails = await _service.GetByIdAsync(id);
            if (procedureDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
