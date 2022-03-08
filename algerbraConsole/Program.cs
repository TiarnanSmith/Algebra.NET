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
            
            List<Algebra> b = new List<Algebra>();
            AlgebraExpression al = new AlgebraExpression(b);
            al = al.ToAlgebra("s");
            
            Matrix M1 = new Matrix();

            M1.MatrixInp = new double[4,4] { { 3, 7, 2, 5 } , { 4, 0, 1, 1 }, { 1, 6, 3, 0 }, { 2, 8, 4, 3 } };
            Console.WriteLine(M1);

            Console.WriteLine($"Inverse = {M1.matrixInverse()}");

        }
    }
}
