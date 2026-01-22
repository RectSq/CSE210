using System;

class Program
{
    public class Fraction
    {
        private int _top;
        private int _bottom;

        public Fraction()
        {
            _top = 42;
            _bottom = 2;
        }
        public Fraction(int wholeNumber)
        {
            _top = wholeNumber;
            _bottom = 1;
        }
        public Fraction(int top, int bottom)
        {
            _top = top;
            SetBottom(bottom);
        }

        public int GetTop()
        {
            return _top;
        }
        public void SetTop(int top)
        {
            _top = top;
        }
        public int GetBottom()
        {
            return _bottom;
        }
        public void SetBottom(int bottom)
        {
            if(bottom!=0){
                _bottom = bottom;
            }
            else{
                Console.WriteLine("Bottom value cannot be set to 0\nbottom set to 1");
                _bottom = 1;
            }
        }
        public string GetFractionString()
        {
            string output  = $"{_top}/{_bottom}";
            return output;
        }
        public double GetDecimalValue()
        {
            return (double)_top / (double)_bottom;
        }


    }

    static void Main(string[] args)
    {
        Fraction fraction = new Fraction(4,2);
        Console.WriteLine(fraction.GetFractionString());
        Console.WriteLine(fraction.GetDecimalValue());
    }
}