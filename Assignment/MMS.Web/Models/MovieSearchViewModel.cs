
using MMS.Data.Entities;

namespace MMS.Web.Models;

public class MovieSearchViewModel
{
    // result set
    public IList<Movie> Movies { get; set;} = new List<Movie>();

    // search options        
    public string Query { get; set; } = string.Empty;

}