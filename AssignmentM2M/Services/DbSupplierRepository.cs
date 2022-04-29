using AssignmentM2M.Models;
using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;

namespace AssignmentM2M.Services
{
    /// <summary>
    /// Supplier Repository that stores operation methods for items
    /// </summary>
    /// <seealso cref="AssignmentM2M.Services.ISupplierRepository" />
    public class DbSupplierRepository : ISupplierRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSupplierRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public DbSupplierRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the specified new supplier.
        /// </summary>
        /// <param name="newSupplier">The new supplier.</param>
        /// <returns></returns>
        public Supplier Create(Supplier newSupplier)
        {
            _db.Suppliers.Add(newSupplier);
            _db.SaveChanges();
            return newSupplier;
        }


        /// <summary>
        /// Read an Supplier entity based on ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Supplier? Read(string code)
        {
            return _db.Suppliers
                .Include(s => s.SupplierItems)
                    .ThenInclude(si => si.Item)
                .FirstOrDefault(s => s.SupplierCode == code);
        }

        /// <summary>
        /// Read ALL Supplier entities
        /// </summary>
        /// <returns></returns>
        public ICollection<Supplier> ReadAll()
        {
            return _db.Suppliers
                .Include(s => s.SupplierItems)
                .ToList();
        }

        /// <summary>
        /// Updates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="supplier">The supplier.</param>
        public void Update(string code, Supplier supplier)
        {
            Supplier? supplierToUpdate = Read(code);
            if (supplierToUpdate != null)
            {
                supplierToUpdate.Name = supplier.Name;
                supplierToUpdate.Description = supplier.Description;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void Delete(string code)
        {
            Supplier? supplierToDelete = Read(code);
            if (supplierToDelete != null)
            {
                _db.Suppliers.Remove(supplierToDelete);
                _db.SaveChanges();
            }

        }

    }
}
