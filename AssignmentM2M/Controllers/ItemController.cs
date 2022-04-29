using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentM2M.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ItemController : Controller
    {
        /// <summary>
        /// The item repo
        /// </summary>
        private IItemRepository _itemRepo;
        /// <summary>
        /// The store repo
        /// </summary>
        private IStoreRepository _storeRepo;
        /// <summary>
        /// The supplier repo
        /// </summary>
        private ISupplierRepository _supplierRepo;



        /// <summary>
        /// Initializes a new instance of the <see cref="ItemController"/> class.
        /// </summary>
        /// <param name="itemRepo">The item repo.</param>
        /// <param name="storeRepo">The store repo.</param>
        /// <param name="supplierRepo">The supplier repo.</param>
        public ItemController(IItemRepository itemRepo, IStoreRepository storeRepo, ISupplierRepository supplierRepo)
        {
            _itemRepo = itemRepo;
            _storeRepo = storeRepo;
            _supplierRepo = supplierRepo;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_itemRepo.ReadAll());
        }

        /// <summary>
        /// Registers the specified storecode.
        /// </summary>
        /// <param name="storecode">The storecode.</param>
        /// <returns></returns>
        public IActionResult Register([Bind(Prefix = "id")] string storecode)
        {
                var store = _storeRepo.Read(storecode); //Read in the store entity
                if (store == null)
                {
                    return RedirectToAction("Index", "Store");
                }
                var allItems = _itemRepo.ReadAll(); //Read in all items
                var itemsRegistered = store.StoreItems //Get the items already registered to the store
                    .Select(si => si.Item).ToList();
                var itemsNotRegistered = allItems.Except(itemsRegistered); //Get the items not registered to the store
                ViewData["Store"] = store;
                return View(itemsNotRegistered);
            
        }


        /// <summary>
        /// Suppliers the register.
        /// </summary>
        /// <param name="suppliercode">The suppliercode.</param>
        /// <returns></returns>
        public IActionResult SupplierRegister([Bind(Prefix = "id")] string suppliercode)
        {
            var supplier = _supplierRepo.Read(suppliercode); //Read in the store entity
            if (supplier == null)
            {
                return RedirectToAction("Index", "Store");
            }
            var allItems = _itemRepo.ReadAll(); //Read in all items
            var itemsRegistered = supplier.SupplierItems //Get the items already registered to the store
                .Select(si => si.Item).ToList();
            var itemsNotRegistered = allItems.Except(itemsRegistered); //Get the items not registered to the store
            ViewData["Supplier"] = supplier;
            return View(itemsNotRegistered);

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
        /// Creates the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Item newItem)
        {
            if (ModelState.IsValid)
            {
                _itemRepo.Create(newItem);
                return RedirectToAction("Index");
            }
            return View(newItem);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var item = _itemRepo.Read(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var item = _itemRepo.Read(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }
        /// <summary>
        /// Edits the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _itemRepo.Update(item.Id, item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            var item = _itemRepo.Read(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _itemRepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
