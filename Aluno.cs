namespace Revisao
{
    //ideal que aluno fosse uma class, mas como é um exemplo muito simples, será uma struct
    public struct Aluno
    {
        //cria linha de uma propriedade para a struct/classe
        //propriedade se caracteriza pelo get/set e podemos alterar a implementação desses para fazer tratativas no momento de alterar o valor da propriedade ou obter o valor dela
        public string Nome { get; set; }
        public decimal Nota { get; set; }
    }
}