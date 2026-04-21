using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame
{
    
    public class Card
    {
        public string Suit { get; }
        public string Rank { get; }
        public int Value { get; }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
            
            Value = rank switch
            {
                "J" or "Q" or "K" => 10,
                "A" => 11,
                _ => int.Parse(rank)
            };
        }

        public override string ToString() => $"{Rank} {Suit}";
    }

   
    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random rng = new Random();

        public Deck()
        {
            string[] suits = { "Kier", "Karo", "Trefl", "Pik" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            
            foreach (var s in suits)
                foreach (var r in ranks)
                    cards.Add(new Card(s, r));

            // Tasowanie
            cards = cards.OrderBy(x => rng.Next()).ToList();
        }

        public Card Deal()
        {
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    
    public class Hand
    {
        public List<Card> Cards { get; } = new List<Card>();
        public int TotalValue { get; private set; }
        private int aces = 0;

        public void AddCard(Card card)
        {
            Cards.Add(card);
            TotalValue += card.Value;
            if (card.Rank == "A") aces++;
            AdjustForAce();
        }

        private void AdjustForAce()
        {
            while (TotalValue > 21 && aces > 0)
            {
                TotalValue -= 10;
                aces--;
            }
        }
    }

   
    public class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; } = new Hand();
    }

    public class Dealer : Player
    {
        public Dealer() { Name = "Dealer"; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WITAJ W BLACKJACKU ===");
            
            Deck deck = new Deck();
            Player player = new Player { Name = "Kuba" };
            Dealer dealer = new Dealer();

            // Rozdanie 
            player.Hand.AddCard(deck.Deal());
            dealer.Hand.AddCard(deck.Deal());
            player.Hand.AddCard(deck.Deal());
            dealer.Hand.AddCard(deck.Deal());

            // Tura gracza
            while (true)
            {
                Console.WriteLine($"\nTwoje karty: {string.Join(", ", player.Hand.Cards)}");
                Console.WriteLine($"Twoja suma: {player.Hand.TotalValue}");
                Console.WriteLine($"Pierwsza karta dealera: {dealer.Hand.Cards[0]}");

                if (player.Hand.TotalValue >= 21) break;

                Console.Write("\nChcesz dobrać (h) czy spasować (s)? ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "h")
                    player.Hand.AddCard(deck.Deal());
                else
                    break;
            }

            
            if (player.Hand.TotalValue > 21)
            {
                Console.WriteLine("\nPrzekroczyłeś 21! Dealer wygrywa.");
            }
            else
            {
                // Tura dealera
                Console.WriteLine("\n--- Tura Dealera ---");
                while (dealer.Hand.TotalValue < 17)
                {
                    dealer.Hand.AddCard(deck.Deal());
                    Console.WriteLine($"Dealer dobiera: {dealer.Hand.Cards.Last()}");
                }

                Console.WriteLine($"Suma Dealera: {dealer.Hand.TotalValue}");

                
                if (dealer.Hand.TotalValue > 21)
                    Console.WriteLine("Dealer przekroczył 21! Wygrywasz!");
                else if (player.Hand.TotalValue > dealer.Hand.TotalValue)
                    Console.WriteLine("Wygrałeś!");
                else if (player.Hand.TotalValue < dealer.Hand.TotalValue)
                    Console.WriteLine("Dealer wygrywa.");
                else
                    Console.WriteLine("Remis!");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wyjść...");
            Console.ReadKey();
        }
    }
}
