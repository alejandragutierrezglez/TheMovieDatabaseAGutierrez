namespace TheMovieDatabaseAGutierrez.Models
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string media_type { get; set; }
        public int media_id { get; set; }
        public bool favorite { get; set; }
        public List<object> Movies { get; set; }
    }
}
