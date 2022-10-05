using System;
using System.ComponentModel.DataAnnotations;

namespace BookEntityDemo.Data.Models
{
    public class Books
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}
