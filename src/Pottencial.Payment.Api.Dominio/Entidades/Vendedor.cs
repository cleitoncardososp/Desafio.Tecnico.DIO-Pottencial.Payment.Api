namespace Pottencial.Payment.Api.Dominio.Entidades
{
    public class Vendedor
    {
        public Guid Id { get; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Vendedor(string cpf, string nome, string email, string telefone)
        {
            Id = Guid.NewGuid();

            Cpf = cpf;
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

        public Vendedor(Guid id, string cpf, string nome, string email, string telefone)
        {
            Id = id;
            Cpf = cpf;
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

    }
}
