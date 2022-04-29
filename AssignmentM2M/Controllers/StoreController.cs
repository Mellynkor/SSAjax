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
    public class StoreController : Controller
    {
        /// <summary>
        /// The store repo
        /// </summary>
        private IStoreRepository _storeRepo;


        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="storeRepo">The store repo.</param>
        public StoreController(IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }


        /// <summary>
        /// Filter Stores and rank them by the amount of items they carry
        /// </summary>
        /// <returns>
        /// All Stores ranked by their item count
        /// </returns>
        public IActionResult ItemCountFilter()
        {
            ViewData["Message"] = "All Stores by item count";
            var stores = _storeRepo.ReadAll();
            var model = from s in stores
                        orderby s.StoreItems.Count() descending
                        select s;
            return View(model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var store = _storeRepo.Read("001");
            Debug.WriteLine(store.StoreItems.Count() + "Here");
            return View(_storeRepo.ReadAll());
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
        /// Creates the specified new store.
        /// </summary>
        /// <param name="newStore">The new store.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Store newStore)
        {
            if (ModelState.IsValid)
            {
                _storeRepo.Create(newStore);
                return RedirectToAction("Index");
            }
            return View(newStore);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Details(string id)
        {
            var store = _storeRepo.Read(id);
            if (store == null)
            {
                return RedirectToAction("Index");
            }
            return View(store);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            var store = _storeRepo.Read(id);
            if (store == null)
            {
                return RedirectToAction("Index");
            }
            return View(store);
        }
        /// <summary>
        /// Edits the specified store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                _storeRepo.Update(store.StoreCode, store);
                return RedirectToAction("Index");
            }
            return View(store);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Delete(string id)
        {
            var store = _storeRepo.Read(id);
            if (store == null)
            {
                return RedirectToAction("Index");
            }
            return View(store);
        }
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The code.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _storeRepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
