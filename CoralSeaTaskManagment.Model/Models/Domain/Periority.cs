namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Periority
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
