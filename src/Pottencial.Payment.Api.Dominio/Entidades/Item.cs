namespace Pottencial.Payment.Api.Dominio.Entidades
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public Item(string nome)
        {
            Id = Guid.NewGuid();

            Nome = nome;
        }

        public Item(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
