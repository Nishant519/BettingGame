using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BettingGame
{
    public class Bet
    {
        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _dog;

        public int Dog
        {
            get { return _dog; }
            set { _dog = value; }
        }

        private Punter _bettor;

        public Punter Bettor
        {
            get { return _bettor; }
            set { _bettor = value; }
        }

        public string GetDescription()
        {
            if (this._amount == 0) // mean initially user doesnot have any bucks or bet placed
                return this._bettor.Name + " hasn't placed any bet";
            else // else return what he placed and on what dog
                return this._bettor.Name + " placed " + this._bettor.MyBet._amount.ToString() + " bucks on dog # " + this._bettor.MyBet.Dog.ToString();
        }

        public int Payout(int winningDogNo)
        {
            if (this._bettor.MyBet.Dog == winningDogNo)
                return this._amount;
            else
                return -this._amount;
        }
    }
}
