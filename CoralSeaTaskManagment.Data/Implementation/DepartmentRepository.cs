
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Department department)
        {
            var departmentindb = _context.Departments.FirstOrDefault(x => x.Id == department.Id);
            if (departmentindb != null) 
            {   
                departmentindb.Name = department.Name;
                departmentindb.NameAr = department.NameAr;
                departmentindb.HotelId = department.HotelId;
                departmentindb.CreatedTime = DateTime.Now;

            }
        }
    }
}
