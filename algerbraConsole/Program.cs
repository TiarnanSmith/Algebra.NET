using System;
using AlgebraLibary;
using System.Collections.Generic;

namespace algerbraConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
            Matrix M1 = new Matrix();
            M1.MatrixInp = new double[4,4] { { 3, 7, 2, 5 } , { 4, 0, 1, 1 }, { 1, 6, 3, 0 }, { 2, 8, 4, 3 } };
            Console.WriteLine(M1);
            */

            AlgebraEquation algebraEquation = new AlgebraEquation();


            AlgebraExpression eq1 = algebraEquation.QuadraticGen(50, 2);
            AlgebraExpression eq2 = algebraEquation.QuadraticGen(50, 2);

            eq1 = eq1 * eq2;
            Console.WriteLine(eq1);
            
        }
    }
}
