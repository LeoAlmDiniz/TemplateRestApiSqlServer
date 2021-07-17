using System;

namespace catJogos.Expcetions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
            : base("Este jogo não está cadastrado")
        {
            
        }
    }
}