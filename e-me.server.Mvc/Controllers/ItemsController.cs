using System.Linq;
using e_me.Model.Models;
using e_me.server.Mvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_me.server.Mvc.Repositories.Interfaces;

namespace e_me.server.Mvc.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Items.FindAll());
        }

        // GET: Items/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _unitOfWork.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Items.Add(item);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _unitOfWork.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,Title,Description")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var current = _unitOfWork.Items.Find(item.Id);
                    current.Description = item.Description;
                    current.Title = item.Title;
                    _unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _unitOfWork.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var item = _unitOfWork.Items.Find(id);
            _unitOfWork.Items.Remove(item);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _unitOfWork.Items.FindAll().Any(e => e.Id == id);
        }
    }
}
