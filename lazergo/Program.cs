using lazergo.Conexao;
using lazergo.Services.Agenda;
using lazergo.Services.Cliente;
using lazergo.Services.Sessaologado;
using lazergo.Services.Usuario;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Adicione esta linha para configurar a sessão
builder.Services.AddHttpContextAccessor();

// Conexão com o banco de dados
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseMySql("Server=127.0.0.1;Database=agenda;User=root;Password=;", new MySqlServerVersion(new Version(8, 2, 12))));

// Criar métodos para o AgendaService conversar com o IAgendaInterface
builder.Services.AddScoped<IAgendaInterface, AgendaService>();
builder.Services.AddScoped<IClienteInterface, ClienteService>(); // Certifique-se de que ClienteService está implementado
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é de 30 dias. Você pode querer alterar isso para cenários de produção, consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
