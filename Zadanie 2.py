skala = input("Wybierz kierunek (C - ºC na ºF || F - ºF na ºC): ").upper()
temp = float(input("Podaj wartość temperatury: "))

if skala == 'C':
    wynik = (temp * 9/5) + 32
    print(f"{temp}°C to {wynik}°F")
elif skala == 'F':
    wynik = (temp - 32) * 5/9
    print(f"{temp}°F to {wynik}°C")
else:
    print("Nieprawidłowy wybór. Wybierz C lub F.")