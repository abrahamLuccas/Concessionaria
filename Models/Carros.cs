namespace Concessionaria.Models
{
    public class Carros
    {
        public Guid Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Km { get; set; }
        public string Combustivel { get; set; }
        public string Valor { get; set; }
        public byte[]? Foto { get; set; }
    }
}
