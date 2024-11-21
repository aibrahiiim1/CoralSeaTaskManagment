
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class AssignRepository : GenericRepository<Assign>, IAssignRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Assign assign)
        {
            var assignindb = _context.Assigns.FirstOrDefault(x => x.Id == assign.Id);
            if (assignindb != null) 
            {   
                assignindb.Name = assign.Name;
                assignindb.NameAr = assign.NameAr;
                assignindb.DepartmentId = assign.DepartmentId;
                assignindb.HotelId = assign.HotelId;
                assignindb.CreatedTime = DateTime.Now;

            }
        }
    }
}
