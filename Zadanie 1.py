num1 = float(input("Liczba 1: "))
num2 = float(input("Liczba 2: "))
op = input("Działanie (+, -, *, /): ")

if op == '+':
    print(num1 + num2)
elif op == '-':
    print(num1 - num2)
elif op == '*':
    print(num1 * num2)
elif op == '/':
    if num2 == 0:
        print("Nie dziel przez 0!")
    else:
        print(num1 / num2)
else:
    print("Błędny znak")