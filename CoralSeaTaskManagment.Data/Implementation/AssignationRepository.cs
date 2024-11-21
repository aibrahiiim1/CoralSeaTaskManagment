using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class AssignationRepository : GenericRepository<Assignation>, IAssignationRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Assignation assignation)
        {
            var assignationindb = _context.Assignations.FirstOrDefault(x => x.Id == assignation.Id);
            if (assignationindb != null) 
            {
                assignationindb.AssignId = assignation.AssignId;
                assignationindb.CreatedTime = DateTime.Now;
                assignationindb.OrderId = assignation.OrderId;
                assignationindb.HotelId = assignation.HotelId;
                assignationindb.ApplicationUserId = assignation.ApplicationUserId;
                assignationindb.TechName = assignation.TechName;
            }
    }
    }
}
