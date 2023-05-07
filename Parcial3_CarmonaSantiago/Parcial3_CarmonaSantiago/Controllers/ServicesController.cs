using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3_CarmonaSantiago.DAL;
using Parcial3_CarmonaSantiago.DAL.Entities;
using Parcial3_CarmonaSantiago.Helpers;
using Parcial3_CarmonaSantiago.Models;
using System.Diagnostics.Metrics;

namespace Parcial3_CarmonaSantiago.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DatabaseContext _context;
        private readonly IDropDownListsHelper _dropDownListsHelper;


        public ServicesController(DatabaseContext context, IDropDownListsHelper dropDownListsHelper, IUserHelper userHelper)
        {
            _context = context;
            _dropDownListsHelper = dropDownListsHelper;
            _userHelper = userHelper;
        }

        private async Task<Service> GetServiceById(Guid? serviceId)
        {
            Service service = await _context.Services
                .FirstOrDefaultAsync(service => service.Id == serviceId);
            return service;
        }
        private async Task<Vehicle> GetVehicleById(Guid? vehicleId)
        {
            Vehicle vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(vehicle => vehicle.Id == vehicleId);
            return vehicle;
        }
        private async Task<User> GetUserByName(string userName)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(users => users.Email == userName);
            return user;
        }

        private async Task<VehicleDetail> GetVehicleDetailById(Guid? vehicleDetailId)
        {
            VehicleDetail vehicleDetail = await _context.VehicleDetails.Include(vehicleDetail => vehicleDetail.Vehicle).ThenInclude(vehicle => vehicle.Service).FirstOrDefaultAsync(vehicleDetail => vehicleDetail.Id == vehicleDetailId);
            return vehicleDetail;
        }

        // GET: Services
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
            return _context.Services != null ?
                        View(await _context.Services.ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.Services'  is null.");
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddVehicleService()
        {
            AddVehicleServiceViewModel addVehicleServiceViewModel = new()
            {
                Id = Guid.Empty,
                Services = await _dropDownListsHelper.GetDDLServices(),
            };

            return View(addVehicleServiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddVehicleService(AddVehicleServiceViewModel addVehicleServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle vehicle = new Vehicle()
                    {
                        NumberPlate = addVehicleServiceViewModel.NumberPlate,
                        Owner = addVehicleServiceViewModel.Owner,
                        Service = await GetServiceById(addVehicleServiceViewModel.ServiceId)
                    };

                    _context.Add(vehicle);

                    VehicleDetail vehicleDetail = new VehicleDetail()
                    {
                        CreationDate = DateTime.Now,
                        DeliveryDate = null,
                        Vehicle = vehicle,
                        User = await GetUserByName(User.Identity.Name)
                    };

                    _context.Add(vehicleDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(addVehicleServiceViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ProcessService()
        {
            return View(await _context.VehicleDetails.Include(vehicleDetails => vehicleDetails.Vehicle).ThenInclude(vehicle => vehicle.Service).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> EditProcessService(Guid? vehicleDetailId)
        {
            Console.WriteLine(vehicleDetailId);
            if (vehicleDetailId == null || _context.VehicleDetails == null)
            {
                return NotFound();
            }

            var vehicleDetail = await GetVehicleDetailById(vehicleDetailId);
            if (vehicleDetail == null)
            {
                return NotFound();
            }
            return View(vehicleDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProcessService(Guid vehicleDetailId, VehicleDetail vehicleDetail)
        {
            if (vehicleDetailId != vehicleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                _context.Update(vehicleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProcessService));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(vehicleDetail);
        }

        [HttpGet]
        public async Task<IActionResult> ServiceState()
        {
            return View(await _context.VehicleDetails.Include(vehicleDetails => vehicleDetails.Vehicle).ThenInclude(vehicle => vehicle.Service).ToListAsync());
        }
    }
}
