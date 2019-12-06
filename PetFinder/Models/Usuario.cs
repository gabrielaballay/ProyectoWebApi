namespace PetFinder.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public int Estado { get; set; }
        public int ProvinciaId { get; set; }
    }
}
