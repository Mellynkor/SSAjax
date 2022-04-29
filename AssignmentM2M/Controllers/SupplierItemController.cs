using AssignmentM2M.Models.Entities;
using AssignmentM2M.Models.Entities.ViewModels;
using AssignmentM2M.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentM2M.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SupplierItemController : Controller
    {
        /// <summary>
        /// The supplier item repo
        /// </summary>
        private ISupplierItemRepository _supplierItemRepo;
        /// <summary>
        /// The item repo
        /// </summary>
        private IItemRepository _itemRepo;
        /// <summary>
        /// The supplier repo
        /// </summary>
        private ISupplierRepository _supplierRepo;


        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierItemController"/> class.
        /// </summary>
        /// <param name="supplierItemRepo">The supplier item repo.</param>
        /// <param name="supplierRepo">The supplier repo.</param>
        /// <param name="itemRepo">The item repo.</param>
        public SupplierItemController(
            ISupplierItemRepository supplierItemRepo,
            ISupplierRepository supplierRepo, 
            IItemRepository itemRepo)
        {
            _supplierItemRepo = supplierItemRepo;
            _supplierRepo = supplierRepo;
            _itemRepo = itemRepo;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_supplierItemRepo.ReadAll());
        }

        /// <summary>
        /// Filter Stores to just those that carry Dell products
        /// </summary>
        /// <returns>
        /// All Stores with Dell products
        /// </returns>
        public IActionResult DellFilter()
        {
            ViewData["Message"] = "All Dell products";
            var supplieritems = _supplierItemRepo.ReadAll();
            var model = from s in supplieritems
                        where s.SupplierId == "001"
                        select s;
            return View(model);
        }

        /// <summary>
        /// Creates the specified supplier code.
        /// </summary>
        /// <param name="SupplierCode">The supplier code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public IActionResult Create([Bind(Prefix = "id")] string SupplierCode, int itemId)
        {
            var supplier = _supplierRepo.Read(SupplierCode); //Read in the store
            if (supplier == null)
            {
                return RedirectToAction("Index", "Supplier");
            }
            var item = _itemRepo.Read(itemId); //Read in the item
            if (item == null)
            {
                return RedirectToAction("Details", "Supplier", new { id = SupplierCode });
            }
            
            var supplierItem = supplier.SupplierItems //Read in the storeitem
                .SingleOrDefault(si => si.ItemId == itemId);
            if (supplierItem != null)
            {
                return RedirectToAction("Details", "Supplier", new { id = SupplierCode });
            }
            
            var supplierItemVM = new SupplierItemVM
            {
                Supplier = supplier,
                Item = item
            };

            return View(supplierItemVM);
        }


        /// <summary>
        /// Creates the confirmed.
        /// </summary>
        /// <param name="SupplierCode">The supplier code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public IActionResult CreateConfirmed(string SupplierCode, int itemId)
        {
            _supplierItemRepo.Create(SupplierCode, itemId);
            return RedirectToAction("Details", "Supplier", new { id = SupplierCode });
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var supplieritem = _supplierItemRepo.Read(id);
            if (supplieritem == null)
            {
                return RedirectToAction("Index");
            }
            return View(supplieritem);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var storeitem = _supplierItemRepo.Read(id);
            if (storeitem == null)
            {
                return RedirectToAction("Index");
            }
            return View(storeitem);
        }
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(int id, double price)
        {
            if (ModelState.IsValid)
            {
                _supplierItemRepo.Update(id, price);
                return RedirectToAction("Index", "Supplier");
            }
            var supplieritem = _supplierItemRepo.Read(id);
            return View(supplieritem);
        }

        /// <summary>
        /// Removes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Remove(int id)
        {
            var supplieritem = _supplierItemRepo.Read(id);
            if (supplieritem == null)
            {
                return RedirectToAction("Index");
            }
            return View(supplieritem);
        }
        /// <summary>
        /// Removes the confirmed.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Remove")]
        public IActionResult RemoveConfirmed(string code, int id)
        {
            _supplierItemRepo.Delete(code,id);
            return RedirectToAction("Index", "Supplier");
        }

    }
}
