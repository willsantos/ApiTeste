var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var alunos = new List<Aluno>
{
    new Aluno { Nome = "Ana", Idade = 20, Curso = "Engenharia", Turma = "A" },
    new Aluno { Nome = "Bruno", Idade = 21, Curso = "Matem�tica", Turma = "B" },
    new Aluno { Nome = "Carla", Idade = 19, Curso = "Biologia", Turma = "C" }
};

// Criar uma rota GET que retorna uma lista de alunos
app.MapGet("/alunos", () => alunos);

// Criar uma rota POST que adiciona um aluno na lista e retorna o JSON com os dados adicionados
app.MapPost("/alunos", async (HttpContext context) =>
{
    var random = new Random();
    if (random.NextDouble() < 0.5)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Erro interno no servidor.");
        return;
    }

    var novoAluno = await context.Request.ReadFromJsonAsync<Aluno>();
    alunos.Add(novoAluno);

    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsJsonAsync(novoAluno);
});

//TODO: rotas UPDATE E DELETE

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public class Aluno
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Curso { get; set; }
    public string Turma { get; set; }
}