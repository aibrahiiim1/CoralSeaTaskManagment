using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class GorderRepository : GenericRepository<Gorder>, IGorderRepository
    {
        private readonly ApplicationDbContext _context;
        public GorderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Gorder gorder)
        {
            var gorderindb = _context.Gorders.FirstOrDefault(x => x.Id == gorder.Id);
            if (gorderindb != null) 
            {   
                gorderindb.CreatedTime = gorder.CreatedTime;
                gorderindb.ApplicationUserId = gorder.ApplicationUserId;
                gorderindb.Comment= gorder.Comment;
                gorderindb.GdepartmentFId= gorder.GdepartmentFId;
                gorderindb.HotelId= gorder.HotelId;
                gorderindb.GitemId= gorder.GitemId;
                gorderindb.DepartmentId= gorder.DepartmentId;
                gorderindb.GlocationId= gorder.GlocationId;
                gorderindb.OstatusId= gorder.OstatusId;
                gorderindb.OtypeId= gorder.OtypeId;
                gorderindb.GroomsId= gorder.GroomsId;
                gorderindb.Comment= gorder.Comment;
            }
        }
    }
}
