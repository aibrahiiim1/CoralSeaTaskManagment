namespace CoralSeaTaskManagment.Model.Models.Domain
{
    //[Table("Assignssss")] // to change table name
    //[Table("Assignssss",Schema ="conf")] // to change table name or to change schema instate of (dbo.)
    public class Assign
    {
        
        public int Id { get; set; }
        //[Required] // to be column required by annotation method
        //[Column("NameAssign")] // to change column name in database
        //[Column(TypeName ="varchar(200)")]  //to change column Type in database
        //[MaxLength(100)]  //to change column Maxmum length
        //[Comment("this is comment for only referance")] //to set comment for only referance
        //[Key] // to make any name of the tables to be primary key
        // ما هي composit key and use it by anootation????????????????????

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // if you have table with not a lot of data like CATEGORY just 4 or 5 rows only so  
        //public byte Id { get; set; }                           //you lust use byte instate of INT to take less of the memory for best berformance and add identity annotation 

        public string Name { get; set; }
        public string NameAr { get; set; }
        public int DepartmentId { get; set; }
        public int HotelId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        // Navigation Properties
        public Hotel? Hotels { get; set; }
        public Department? Departments { get; set; }

    }
}
