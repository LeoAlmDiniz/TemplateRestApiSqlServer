using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catJogos.Entitites;

namespace catJogos.Repositorio
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("60b726ae-2ff2-497d-9cb3-bc35faccfc56"), new Jogo {Id = Guid.Parse("60b726ae-2ff2-497d-9cb3-bc35faccfc56"), Nome = "Fifa 21", Produtora = "EA", Preco = 200} },
            {Guid.Parse("6ad7a24a-0a72-4a8d-958a-ef2ad4e4d927"), new Jogo {Id = Guid.Parse("6ad7a24a-0a72-4a8d-958a-ef2ad4e4d927"), Nome = "Fifa 20", Produtora = "EA", Preco = 160} },
            {Guid.Parse("0accd4eb-486b-4f35-9de8-489d334d7cdf"), new Jogo {Id = Guid.Parse("0accd4eb-486b-4f35-9de8-489d334d7cdf"), Nome = "Fifa 19", Produtora = "EA", Preco = 140} },
            {Guid.Parse("56ec9690-97c7-4066-a698-bdcd6d7cc7b8"), new Jogo {Id = Guid.Parse("56ec9690-97c7-4066-a698-bdcd6d7cc7b8"), Nome = "Fifa 18", Produtora = "EA", Preco = 89} },
            {Guid.Parse("7578c0b6-3ea3-4372-8477-e64780d84e39"), new Jogo {Id = Guid.Parse("7578c0b6-3ea3-4372-8477-e64780d84e39"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("3faee789-5edb-4ed7-89b8-4641f91a9f8f"), new Jogo {Id = Guid.Parse("3faee789-5edb-4ed7-89b8-4641f91a9f8f"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //não é necessário pois não tem conexão com o banco
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina-1)*quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
            {
                return null;
            }
            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
    }
}