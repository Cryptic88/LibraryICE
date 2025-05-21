using Microsoft.AspNetCore.SignalR.Protocol;

namespace LibraryICE.Models
{
    public class Book
    {
       public int BookID { get; set; }
       public string Title { get; set; }
       public string Author { get; set; }
       public string ISBN { get; set; }
       public int TypeID { get; set; }
       public BookType? Type { get; set; }
    }
}
