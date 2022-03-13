using System;
using AlgebraLibary;
using System.Collections.Generic;
using unsimplifedForm;

namespace algerbraConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            List<Algebra> b = new List<Algebra>();
            AlgebraExpression al = new AlgebraExpression(b);
            al = al.ToAlgebra("s");
            Matrix M1 = new Matrix();
            M1.MatrixInp = new double[4,4] { { 3, 7, 2, 5 } , { 4, 0, 1, 1 }, { 1, 6, 3, 0 }, { 2, 8, 4, 3 } };
            Console.WriteLine(M1);
            Console.WriteLine($"Inverse = {M1.matrixInverse()}");
            */

            List<Algebra> al = new List<Algebra>(); // Current limitation of the constructors requires this.
            AlgebraExpression eq1 = new AlgebraExpression(al);
            AlgebraExpression eq2 = new AlgebraExpression(al);
            string[] eqs = new string[2] { "-12x -3 +2x^2", "8x" };
            eq1 = eq1.ToAlgebra(eqs[0]);
            eq2 = eq1.ToAlgebra(eqs[1]);
            eq1 = eq1 + eq2;
            Console.WriteLine(eq1);
        }
    }
}
