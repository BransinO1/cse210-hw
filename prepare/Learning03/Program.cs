// Week 03 practice assignment - creating fraction and decimal values - encapsulation (private and public classes)

using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    public Fraction(int numerator)
    {
        this.numerator = numerator;
        denominator = 1;
    }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        this.numerator = numerator;
        this.denominator = denominator;
    }

    public int Numerator
    {
        get { return numerator; }
        set { numerator = value; }
    }

    public int Denominator
    {
        get { return denominator; }
        set
        {
            if (value == 0)
                throw new ArgumentException("Denominator cannot be zero.");
            denominator = value;
        }
    }

    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}
class Program
{
    static void Main()
    {
        // Pay attention to how the paranethesis creates the fractions below. 
        Fraction fraction1 = new Fraction();       // 1/1
        Fraction fraction2 = new Fraction(5);      // 5/1
        Fraction fraction3 = new Fraction(3, 4);   // 3/4
        Fraction fraction4 = new Fraction(1, 3);   // 1/3

        // Verify and display different fraction values
        Console.WriteLine("Fraction 1: " + fraction1.GetFractionString());
        Console.WriteLine("Decimal value: " + fraction1.GetDecimalValue());

        Console.WriteLine("Fraction 2: " + fraction2.GetFractionString());
        Console.WriteLine("Decimal value: " + fraction2.GetDecimalValue());

        Console.WriteLine("Fraction 3: " + fraction3.GetFractionString());
        Console.WriteLine("Decimal value: " + fraction3.GetDecimalValue());

        Console.WriteLine("Fraction 4: " + fraction4.GetFractionString());
        Console.WriteLine("Decimal value: " + fraction4.GetDecimalValue());
    }
}