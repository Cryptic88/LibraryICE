using System.ComponentModel.DataAnnotations;

namespace LibraryICE.Models
{
    public class BookType
    {
        [Key]
        public int TypeID { get; set; }
        public string Type{ get; set; }
    }
}
