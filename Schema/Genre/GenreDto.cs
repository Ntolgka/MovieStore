using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Schema.Genre;

public class GenreDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GenreId { get; set; }
    public string Name { get; set; }
}