import random

class Card:
    def __init__(self, suit, rank):
        self.suit = suit
        self.rank = rank
        self.value = 10 if rank in ['J', 'Q', 'K'] else (11 if rank == 'A' else int(rank))

    def __str__(self):
        return f"{self.rank} {self.suit}"

class Deck:
    def __init__(self):
        suits = ['Kier', 'Karo', 'Trefl', 'Pik']
        ranks = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A']
        self.cards = [Card(s, r) for s in suits for r in ranks]
        random.shuffle(self.cards)

    def deal(self):
        return self.cards.pop()

class Hand:
    def __init__(self):
        self.cards = []
        self.value = 0
        self.aces = 0

    def add_card(self, card):
        self.cards.append(card)
        self.value += card.value
        if card.rank == 'A': self.aces += 1
        while self.value > 21 and self.aces:
            self.value -= 10
            self.aces -= 1

class Player:
    def __init__(self, name="Gracz"):
        self.name = name
        self.hand = Hand()

def play_blackjack():
    deck = Deck()
    player = Player("Gracz")
    dealer = Player("Dealer")

    # Rozdanie
    for _ in range(2):
        player.hand.add_card(deck.deal())
        dealer.hand.add_card(deck.deal())

    print(f"Twoje karty: {[str(c) for c in player.hand.cards]} | Suma: {player.hand.value}")
    print(f"Karta dealera: {dealer.hand.cards[0]}")

    # Tura gracza
    while player.hand.value < 21:
        choice = input("Chcesz dobrać (h) czy spasować (s)? ").lower()
        if choice == 'h':
            player.hand.add_card(deck.deal())
            print(f"Twoje karty: {[str(c) for c in player.hand.cards]} | Suma: {player.hand.value}")
        else:
            break

    if player.hand.value > 21:
        print("Przegrałeś! Przekroczyłeś 21.")
        return

    # Tura dealera
    print(f"\nTura dealera. Karty dealera: {[str(c) for c in dealer.hand.cards]}")
    while dealer.hand.value < 17:
        dealer.hand.add_card(deck.deal())
        print(f"Dealer dobiera: {dealer.hand.cards[-1]} | Suma: {dealer.hand.value}")

    # Wynik końcowy
    if dealer.hand.value > 21 or player.hand.value > dealer.hand.value:
        print("Wygrałeś!")
    elif player.hand.value < dealer.hand.value:
        print("Dealer wygrywa.")
    else:
        print("Remis!")

if __name__ == "__main__":
    play_blackjack()
