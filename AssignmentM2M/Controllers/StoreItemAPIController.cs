using AssignmentM2M.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentM2M.Controllers;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class StoreItemAPIController : ControllerBase
{
    /// <summary>
    /// The store repo
    /// </summary>
    private readonly IStoreRepository _storeRepo;
    /// <summary>
    /// The item repo
    /// </summary>
    private readonly IItemRepository _itemRepo;
    /// <summary>
    /// The store item repo
    /// </summary>
    private readonly IStoreItemRepository _storeItemRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreItemAPIController"/> class.
    /// </summary>
    /// <param name="storeRepo">The store repo.</param>
    /// <param name="itemRepo">The item repo.</param>
    /// <param name="storeItemRepo">The store item repo.</param>
    public StoreItemAPIController(
        IStoreRepository storeRepo,
        IItemRepository itemRepo,
        IStoreItemRepository storeItemRepo)
    {
        _storeRepo = storeRepo;
        _itemRepo = itemRepo;
        _storeItemRepo = storeItemRepo;
    }

    /// <summary>
    /// Posts the specified store code.
    /// </summary>
    /// <param name="StoreCode">The store code.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <returns></returns>
    [HttpPost("create")]
    public IActionResult Post([FromForm] string StoreCode, [FromForm]int itemId)
    {
        var storeItem = _storeItemRepo.Create(StoreCode, itemId);
        // Remove the circular reference for the JSON
        storeItem?.Store?.StoreItems.Clear();
        Debug.WriteLine("\n\n\n\nUsing API to Create store item\n\n\n\n");
        return CreatedAtAction("Get", 
            new { id = storeItem?.Id }, storeItem);
    }

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns></returns>
    [HttpGet("storeitems")]
    public IActionResult Get()
    {
        var stores = _storeRepo.ReadAll();
        var storeItems =
           _storeItemRepo.ReadAll();
        var model = from s in stores
                    join si in storeItems
                        on s.StoreCode equals si.StoreId
                    orderby s.Name
                    select new
                    {
                        StoreName = s.Name,
                        StoreFullCode = si.Store!.StoreCode,
                        si.Name
                    };
        Debug.WriteLine("\n\n\n\nUsing API to return all the store items\n\n\n\n");
        return Ok(model);
    }


    [HttpGet("findmaxprice")]
    public IActionResult GetMax()
    {
        var stores = _storeRepo.ReadAll();
        var storeItems =
           _storeItemRepo.ReadAll();
        var model = from s in stores
                    join si in storeItems
                        on s.StoreCode equals si.StoreId
                    orderby si.Price descending
                    select new
                    {
                        StoreFullCode = si.Store!.StoreCode,
                        StoreName = s.Name,
                        ItemName = si.Name,
                        si.Price
                    };
        Debug.WriteLine("\n\n\n\nUsing API to return all the store items by max price\n\n\n\n");
        return Ok(model);
    }



    [HttpGet("walmartfilter")]
    public IActionResult GetWalmart()
    {
        var stores = _storeRepo.ReadAll();
        var storeItems =
           _storeItemRepo.ReadAll();
        var model = from s in stores
                    join si in storeItems
                        on s.StoreCode equals si.StoreId
                    where s.Name.Equals("Walmart")
                    orderby si.StoreId descending
                    select new
                    {
                        StoreFullCode = si.Store!.StoreCode,
                        StoreName = s.Name,
                        ItemName = si.Name,
                        si.Price
                    };
        Debug.WriteLine("\n\n\n\nUsing API to return all the store items walmart carries\n\n\n\n");
        return Ok(model);
    }



    /// <summary>
    /// Deletes the specified store code.
    /// </summary>
    /// <param name="StoreCode">The store code.</param>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpPost("remove")]
    public IActionResult Remove(
        [FromForm] string StoreCode, [FromForm] int id)
    {
        Debug.WriteLine("\n\n\n\nUsing API to Remove store item\n\n\n\n");
        _storeItemRepo.Remove(StoreCode, id);
        return NoContent(); // 204 as per HTTP specification
    }


    /// <summary>
    /// Edits the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="price">The price.</param>
    /// <param name="storeitem">The storeitem.</param>
    /// <returns></returns>
    [HttpPut("update")]
    public IActionResult Update([FromForm] int id, [FromForm] double price)
    {
            _storeItemRepo.Update(id, price);
        Debug.WriteLine("\n\n\n\nUsing API to edit store item\n\n\n\n");
        return NoContent();
    }





}
