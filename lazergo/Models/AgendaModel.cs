//Aqui eu crio a Tabela AgendaModel do banco de dados. 

namespace lazergo.Models
{
    public class AgendaModel
    {
        public int Id { get; set; }
        public string Nometerreno { get; set; } = string.Empty;
        public string NomeImagem { get; set;} = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
    }
}
