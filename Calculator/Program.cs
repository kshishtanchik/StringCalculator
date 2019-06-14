using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Строковый калькулятор");
            string s=null;
            while (s != "нет")
            {
                Console.WriteLine("Введите пример:");
                s = Console.ReadLine();
                var result = new StringCalc(s);
                Console.WriteLine(result.Message);
                Console.WriteLine("Повторить?(да/нет)");
                s = Console.ReadLine();
            }
        }
    }
}