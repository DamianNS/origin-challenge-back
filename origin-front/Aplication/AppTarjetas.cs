using origin_front.Domain;
using origin_front.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace origin_front.Aplication
{
    public class AppTarjetas
    {
        public enum CardStatus
        {
            Ok,
            Blocked,
            Unauthorized
        }

        private readonly OriginContext _dbContext;
        public AppTarjetas(
            OriginContext dbContext
        ) {
            this._dbContext = dbContext;
        }

        public Tarjeta searchCard(string id)
        {
            var card = this._dbContext.Tarjetas.Find(id);
            if(card == null)
            {
                throw new NotFoundException();
            }

            card.Lock = card.Lock.HasValue ? card.Lock.Value : false;

            return card;
        }

        public CardStatus validatePin(string id, int ping)
        {
            var card = this.searchCard(id);

            if (card.Lock.HasValue && card.Lock.Value)
            {
                return CardStatus.Blocked;
            }

            if (card.Pin.HasValue)
            {
                if( card.Pin.Value == ping)
                {
                    return CardStatus.Ok;
                }

                card.Intentos = (card.Intentos ?? 0) + 1;
                if(card.Intentos > 3)
                {
                    card.Lock = true;
                }
                this._dbContext.Tarjetas.Update(card);
                this._dbContext.SaveChanges();
            }

            return CardStatus.Unauthorized;
        }
    }
}
