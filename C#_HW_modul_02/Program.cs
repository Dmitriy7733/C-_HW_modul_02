using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

Console.WriteLine(@"Задание №1. Объявить одномерный(5 элементов) массив с именем A и двумерный массив (3 строки, 4 столбца) дробных чисел с именем B. Заполнить одномерный массив 
А числами, введенными с клавиатуры пользователем, а 
двумерный массив В случайными числами с помощью 
циклов. Вывести на экран значения массивов: массива
А в одну строку, массива В — в виде матрицы. Найти в 
данных массивах общий максимальный элемент, минимальный элемент, общую сумму всех элементов, общее 
произведение всех элементов, сумму четных элементов 
массива А, сумму нечетных столбцов массива В.");

// Одномерный массив A

double[] A = new double[5];

// Двумерный массив B
double[,] B = new double[3, 4];
for (int i = 0; i < A.Length; i++)
{
    Console.Write($"Введите значение для A[{i}]: ");
    A[i] = Convert.ToDouble(Console.ReadLine());
}

Random random = new Random();
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 4; j++)
    {
        B[i, j] = random.NextDouble();
    }
}

Console.Write("Массив A: ");
foreach (var element in A)
{
    Console.Write(element + " ");
}
Console.WriteLine();
Console.WriteLine("Массив B:");
for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 4; j++)
    {
        Console.Write(B[i, j] + " ");
    }
    Console.WriteLine();
}

double maxA = A.Max();
double minA = A.Min();
double sumA = A.Sum();
double productA = A.Aggregate(1.0, (acc, x) => acc * x);

double maxB = B.Cast<double>().Max();
double minB = B.Cast<double>().Min();
double sumB = B.Cast<double>().Sum();
double productB = B.Cast<double>().Aggregate(1.0, (acc, x) => acc * x);

Console.WriteLine($"Максимальный элемент в A: {maxA}");
Console.WriteLine($"Минимальный элемент в A: {minA}");
Console.WriteLine($"Общая сумма элементов в A: {sumA}");
Console.WriteLine($"Общее произведение элементов в A: {productA}");

Console.WriteLine($"Максимальный элемент в B: {maxB}");
Console.WriteLine($"Минимальный элемент в B: {minB}");
Console.WriteLine($"Общая сумма элементов в B: {sumB}");
Console.WriteLine($"Общее произведение элементов в B: {productB}");

double sumEvenA = A.Where(x => x % 2 == 0).Sum();

double sumOddColumnsB = 0.0;
for (int j = 0; j < 4; j++)
{
    double columnSum = 0.0;
    for (int i = 0; i < 3; i++)
    {
        columnSum += B[i, j];
    }
    if (j % 2 != 0)
    {
        sumOddColumnsB += columnSum;
    }
}

Console.WriteLine($"Сумма четных элементов в A: {sumEvenA}");
Console.WriteLine($"Сумма элементов нечетных столбцов в B: {sumOddColumnsB}");

Console.WriteLine(@"Задание 2. Дан двумерный массив размерностью 5×5, заполненный случайными числами из диапазона от –100 до 100. 
Определить сумму элементов массива, расположенных 
между минимальным и максимальным элементами.");


int[,] array = new int[5, 5];
Random random1 = new Random();

for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        array[i, j] = random.Next(-100, 101);
    }
}
int min = array[0, 0];
int max = array[0, 0];
int minRow = 0;
int minCol = 0;
int maxRow = 0;
int maxCol = 0;

for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        if (array[i, j] < min)
        {
            min = array[i, j];
            minRow = i;
            minCol = j;
        }
        if (array[i, j] > max)
        {
            max = array[i, j];
            maxRow = i;
            maxCol = j;
        }
    }
}
int sum = 0;

int startRow = Math.Min(minRow, maxRow);
int endRow = Math.Max(minRow, maxRow);
int startCol = Math.Min(minCol, maxCol);
int endCol = Math.Max(minCol, maxCol);

for (int i = startRow; i <= endRow; i++)
{
    for (int j = startCol; j <= endCol; j++)
    {
        sum += array[i, j];
    }
}
Console.WriteLine("Двумерный массив:");
for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        Console.Write(array[i, j] + "\t");
    }
    Console.WriteLine();
}

Console.WriteLine($"Минимальный элемент: {min}, Максимальный элемент: {max}");
Console.WriteLine($"Сумма элементов между минимальным и максимальным элементами: {sum}");

