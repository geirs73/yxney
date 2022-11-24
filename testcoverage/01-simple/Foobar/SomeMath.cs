namespace Foobar;
public class SomeMath
{
    public static int Add(int a, int b) => a + b;
    public static int Divide(int a, int b) => a / b;
    public static bool GreaterThanOrEqual(int a, int b) => a >= b;

    public static int MaxValueInArray(int[] numbers)
    {
        int maxValue = Int32.MinValue;
        foreach (int number in numbers)
        {
            if (number > maxValue)
            {
                maxValue = number;
            }
        }
        return maxValue;
    }

    public static int[] EvenNumbersInArray(int [] numbers)
    {
        List<int> evenNumbers = new();
        foreach (int number in numbers)
        {
            if (number % 2 == 0) {
                evenNumbers.Add(number);
            }
        }
        return evenNumbers.ToArray();
    }

}
