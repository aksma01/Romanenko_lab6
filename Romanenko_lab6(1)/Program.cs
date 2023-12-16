using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romanenko_lab6_1_
{
    public class Calculator<T>
    {
        public delegate T AdditionDelegate(T a, T b);
        public delegate T SubtractionDelegate(T a, T b);
        public delegate T MultiplicationDelegate(T a, T b);
        public delegate T DivisionDelegate(T a, T b);

        public AdditionDelegate Add { get; set; }
        public SubtractionDelegate Subtract { get; set; }
        public MultiplicationDelegate Multiply { get; set; }
        public DivisionDelegate Divide { get; set; }
       
        public Calculator()
        {           
            Add = (a, b) => OperatorAdd(a, b);
            Subtract = (a, b) => OperatorSubtract(a, b);
            Multiply = (a, b) => OperatorMultiply(a, b);
            Divide = (a, b) => OperatorDivide(a, b);
        }
       
        public T PerformAddition(T a, T b) => Add(a, b);
        public T PerformSubtraction(T a, T b) => Subtract(a, b);
        public T PerformMultiplication(T a, T b) => Multiply(a, b);
        public T PerformDivision(T a, T b) => Divide(a, b);
       
        private T OperatorAdd(T a, T b) => (dynamic)a + (dynamic)b;
        private T OperatorSubtract(T a, T b) => (dynamic)a - (dynamic)b;
        private T OperatorMultiply(T a, T b) => (dynamic)a * (dynamic)b;
        private T OperatorDivide(T a, T b) => (dynamic)a / (dynamic)b;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Calculator<int> intCalculator = new Calculator<int>();
            Console.WriteLine("Integer calculations:");
            Console.WriteLine($"Addition: {intCalculator.PerformAddition(5, 3)}");
            Console.WriteLine($"Subtraction: {intCalculator.PerformSubtraction(10, 7)}");
            Console.WriteLine($"Multiplication: {intCalculator.PerformMultiplication(4, 5)}");
            Console.WriteLine($"Division: {intCalculator.PerformDivision(15, 3)}");
           
            Calculator<double> doubleCalculator = new Calculator<double>();
            Console.WriteLine("\nDouble calculations:");
            Console.WriteLine($"Addition: {doubleCalculator.PerformAddition(5.5, 3.2)}");
            Console.WriteLine($"Subtraction: {doubleCalculator.PerformSubtraction(10.8, 7.1)}");
            Console.WriteLine($"Multiplication: {doubleCalculator.PerformMultiplication(4.2, 5.7)}");
            Console.WriteLine($"Division: {doubleCalculator.PerformDivision(15.6, 3.3)}");
        }
    }
}
