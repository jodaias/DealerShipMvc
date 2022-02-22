using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealerShipMvc.Data;
using DealerShipMvc.Models;
using DealerShipMvc.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DealerShipMvc.Extensions;
using KissLog;

namespace DealerShipMvc.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IKLogger _kLogger;

        public CarController(ICarRepository carRepository, IKLogger kLogger)
        {
            _carRepository = carRepository;
            _kLogger = kLogger;
        }

        // GET: Car
        [ClaimsAuthorize("Cars", "CanReady")]
        public async Task<IActionResult> Index()
        {
            _kLogger.Trace("Passei por aqui");

            var cars = await _carRepository.GetAll().ToListAsync();

            return View(cars);
        }

        // GET: Car/Details/5
        [ClaimsAuthorize("Cars", "CanReady")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ClaimsAuthorize("Cars", "CanCreate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Price,Color,Year")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();

                await _carRepository.AddAsync(car);

                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Edit/5
        [ClaimsAuthorize("Cars", "CanEdit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Cars", "CanEdit")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Model,Price,Color,Year")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _carRepository.UpdateAsync(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Delete/5
        [ClaimsAuthorize("Cars", "CanDelete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [ClaimsAuthorize("Cars", "CanDelete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            
            await _carRepository.RemoveAsync(car);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(Guid id)
        {
            var car = await _carRepository.GetByIdAsync(id);

            return car != null;
        }
    }
}
