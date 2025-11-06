# Formulário de Cadastro de Pessoa - Zuri

Aplicação React/Next.js para cadastro de pessoas com integração à API .NET.

## 🚀 Como Executar

### Pré-requisitos
- Node.js 18 ou superior
- npm ou yarn

### Instalação

1. Instale as dependências:
\`\`\`bash
npm install
\`\`\`

2. Execute o projeto:
\`\`\`bash
npm run dev
\`\`\`

3. Abra o navegador em [http://localhost:3000](http://localhost:3000)

## ⚙️ Configuração da API

### Endpoint da API
O formulário está configurado para enviar dados para:
\`\`\`
https://localhost:7254/api/Person
\`\`\`

Para alterar o endpoint, edite o arquivo \`app/page.tsx\` na linha do fetch.

### Configuração CORS na API .NET

Adicione o seguinte código no \`Program.cs\` da sua API:

\`\`\`csharp
// Adicione antes de builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Adicione antes de app.UseAuthorization()
app.UseCors("AllowFrontend");
\`\`\`

## 📋 Funcionalidades

- Validação de campos obrigatórios
- Validação de formato de e-mail
- Seletor de data (calendário) para evitar erros de digitação
- Dropdown para estado civil
- Mensagens de erro em português
- Feedback visual de sucesso/erro
- Design responsivo

## 🎨 Campos do Formulário

1. **Nome** - Campo de texto obrigatório
2. **Data de Nascimento** - Seletor de data (formato DD/MM/AAAA)
3. **E-mail** - Campo com validação de formato
4. **Estado civil** - Dropdown com opções: Solteiro(a), Casado(a), Divorciado(a), Viúvo(a)

## 📦 Contrato da API

O formulário envia os dados no seguinte formato JSON:

\`\`\`json
{
  "name": "João Marcos Sakalauska",
  "birthDate": "1988-07-15T00:00:00Z",
  "email": "jsakalauska@example.com",
  "maritalStatus": "Casado(a)"
}
\`\`\`

## 🛠️ Tecnologias

- Next.js 16
- React 19
- TypeScript
- Tailwind CSS
- shadcn/ui
- date-fns
- Radix UI