Console.WriteLine(@"Задание 3. Пользователь вводит строку с клавиатуры. Необходимо зашифровать данную строку используя шифр Цезаря.
Кроме механизма шифровки, реализуйте механизм расшифрования");


Console.WriteLine("Введите строку для шифрования:");
string input = Console.ReadLine();

Console.WriteLine("Введите сдвиг (целое число):");
int shift = Convert.ToInt32(Console.ReadLine());

string encrypted = Encrypt(input, shift);
Console.WriteLine($"Зашифрованная строка: {encrypted}");

string decrypted = Decrypt(encrypted, shift);
Console.WriteLine($"Расшифрованная строка: {decrypted}");

static string Encrypt(string input, int shift)
{
    char[] result = input.ToCharArray();

    for (int i = 0; i < result.Length; i++)
    {
        char originalChar = result[i];

        if (char.IsLetter(originalChar))
        {
            char shiftedChar = (char)(originalChar + shift);

            if ((char.IsLower(originalChar) && shiftedChar > 'z') ||
                (char.IsUpper(originalChar) && shiftedChar > 'Z'))
            {
                shiftedChar = (char)(shiftedChar - 26);
            }

            result[i] = shiftedChar;
        }
    }

    return new string(result);
}

static string Decrypt(string input, int shift)
{
    return Encrypt(input, -shift);
}
Console.WriteLine(@"Задание №4. Создайте приложение, которое производит операции 
над матрицами:
■ Умножение матрицы на число;
■ Сложение матриц;
■ Произведение матриц");


Console.WriteLine("Введите размерность матрицы A (строки и столбцы):");
int rowsA = int.Parse(Console.ReadLine());
int colsA = int.Parse(Console.ReadLine());

Console.WriteLine("Введите размерность матрицы B (строки и столбцы):");
int rowsB = int.Parse(Console.ReadLine());
int colsB = int.Parse(Console.ReadLine());

if (colsA != rowsB)
{
    Console.WriteLine("Умножение матриц невозможно. Количество столбцов матрицы A должно быть равно количеству строк матрицы B.");
    return;
}

double[,] matrixA = new double[rowsA, colsA];
double[,] matrixB = new double[rowsB, colsB];

Console.WriteLine("Введите элементы матрицы A:");
ReadMatrix(matrixA);

Console.WriteLine("Введите элементы матрицы B:");
ReadMatrix(matrixB);

Console.WriteLine("Выберите операцию:");
Console.WriteLine("1. Умножение матрицы на число");
Console.WriteLine("2. Сложение матриц");
Console.WriteLine("3. Произведение матриц");

int choice = int.Parse(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.WriteLine("Введите число, на которое нужно умножить матрицу A:");
        double scalar = double.Parse(Console.ReadLine());
        MultiplyMatrixByScalar(matrixA, scalar);
        break;
    case 2:
        if (rowsA != rowsB || colsA != colsB)
        {
            Console.WriteLine("Сложение матриц невозможно. Размерности матриц должны совпадать.");
            return;
        }
        AddMatrices(matrixA, matrixB);
        break;
    case 3:
        MultiplyMatrices(matrixA, matrixB);
        break;
    default:
        Console.WriteLine("Некорректный выбор операции.");
        break;
}
    

    static void ReadMatrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"Введите элемент [{i}, {j}]: ");
            matrix[i, j] = double.Parse(Console.ReadLine());
        }
    }
}

static void MultiplyMatrixByScalar(double[,] matrix, double scalar)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    Console.WriteLine("Результат умножения матрицы на число:");

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            matrix[i, j] *= scalar;
            Console.Write($"{matrix[i, j]} ");
        }
        Console.WriteLine();
    }
}

static void AddMatrices(double[,] matrixA, double[,] matrixB)
{
    int rows = matrixA.GetLength(0);
    int cols = matrixA.GetLength(1);

    Console.WriteLine("Результат сложения матриц:");

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            matrixA[i, j] += matrixB[i, j];
            Console.Write($"{matrixA[i, j]} ");
        }
        Console.WriteLine();
    }
}

