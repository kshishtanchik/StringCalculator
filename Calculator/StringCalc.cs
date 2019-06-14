using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    public class StringCalc
    {
        private const string memberPattern = @"(?<operator>([\+\-\*\/]{0,1}))(?<member>(\d+\,{0,1}\d*)|(\((([\+\-\*\/]{0,1})(\d+\,{0,1}\d*))*\)))";
        private const string formatPattern = @"[\d\(\)\+\-\*\/\,]";
        public decimal MathResult{ get; private set; }
        public string Message { get; private set; }

        public StringCalc(string s)
        {
           
            if (Regex.IsMatch(s, formatPattern))
            {
                MathResult = mathSimpleExpretion(s);
                Message = string.Format("Результат вычисленияя: {0}", MathResult);                
            }                
            else
                Message = "Введенная строка содержит недопустиые символы";
        }
        private decimal mathSimpleExpretion(string s)
        {
            decimal result = 0;            
            var members = new Queue<ExpretionMember>();
            var expretionMembers = Regex.Matches(s, memberPattern);
            foreach (Match item in expretionMembers)
            {
                var currentStringMember = item.Groups["member"].Value;
                if (decimal.TryParse(currentStringMember, out decimal decCurrentmember))
                    members.Enqueue(new ExpretionMember()
                    {
                        Operation = item.Groups["operator"].Value,
                        Member = item.Groups["member"].Value
                    });
                else
                {
                    var expiration = mathSimpleExpretion(currentStringMember.TrimStart('(').TrimEnd(')'));
                    members.Enqueue(new ExpretionMember()
                    {
                        Member = expiration.ToString(),
                        Operation= item.Groups["operator"].Value
                    });
                }   
            }
            while (members.Count!=0)
            {
                var currentMember=members.Dequeue();
                if (members.Count > 0)
                {
                    var nextMember = members.Peek();
                    switch (nextMember.Operation)
                    {
                        case "*":
                            var multiple =decimal.Multiply(currentMember.GetDecimalValue, nextMember.GetDecimalValue);
                            result += multiple;
                            members.Dequeue();
                            continue;
                        case "/":
                            var division =decimal.Divide(currentMember.GetDecimalValue, nextMember.GetDecimalValue);
                            result += division;
                            members.Dequeue();
                            continue;
                        default:
                            break;
                    }
                }

                result = decimal.Add(result, currentMember.GetDecimalValue);

            }
            return result;
        }
    }

    class ExpretionMember
    {
        public string Operation { set; get; }
        public string Member { get; set; }
        public decimal GetDecimalValue
        {
            get
            {
                var strmember = string.Join("", new List<string>() { Operation, Member });
                if (decimal.TryParse(strmember, out decimal decMember))
                    return decMember;
                return decimal.Parse(Member);
            }
        }
    }
}
