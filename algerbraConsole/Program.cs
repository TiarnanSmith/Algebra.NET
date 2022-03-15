using System;
using AlgebraLibary;
using System.Collections.Generic;

namespace algerbraConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            
            Matrix M1 = new Matrix();
            M1.MatrixInp = new double[4,4] { { 3, 7, 2, 5 } , { 4, 0, 1, 1 }, { 1, 6, 3, 0 }, { 2, 8, 4, 3 } };
            Console.WriteLine(M1);
            
            
            /*
            List<Algebra> al = new List<Algebra>(); // Current limitation of the constructors requires this.
            AlgebraExpression bal = new AlgebraExpression(al);
            string[] eqs = new string[3] { "-2x^2", "8x", "9x" };
            AlgebraExpression eq1 = bal.ToAlgebra(eqs[0]);
            AlgebraExpression eq2 = bal.ToAlgebra(eqs[1]);
            AlgebraExpression eq3 = bal.ToAlgebra(eqs[2]);
            eq1 = eq1 + eq2 + eq3;
            Console.WriteLine(eq1);
            */
        }
    }
}
