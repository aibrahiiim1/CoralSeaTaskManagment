namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class RefreshAccessToken
    {
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Expires { get; set; }
        public bool Enabled { get; set; }
        //public User User { get; set; }
        //public string UserEmail { get; set; }
    }
}
