using System;

namespace catJogos.Expcetions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            : base("Este jogo já está cadastrado")
        {
            
        }
    }
}