//Aqui é criado os serviços, todos os meios desenvolvidos na interface 
using Microsoft.EntityFrameworkCore;
using lazergo.Conexao;
using lazergo.Dto;
using lazergo.Models;

namespace lazergo.Services.Agenda
{
    public class AgendaService : IAgendaInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;

        public AgendaService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }

        public string GerarCaminhoArquivo(List<IFormFile> fotos)
        {
            var hashList = new List<string>();

            foreach (var foto in fotos)
            {
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoImagem = $"{Path.GetFileNameWithoutExtension(foto.FileName).Replace(" ", "").ToLower()}_{codigoUnico}{Path.GetExtension(foto.FileName)}";

                var caminhoParaSalvarImagem = Path.Combine(_sistema, "imagens");

                if (!Directory.Exists(caminhoParaSalvarImagem))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagem);
                }

                var caminhoCompleto = Path.Combine(caminhoParaSalvarImagem, nomeCaminhoImagem);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    foto.CopyTo(stream);
                }

                hashList.Add(nomeCaminhoImagem);
            }

            return string.Join(",", hashList);
        }
        public async Task<AgendaModel> CriarAgenda(AgendaCriacaoDto agendaCriacaoDto, List<IFormFile> fotos)

        {
            try
            {
                var nomeCaminhosImagens = GerarCaminhoArquivo(fotos);
                var agenda = new AgendaModel
                {
                    Nometerreno = agendaCriacaoDto.Nometerreno,
                    Descricao = agendaCriacaoDto.Descricao,
                    Valor = agendaCriacaoDto.Valor,
                    NomeImagem = nomeCaminhosImagens
                };

                _context.Add(agenda);
                await _context.SaveChangesAsync();

                return agenda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgendaModel>> GetAgendas()
        {
            try
            {
               return await _context.agendaModels.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgendaModel> GetAgendaPorId(int id)
        {
            try
            {
                return await _context.agendaModels.FirstOrDefaultAsync(agenda => agenda.Id == id);

            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message); 
            }
        }

        public async Task<AgendaModel> EditarAgenda(AgendaModel agendaModel, List<IFormFile>? fotos)
        {
            try
            {
                var agendaBanco = await _context.agendaModels.AsNoTracking().FirstOrDefaultAsync(agendaBD => agendaBD.Id == agendaModel.Id);

                var nomeCaminhoImagem = "";

                if (fotos != null && fotos.Count > 0)
                {
                    // Apagar as imagens existentes se houver novas imagens
                    var imagensExistentes = agendaBanco.NomeImagem.Split(',');
                    foreach (var imagem in imagensExistentes)
                    {
                        string caminhoImagemExistente = Path.Combine(_sistema, "imagens", imagem);
                        if (File.Exists(caminhoImagemExistente))
                        {
                            File.Delete(caminhoImagemExistente);
                        }
                    }

                    // Gerar novos caminhos de imagem
                    nomeCaminhoImagem = GerarCaminhoArquivo(fotos);
                }

                // Atualizar os campos
                agendaBanco.Nometerreno = agendaModel.Nometerreno;
                agendaBanco.Descricao = agendaModel.Descricao;
                agendaBanco.Valor = agendaModel.Valor;

                if (!string.IsNullOrEmpty(nomeCaminhoImagem))
                {
                    agendaBanco.NomeImagem = nomeCaminhoImagem;
                }

                _context.Update(agendaBanco);
                await _context.SaveChangesAsync();

                return agendaModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<AgendaModel> DeletarAgenda(int id)
        {
            try
            {
                var agenda = await _context.agendaModels.FirstOrDefaultAsync(agendabanco => agendabanco.Id == id);
                _context.Remove(agenda);
                await _context.SaveChangesAsync();
                return agenda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgendaModel>> GetAgendasFiltro(string? pesquisar)
        {
            try
            {
                var agendaModels = await _context.agendaModels.Where(agendaBanco => agendaBanco.Nometerreno.Contains(pesquisar)).ToListAsync();
                return agendaModels;
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }
    }
}
