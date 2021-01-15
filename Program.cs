using System;

namespace Revisao
{
    class Program
    {
        static void Main(string[] args)
        {
            //criar array de struct aluno
            //declarar no momento de instanciar o meu array, tem que infomar a quantidade/tamanho do array -> pre-alocação de memória
            Aluno[] alunos = new Aluno[5];

            //automaticamente vai assumir essa variável como um inteiro
            int indiceAluno = 0;

            //retorna a string do método isolado -> opcaoUsuario
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                case "1":
                    Console.WriteLine("Informe o nome do aluno: ");
                    // instanciando um obj do tipo aluno
                    // aqui no aluno não precisava especificamente fazer a declaração do tipo de variável (Aluno), poderia simplesmente usar o var que serve para qualquer tipo de variável
                    Aluno aluno = new Aluno();
                    // pega do Console e coloca direto no objeto
                    // poderia ter variavel, mas teria alocacao para depois transferir, corta esse passo
                    aluno.Nome = Console.ReadLine();

                    Console.WriteLine("Informe a nota do aluno: ");
                    // erro pois uma variavel string para o tipo decimal -> nota é decimal e readline é string

                    //tipo decimal
                    // var funcionalidade de C# de inferência de tipo -> automaticamente não precisa indicar o tipo de variável, ele vai pegar o que estiver inserindo na variável automaticamente
                    // parse vai retornar decimal -> nota passa a ter valor decimal
                    // var nota = decimal.Parse(Console.ReadLine());

                    // aluno.Nota = nota;

                    //poderia fazer direto que nem foi feito anteriormente, atribuindo direto ao objeto
                    // problema: se no momento de informar a nota o usuario informar um valor que não é decimal, vai dar um erro pois o parse vai tentar converter a string e vai falhar
                    // aluno.Nota = decimal.Parse(Console.ReadLine());

                    // outra opção seria tentar o TryParse do decimal -> método que retorna um valor booleano que ve se consegue ou não fazer o parse do valor string para decimal
                    // se conseguir, setar a nota do aluno
                    // tryparse aceita como argumento o valor que vai dizer se consegue ser parseado e também espera um parametro do tipo decimal com modificador de out
                    // declarando a variável nota no próprio método
                    if (decimal.TryParse(Console.ReadLine(), out decimal nota))
                    {
                        aluno.Nota = nota;
                    }
                    else
                    {
                        //caso não possa ser parseado
                        throw new ArgumentException("Valor da nota deve ser decimal");
                    }

                    //array de até 5 é fácil controlar, mais do que isso é mais dificil
                    // coloca o aluno no array já criado na posição indicada
                    alunos[indiceAluno] = aluno;

                    // para que na próxima inclusão ele já vá para a próxima posição do array
                    indiceAluno++;

                    break;
                case "2":
                    // não pode chamar de aluno pois já foi utilizado em cima, como o escopo é pequeno será chamado de 'a'
                    // para cada aluno naquele array vai rodar o comando
                    foreach(var a in alunos)
                    {
                        // if (a != null)
                        // erro pois aluno é uma struct -> só é nulo tipos por referências -> classes, arrays, etc. Quando não instancia, não preenche, pode ser nulo.
                        // nesse caso para simplificar:
                        // se o nome não for vazio, imprime
                        // erro pois nome do aluno está nulo -> string -> tipo referência -> se não setar ela, não inicializar, ela vem por padrão valor nulo.
                        // if (!a.Nome.Equals(""))

                        // outra forma mais interessante invés do Equals
                        if (!string.IsNullOrEmpty(a.Nome))
                        {
                            Console.WriteLine($"ALUNO: {a.Nome} - NOTA: {a.Nota}");
                        }
                    }
                    // esse foreach vai passar por todos os itens do array, até aqueles que foram apenas reservados em memória e que ainda não existem, sendo impressos em tela as marcações vazias
                    // o que se pode fazer é imprimir apenas aqueles que estão preenchidos -> if

                    break;
                case "3":
                    // queremos saber quantos alunos temos preenchidos para pegar a nota Total e dividir pelo número de alunos
                    decimal notaTotal = 0;
                    var nrAlunos = 0;

                    for (int i = 0; i < alunos.Length; i++)
                    {
                        // tem que tratar por nome de novo o array para não mostrar as posições vazias
                        if (!string.IsNullOrEmpty(alunos[i].Nome))
                        {
                            // está dando problema pois a propriedade nota é decimal e está somando com um inteiro, tem que explicitar que nota é decimal (ao inves de var)
                            notaTotal = notaTotal + alunos[i].Nota;
                            // para somar quantos registros de alunos tem
                            nrAlunos++;
                        }
                    }

                    var mediaGeral = notaTotal / nrAlunos;
                    //variavel do tipo conceito que é enum
                    Conceito conceitoGeral;

                    //utilizando enum para exemplificar, retornando a média e conceito contido no enum

                    if (mediaGeral < 2)
                    {
                        conceitoGeral = Conceito.E;
                    }
                    else if (mediaGeral < 4)
                    {
                        conceitoGeral = Conceito.D;
                    }
                    else if (mediaGeral < 6)
                    {
                        conceitoGeral = Conceito.C;
                    }
                    else if (mediaGeral < 8)
                    {
                        conceitoGeral = Conceito.B;
                    }
                    else
                    {
                        conceitoGeral = Conceito.A;
                    }

                    Console.WriteLine($"MÉDIA GERAL: {mediaGeral} - CONCEITO: {conceitoGeral}");

                    break;
                default:
                    // disparar uma exceção que vai informar que o valor informado está fora do range estipulado de opções
                    throw new ArgumentOutOfRangeException();
                }

                //informa de novo o menu dentro do loop do while, que vai manter esse loop ativo até que o usuário informe a opção de x (sair)

                opcaoUsuario = ObterOpcaoUsuario();

            }
        }

        //por ser comandos que se repetem, vamos extrair em um método -> centraliza e aproveita o código

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Inserir novo aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Calcular média geral");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            // método readline retorna string, então criamos uma variavel do tipo string chamada opcaoUsuario que irá salvar o retorno desse método
            string opcaoUsuario = Console.ReadLine();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}


//como estamos usando ReadLine -> alterar na pasta .vscode o launch -> console usa como padrão internalConsole -> mudar para integratedTerminal