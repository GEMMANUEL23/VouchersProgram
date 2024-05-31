namespace VouchersProgram.Data.Models
{
    public class Teams
    {
        public int TeamID { get; set; }
        public int DepTeamID { get; set; }
        public string TeamName { get; set; }
        public bool TeamIsArchived { get; set; }
    }
}
