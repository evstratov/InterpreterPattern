using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            IExpression exp = context.Evaluate("1-2+31");
            Console.WriteLine(exp.Interprete());
            Console.ReadKey();  
        }
    }
    interface IExpression
    {
        int Interprete();
    }

    class NumberExpression : IExpression
    {
        int number;
        public NumberExpression(int _number)
        {
            number = _number;
        }
        public int Interprete()
        {
            return number;
        }
    }
    class MinusExpression : IExpression
    {
        IExpression left;
        IExpression right;

        public MinusExpression(IExpression _left, IExpression _right)
        {
            left = _left;
            right = _right;
        }
        public int Interprete()
        {
            return left.Interprete() - right.Interprete();
        }
    }
    class PlusExpression : IExpression
    {
        IExpression left;
        IExpression right;

        public PlusExpression(IExpression _left, IExpression _right)
        {
            left = _left;
            right = _right;
        }
        public int Interprete()
        {
            return left.Interprete() + right.Interprete();
        }
    }
    class Context
    {
        public IExpression Evaluate(string s)
        {
            int pos = s.Length - 1;
            while(pos > 0)
            {
                if (Char.IsDigit(s[pos]))
                {
                    pos--;
                }
                else
                {
                    IExpression left = Evaluate(s.Substring(0, pos));
                    IExpression right = new NumberExpression(int.Parse(s.Substring(pos + 1, s.Length-pos-1)));
                    char oper = s[pos];
                    switch(oper)
                    {
                        case '-':
                            return new MinusExpression(left, right);
                            break;
                        case '+':
                            return new PlusExpression(left, right);
                            break;
                    }
                }
            }
            int result = int.Parse(s);
            return new NumberExpression(result);
        }
    }
}
