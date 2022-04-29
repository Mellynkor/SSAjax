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
    public class StoreItemController : Controller
    {
        /// <summary>
        /// The store item repo
        /// </summary>
        private IStoreItemRepository _storeItemRepo;
        /// <summary>
        /// The item repo
        /// </summary>
        private IItemRepository _itemRepo;
        /// <summary>
        /// The store repo
        /// </summary>
        private IStoreRepository _storeRepo;


        /// <summary>
        /// Initializes a new instance of the <see cref="StoreItemController"/> class.
        /// </summary>
        /// <param name="storeItemRepo">The store item repo.</param>
        /// <param name="storeRepo">The store repo.</param>
        /// <param name="itemRepo">The item repo.</param>
        public StoreItemController(
            IStoreItemRepository storeItemRepo, 
            IStoreRepository storeRepo, 
            IItemRepository itemRepo)
        {
            _storeItemRepo = storeItemRepo;
            _storeRepo = storeRepo;
            _itemRepo = itemRepo;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_storeItemRepo.ReadAll());
        }


        /// <summary>
        /// Filter Stores to just those that carry Dell products
        /// </summary>
        /// <returns>
        /// All Stores with Dell products
        /// </returns>
        public IActionResult WalmartFilter()
        {
            ViewData["Message"] = "All products carried by Walmart 001";
            var storeitems = _storeItemRepo.ReadAll();
            var model = from s in storeitems
                        where s.StoreId == "001"
                        select s;
            return View(model);
        }

        /// <summary>
        /// Finds the maximum priced items.
        /// </summary>
        /// <returns></returns>
        public IActionResult FindMaxPrice()
        {
            ViewData["Message"] = "Here are the store items ranked by their price";
            var storeitems = _storeItemRepo.ReadAll();
            var model = from i in storeitems
                        orderby i.Price descending
                        select i;
            return View(model);
        }


        /// <summary>
        /// Creates the specified store code.
        /// </summary>
        /// <param name="StoreCode">The store code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public IActionResult Create([Bind(Prefix = "id")] string StoreCode, int itemId)
        {
            var store = _storeRepo.Read(StoreCode); //Read in the store
            if (store == null)
            {
                return RedirectToAction("Index", "Store");
            }
            var item = _itemRepo.Read(itemId); //Read in the item
            if (item == null)
            {
                return RedirectToAction("Details", "Store", new { id = StoreCode });
            }
            
            var storeItem = store.StoreItems //Read in the storeitem
                .SingleOrDefault(si => si.ItemId == itemId);
            if (storeItem != null)
            {
                return RedirectToAction("Details", "Store", new { id = StoreCode });
            }
            
            var storeItemVM = new StoreItemVM
            {
                Store = store,
                Item = item
            };

            return View(storeItemVM);
        }


        /// <summary>
        /// Creates the confirmed.
        /// </summary>
        /// <param name="StoreCode">The store code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public IActionResult CreateConfirmed(string StoreCode, int itemId)
        {
            _storeItemRepo.Create(StoreCode, itemId);
            return RedirectToAction("Details", "Store", new { id = StoreCode });
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var storeitem = _storeItemRepo.Read(id);
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
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            var storeitem = _storeItemRepo.Read(id);
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
        /// <param name="storeitem">The storeitem.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Update")]
        public IActionResult Update(int id, double price)
        {
            if (ModelState.IsValid)
            {
                _storeItemRepo.Update(id, price);
                return RedirectToAction("Details", "Store");
            }
            return RedirectToAction("Details", "Store");
        }

        /// <summary>
        /// Removes the specified storecode.
        /// </summary>
        /// <param name="storecode">The storecode.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Remove(int id)
        {
            var storeitem = _storeItemRepo.Read(id);
            if (storeitem == null)
            {
                return RedirectToAction("Details", "Store");
            }
            return View(storeitem);
        }
        /// <summary>
        /// Removes the confirmed.
        /// </summary>
        /// <param name="storecode">The storecode.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public IActionResult RemoveConfirmed(string storecode, int id)
        {
            _storeItemRepo.Remove(storecode, id);
            return RedirectToAction("Details", "Store");
        }

    }
}
