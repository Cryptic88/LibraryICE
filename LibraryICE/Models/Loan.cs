namespace LibraryICE.Models
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int BookID { get; set; }
        public DateTime LoanDate { get; set; }
        public Book Book { get; set; }
    }
}
