using System.ComponentModel.DataAnnotations;

namespace lazergo.Dto
{
    public class ClienteCriacaoDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public string Celular { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }
        public string TipoPessoa { get; set; }
        public string TipoUsuario { get; set; }
    }
}
