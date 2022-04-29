using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentM2M.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SupplierController : Controller
    {
        /// <summary>
        /// The supplier repo
        /// </summary>
        private ISupplierRepository _supplierRepo;


        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="supplierRepo">The supplier repo.</param>
        public SupplierController(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }


        /// <summary>
        /// Order by Name function, which simply orders the suppliers by their name alphabetically
        /// </summary>
        /// <returns></returns>
        public IActionResult OrderByName()
        {
            IEnumerable<Supplier> orderingQuery =
                from suppliers in _supplierRepo.ReadAll()
                orderby suppliers.Name ascending
                select suppliers;
            return View(orderingQuery);

        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            string code = "001";
            var Supplier = _supplierRepo.Read(code);
            return View(_supplierRepo.ReadAll());
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified new supplier.
        /// </summary>
        /// <param name="newSupplier">The new supplier.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Supplier newSupplier)
        {
            if (ModelState.IsValid)
            {
                _supplierRepo.Create(newSupplier);
                return RedirectToAction("Index");
            }
            return View(newSupplier);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Details(string id)
        {
            var supplier = _supplierRepo.Read(id);
            if (supplier == null)
            {
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            var supplier = _supplierRepo.Read(id);
            if (supplier == null)
            {
                return RedirectToAction("Index");
            }
            return View(supplier);
        }
        /// <summary>
        /// Edits the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierRepo.Update(supplier.SupplierCode, supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Delete(string id)
        {
            var supplier = _supplierRepo.Read(id);
            if (supplier == null)
            {
                return RedirectToAction("Index");
            }
            return View(supplier);
        }
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The code.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _supplierRepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
