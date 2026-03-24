liczba_ocen = int(input("Ile ocen chcesz wprowadzić? "))
suma_ocen = 0

for i in range(liczba_ocen):
    ocena = float(input(f"Podaj ocenę {i + 1} (1-6): "))
    suma_ocen += ocena

srednia = suma_ocen / liczba_ocen

print(f"Twoja średnia to: {srednia:.2f}")

if srednia >= 3.0:
    print("Gratulacje! Zaliczyłeś przedmiot.")
else:
    print("Niestety, nie udało się zaliczyć przedmiotu.")