static void MultiplyMatrices(double[,] matrixA, double[,] matrixB)
{
    int rowsA = matrixA.GetLength(0);
    int colsA = matrixA.GetLength(1);
    int rowsB = matrixB.GetLength(0);
    int colsB = matrixB.GetLength(1);

    Console.WriteLine("Результат произведения матриц:");

    double[,] resultMatrix = new double[rowsA, colsB];

    for (int i = 0; i < rowsA; i++)
    {
        for (int j = 0; j < colsB; j++)
        {
            for (int k = 0; k < colsA; k++)
            {
                resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
            }
            Console.Write($"{resultMatrix[i, j]} ");
        }
        Console.WriteLine();
    }
 }

Console.WriteLine(@"Задание 5. Пользователь с клавиатуры вводит в строку арифметическое выражение. 
Приложение должно посчитать его результат. Необходимо поддерживать только две операции: + и –.*/");


Console.Write("Введите арифметическое выражение (+ и -): ");
string inputExpression = Console.ReadLine();

try
{
    // Вычисляем результат выражения
    double result = EvaluateExpression(inputExpression);
    Console.WriteLine("Результат: " + result);
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
}

Console.ReadLine();


    static double EvaluateExpression(string expression)
{
    // Разбиваем строку на части по операторам + и -
    string[] parts = expression.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

    // Первое число в выражении
    double result = double.Parse(parts[0]);

    // Проходим по остальным частям выражения и выполняем соответствующие операции
    for (int i = 1; i < parts.Length; i++)
    {
        char operation = expression[parts[i - 1].Length]; // Оператор (+ или -)
        double operand = double.Parse(parts[i]);

        if (operation == '+')
        {
            result += operand;
        }
        else if (operation == '-')
        {
            result -= operand;
        }
    }

    return result;
}
Console.WriteLine(@"Задание 6. Пользователь с клавиатуры вводит некоторый текст. 
Приложение должно изменять регистр первой буквы 
каждого предложения на букву в верхнем регистре");

Console.WriteLine("Введите текст:");
string inputText = Console.ReadLine();

string resultText = CapitalizeFirstLetterInSentences(inputText);

Console.WriteLine("Результат:");
Console.WriteLine(resultText);

Console.ReadLine();
static string CapitalizeFirstLetterInSentences(string input)
{
    // Используем регулярное выражение для разбивки текста на предложения
    string[] sentences = Regex.Split(input, @"(?<=[\.!?])\s+");

    // Проходим по каждому предложению и изменяем регистр первой буквы
    for (int i = 0; i < sentences.Length; i++)
    {
        if (!string.IsNullOrWhiteSpace(sentences[i]))
        {
            // Получаем первый символ и преобразуем его в верхний регистр
            char firstChar = char.ToUpper(sentences[i][0], CultureInfo.CurrentCulture);

            // Составляем измененное предложение
            sentences[i] = firstChar + sentences[i].Substring(1);
        }
    }

    // Объединяем предложения обратно в текст
    string result = string.Join(" ", sentences);
    return result;
}

Console.WriteLine(@"Задание 7. Создайте приложение, проверяющее текст на недопустимые слова. Если недопустимое слово найдено, оно 
должно быть заменено на набор символов *. По итогам 
работы приложения необходимо показать статистику действий.");
Console.WriteLine("Введите текст:");
string inputText1 = Console.ReadLine();

Console.WriteLine("Введите список недопустимых слов (разделяйте запятой):");
string forbiddenWordsInput = Console.ReadLine();
List<string> forbiddenWords = new List<string>(forbiddenWordsInput.Split(','));

int wordReplacements = 0;

string resultText1 = ReplaceForbiddenWords(inputText1, forbiddenWords, out wordReplacements);

Console.WriteLine("Результат:");
Console.WriteLine(resultText1);
Console.WriteLine($"Количество замененных слов: {wordReplacements}");

Console.ReadLine();

    static string ReplaceForbiddenWords(string text, List<string> forbiddenWords, out int wordReplacements)
{
    wordReplacements = 0;
    foreach (string forbiddenWord in forbiddenWords)
    {
        string pattern = "\\b" + Regex.Escape(forbiddenWord.Trim()) + "\\b"; // Убираем пробелы вокруг слова

        int localWordReplacements = wordReplacements;
        text = Regex.Replace(text, pattern, match =>
        {
            localWordReplacements++;
            return new string('*', match.Length);
        }, RegexOptions.IgnoreCase);

        wordReplacements = localWordReplacements;
    }
    return text;
}
