using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace AlgebraLibary
{
    public class Algebra
    {
        public double Coefficient { get; set; }
        public string Unknown { get; set; }
        public double XPower { get; set; }

        public Algebra(double coefficient, double xpower, string unknown)
        {
            Coefficient = coefficient;
            XPower = xpower;
            Unknown = unknown;
        }

        public Algebra()
        {
            Coefficient = 0;
            XPower = 0;
            Unknown = "x";
        }

        public static Algebra operator +(Algebra a, Algebra b)
        {
            return a.Unknown == b.Unknown ? (a.XPower == b.XPower ? new(a.Coefficient + b.Coefficient, a.XPower = a.XPower, a.Unknown = a.Unknown) : null) : null;
        }
        public static Algebra operator -(Algebra a, Algebra b)
        {
            return a.Unknown == b.Unknown ? (a.XPower == b.XPower ? new(a.Coefficient - b.Coefficient, a.XPower = a.XPower, a.Unknown = a.Unknown) : null) : null;
        }
        public static Algebra operator /(Algebra a, Algebra b)
        {
            if (a.Unknown == b.Unknown)
            {
                return new(a.Coefficient * b.Coefficient, a.XPower = a.XPower, a.Unknown = b.Unknown);
            }
            else if (a.Unknown == "") // if there is no x term
            {
                return new(a.Coefficient / b.Coefficient, a.XPower = a.XPower, a.Unknown = b.Unknown);
            }
            else // (a.Unknown != b.Unknown) If the x and y terms are not the same
            {
                return new(a.Coefficient / b.Coefficient, a.XPower = a.XPower, a.Unknown = a.Unknown + b.Unknown);
            }
        }


        public static Algebra operator *(Algebra a, Algebra b)
        {
            if (a.Unknown == b.Unknown)
            {
                return new(a.Coefficient * b.Coefficient, a.XPower + b.XPower, a.Unknown = b.Unknown);
            }
            else if (a.Unknown == "") // if there is no x term
            {
                return new(a.Coefficient * b.Coefficient, a.XPower = a.XPower + b.XPower, a.Unknown = b.Unknown);
            }
            else // (a.Unknown != b.Unknown) If the x and y terms are not the same
            {
                return new(a.Coefficient * b.Coefficient, a.XPower = a.XPower, a.Unknown = a.Unknown + b.Unknown);
            }

        }


        public override string ToString()
        {
            string returnAlgebra;

            if (XPower == 0)
            {
                returnAlgebra = $"{Coefficient}";
            }
            else if (XPower == 1)
            {
                returnAlgebra = $"{Coefficient}{Unknown}";
            }
            else
            {
                returnAlgebra = $"{Coefficient}{Unknown}^{XPower}";
            }
            

            return returnAlgebra;
        }
    }

    public class AlgebraExpression
    {
        public List<Algebra> Algebras { get; set; }

        public AlgebraExpression(List<Algebra> algebras)
        {
            Algebras = algebras;
            // Roots = quadraticEquation(this);
        }

        public AlgebraExpression()
        {
            Algebras = this.Algebras;
        }

        public static AlgebraExpression operator *(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.Algebras.Count != 1 && b.Algebras.Count != 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {
                        c.Add(a.Algebras[i] * b.Algebras[j]);
                        Console.WriteLine($"{a.Algebras[i]} * {b.Algebras[j]} = {a.Algebras[i] * b.Algebras[j]}");
                    }
                }
            }
            else if (a.Algebras.Count == 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {

                        c.Add(a.Algebras[i] * b.Algebras[j]);
                    }

                }
            }
            else if (b.Algebras.Count == 1)
            {
                for (int i = 0; i < b.Algebras.Count; i++)
                {
                    for (int j = 0; j < a.Algebras.Count; j++)
                    {

                        c.Add(b.Algebras[i] * a.Algebras[j]);
                    }

                }
            }
            else
            {
                c.Add(a.Algebras[0] * b.Algebras[0]);
            }

            

            return new(c);
        }

        public static AlgebraExpression operator /(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.Algebras.Count != 1 && b.Algebras.Count != 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {

                        c.Add(a.Algebras[i] / b.Algebras[j]);
                    }

                }
            }
            else
            {
                c.Add(a.Algebras[0] * b.Algebras[0]);
            }

            return new(c);
        }

        public static AlgebraExpression operator +(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            /*
            if (a.Algebras.Count != 1 && b.Algebras.Count != 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    c.Add(a.Algebras[i] + b.Algebras[i]);
                }
            }
            */

            if (a.Algebras.Count != 1 && b.Algebras.Count != 1) // If both expressions are not one
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {
                        if (a.Algebras[i].XPower == b.Algebras[j].XPower)
                        {
                            c.Add(a.Algebras[i] + b.Algebras[j]);
                        }
                    }

                }
            }
            else if (a.Algebras.Count == 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {
                        if (a.Algebras[i].XPower == b.Algebras[j].XPower)
                        {
                            c.Add(a.Algebras[i] + b.Algebras[j]);
                        }
                    }

                }
            }
            else if (b.Algebras.Count == 1)
            {
                for (int i = 0; i < b.Algebras.Count; i++)
                {
                    for (int j = 0; j < a.Algebras.Count; j++)
                    {
                        if (b.Algebras[i].XPower == a.Algebras[j].XPower)
                        {
                            c.Add(b.Algebras[i] + a.Algebras[j]);
                        }
                        else
                        {
                            c.Add(a.Algebras[j]);
                        }
                    }

                }
            }
            else
            {
                c.Add(a.Algebras[0] + b.Algebras[0]);
            }

            if (c.Count == 0) // unlike terms
            {
                for (int i = 0; i < b.Algebras.Count; i++)
                {
                    c.Add(b.Algebras[i]);
                }
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    c.Add(a.Algebras[i]);
                }
            }


            return new(c);
        }

        public static AlgebraExpression operator -(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.Algebras.Count != 1 && b.Algebras.Count != 1) // If both expressions are not one
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {
                        if (a.Algebras[i].XPower == b.Algebras[j].XPower)
                        {
                            c.Add(a.Algebras[i] - b.Algebras[j]);
                        }
                    }

                }
            } 
            else if (a.Algebras.Count == 1)
            {
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    for (int j = 0; j < b.Algebras.Count; j++)
                    {
                        if (a.Algebras[i].XPower == b.Algebras[j].XPower)
                        {
                            c.Add(a.Algebras[i] - b.Algebras[j]);
                        }
                    }

                }
            }
            else if (b.Algebras.Count == 1)
            {
                for (int i = 0; i < b.Algebras.Count; i++)
                {
                    for (int j = 0; j < a.Algebras.Count; j++)
                    {
                        if (b.Algebras[i].XPower == a.Algebras[j].XPower)
                        {
                            c.Add(b.Algebras[i] - a.Algebras[j]);
                        }
                        else
                        {
                            c.Add(a.Algebras[j]);
                        }
                    }

                }
            }
            else
            {
                c.Add(a.Algebras[0] - b.Algebras[0]);
            }

            if (c.Count == 0) // unlike terms
            {
                for (int i = 0; i < b.Algebras.Count; i++)
                {
                    c.Add(b.Algebras[i]);
                }
                for (int i = 0; i < a.Algebras.Count; i++)
                {
                    c.Add(a.Algebras[i]);
                }
            }
            

            return new(c);
        }


        public AlgebraExpression collectingLikeTerms() // merges like terms
        {
            List<Algebra> nope = new List<Algebra>() { };
            AlgebraExpression c = new AlgebraExpression(nope) { };
            AlgebraExpression d = new AlgebraExpression(Algebras);
            List<double> merge = new List<double>();

            for (var i = d.Algebras.Count - 1; i > -1; i--)
            {
                if (!merge.Contains(Algebras[i].XPower))
                {
                    // Console.WriteLine($"Bah: {Algebras[i].ToString()}");
                    merge.Add(Algebras[i].XPower);
                    c.Algebras.Add(Algebras[i]);
                    d.Algebras.RemoveAt(i);
                }
            }

            // Console.WriteLine(d.Algebras.Count);

            for (var i = 0; i < c.Algebras.Count; i++)
            {
                for (var j = 0; j < d.Algebras.Count; j++)
                {
                    if (c.Algebras[i].XPower == d.Algebras[j].XPower)
                    {
                        // Console.WriteLine($"{c.Algebras[i]} + {d.Algebras[j]} = {c.Algebras[i] + d.Algebras[j]}");
                        c.Algebras[i] += d.Algebras[j];
                    }
                }
                // Console.WriteLine(c[i]);
            }

            // Console.WriteLine($"C= {c.ToString()}");
            return c;
        }

        public AlgebraExpression Sort()
        {
            List<Algebra> sorted = Algebras.OrderByDescending(x => x.XPower).ToList<Algebra>();
            AlgebraExpression sortedAl = new AlgebraExpression(sorted);
            return sortedAl;
        }


        private double[] quadraticEquation(AlgebraExpression eq) //ax^2+bx+c
        {
            double a = eq.Algebras[0].Coefficient;
            double b = eq.Algebras[1].Coefficient;
            double c = eq.Algebras[2].Coefficient;

            double discriminant = Math.Pow(b, 2) - 4 * a * c;
            double[] final = new double[2];


            final[0] = -b + Math.Sqrt(discriminant);
            final[1] = -b - Math.Sqrt(discriminant);

            return final;
        }

        public override string ToString()
        {
            string returnAlgebra = $"{Algebras[0].ToString()}";

            for (int i = 1; i < Algebras.Count; i++)
            {
                string currentAL = Algebras[i].ToString();

                if (currentAL.Contains('-')) // negative
                {
                    returnAlgebra = $"{returnAlgebra} - {Algebras[i].ToString().Remove(0, 1)}";
                }
                else
                {
                    returnAlgebra = $"{returnAlgebra} + {Algebras[i].ToString()}";
                }
            }


            return returnAlgebra;
        }

        


        
        public double Substitute(double sub)
        {
            double result = 0;

            for (int i = 0; i < Algebras.Count; i++)
            {
                double hold = 0;
                hold += Math.Pow(sub,Algebras[i].XPower);

                if (Algebras[i].Coefficient != 0)
                {
                    hold *= Algebras[i].Coefficient;
                }
                

                result += hold;

                Console.WriteLine(result);
            }

            return result;
        }

        public AlgebraExpression ToAlgebra(string input) // converts text into written algerbra
        {
            char[] unknowns = new char[] { 'x', 'y' };

            List<string> stringArray = input.Split(' ').ToList<string>();
            List<Algebra> Algebras = new List<Algebra>();

            for (int i = 0; i < stringArray.Count; i++)
            {
                if (stringArray[i].Contains('-'))
                {
                    stringArray[i] = $"{stringArray[i]}";
                    // not needed
                    // stringArray.RemoveAt(i);
                }
                
            }

            for (int i = 0; i < stringArray.Count; i++)
            {
                Algebra toAdd = new Algebra();
                toAdd.Unknown = "x";

                stringArray[i] = stringArray[i].TrimStart('+'); // removes plus sign
                
                
                
                if (stringArray[i].Contains("x^"))
                {
                    string[] local = stringArray[i].Split($"{unknowns[0]}^");
                    toAdd.Coefficient = Convert.ToDouble(local[0]);
                    toAdd.XPower = Convert.ToDouble(local[1]);
                }
                else if (stringArray[i].Any(char.IsDigit) != true && stringArray[i].Contains("x")) // coeffecients
                {
                    string[] local = stringArray[i].Split($"{unknowns[0]}");
                    toAdd.Coefficient = 1;
                    toAdd.Unknown = $"{unknowns[0]}";
                    toAdd.XPower = 1;
                }
                else if (stringArray[i].Contains("x"))
                {
                    string[] local = stringArray[i].Split($"{unknowns[0]}");
                    toAdd.Coefficient = Convert.ToDouble(local[0]);
                    toAdd.Unknown = $"{unknowns[0]}";
                    toAdd.XPower = 1;
                }
                else
                {
                    toAdd.Coefficient = Convert.ToDouble(stringArray[i]);
                    toAdd.XPower = 0;
                    toAdd.Unknown = "";
                }
                Algebras.Add(toAdd);
            }

            return new AlgebraExpression(Algebras);
        }
    }


    public class AlgebraEquation
    {
        static Random random = new Random();

        private static Algebra OneGen(int max, double xP)
        {

            Algebra algebra = new Algebra(random.Next(1, max + 20), xP, "x");

            return algebra;
        }

        public AlgebraExpression QuadraticGen(int maxSize, int degree)
        {
            List<Algebra> al = new List<Algebra>();

            for (int i = 0; i <= degree; i++)
            {
                Algebra bigal = OneGen(maxSize, i);
                al.Add(bigal);
            }
            AlgebraExpression algs = new AlgebraExpression(al);

            return algs.Sort();
        }
    }

    public class Matrix
    {
        public double[,]? MatrixInp { get; set; }

        public Matrix(double[,] matrixInp)
        {
            MatrixInp = matrixInp;
        }

        public Matrix()
        {
            MatrixInp = null;
        }

        public static Matrix operator +(Matrix a, Matrix b) // addition
        {
            if (a.MatrixInp.GetLength(0) != b.MatrixInp.GetLength(0) || a.MatrixInp.GetLength(1) != b.MatrixInp.GetLength(1))
            {
                throw new ArgumentException("You can't add matrixes of different dimentions");
            }

            double[,] c = new double[a.MatrixInp.GetLength(0), b.MatrixInp.GetLength(1)];

            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.MatrixInp.GetLength(1); j++)
                {
                    c[i, j] = a.MatrixInp[i, j] + b.MatrixInp[i, j];
                }
            }

            return new Matrix(c);
        }

        public static Matrix operator -(Matrix a, Matrix b) // addition
        {
            if (a.MatrixInp.GetLength(0) != b.MatrixInp.GetLength(0) || a.MatrixInp.GetLength(1) != b.MatrixInp.GetLength(1))
            {
                throw new ArgumentException("You can't add matrixes of different dimentions");
            }

            double[,] c = new double[a.MatrixInp.GetLength(0), b.MatrixInp.GetLength(1)];

            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.MatrixInp.GetLength(1); j++)
                {
                    c[i, j] = a.MatrixInp[i, j] - b.MatrixInp[i, j];
                }
            }

            return new Matrix(c);
        }

        public static Matrix operator *(Matrix a, Matrix b) // mutiplication
        {
            double[,] c = new double[a.MatrixInp.GetLength(0), b.MatrixInp.GetLength(1)];

            // Console.WriteLine($"A: {a.MatrixInp.GetLength(0)} B: {b.MatrixInp.GetLength(1)}");

            if (a.MatrixInp.Rank != b.MatrixInp.GetLength(0))
            {
                throw new ArgumentException("Can't mutiply matrixes with a different length");
            }

            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                // puts matrix A's collum into an array
                double[] row = matrixMathRow(a, i);

                for (int j = 0; j <= row.Length; j++)
                {
                    double valAdd = 0;

                    double[] col = matrixMathCol(b, j);

                    for (int k = 0; k < col.Length; k++)
                    {

                        // Console.WriteLine($"Value: {valAdd} + {row[k]} * {col[k]} = {valAdd + row[k] * col[k]}");

                        valAdd = valAdd + (row[k] * col[k]);

                    }

                    c[i, j] = valAdd;
                }
            }

            return new Matrix(c);
        }



        public static Matrix operator *(Matrix a, double b) // scalar mutiplication
        {
            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.MatrixInp.GetLength(1); j++)
                {
                    a.MatrixInp[i, j] = a.MatrixInp[i, j] * b;
                }
            }

            return a;
        }

       
        private static double[] matrixMathRow(Matrix a, int dimension) // gets full row
        {
            double[] returnRow = new double[a.MatrixInp.GetLength(1)];

            for (int i = 0; i < returnRow.Length; i++)
            {
                returnRow[i] = a.MatrixInp[dimension, i];
            }

            return returnRow;
        }


        // puts the current collum into array
        private static double[] matrixMathCol(Matrix a, int dimension) // gets full collumn
        {
            double[] returnCol = new double[a.MatrixInp.GetLength(0)];

            for (int i = 0; i < returnCol.Length; i++)
            {
                returnCol[i] = a.MatrixInp[i, dimension];
            }

            return returnCol;
        }

        public override string ToString()
        {
            string returnString = "";

            for (int i = 0; i < MatrixInp.GetLength(0); i++)
            {
                string row = "";

                for (int j = 0; j < MatrixInp.GetLength(1); j++)
                {
                    row = $"{row} {MatrixInp[i, j]}";
                }

                returnString = $"{returnString} \n {row}";
            }


            return returnString;
        }

        public Matrix matrixInverse()
        {
            // https://jamesmccaffrey.wordpress.com/2020/04/01/computing-the-inverse-of-a-matrix-using-minors-cofactors-and-the-adjugate/
            // refrence guide code

            double det = Determinant();

            double[,] store = Cofactors();

            for (int i = 0; i < store.GetLength(0); i++)
            {
                for (int j = 0; j < store.GetLength(1); j++)
                {
                    store[i, j] /= det;
                }
            }

            return new(store);
        }

        private double[,] Cofactors()
        {
            double[,] coFactor = MatrixInp;
            
            for (int i = 0; i < coFactor.GetLength(0); i++)
            {
                for (int j = 0; j < coFactor.GetLength(1); j++)
                {
                    double[,] sub = subMatrix(i, j);
                    double det = SubDeterminant(sub);

                    if ((i + j) % 2 == 0) // "checkerboard inversion"
                    {
                        coFactor[i, j] = det;
                    }
                    else
                    {
                        coFactor[i, j] = -det;
                    }
                }
            }

            return MatrixInp;
        }

        private double[,] subMatrix(int r, int c)
        {
            // d,f indices of result

            int nr = MatrixInp.GetLength(0);
            int cr = MatrixInp.GetLength(1);

            int di = 0, fi = 0;

            double[,] matrixSub = new double[MatrixInp.GetLength(0)- 1, MatrixInp.GetLength(1)- 1];
            
            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < cr; j++)
                {
                    if (i == r || j == c) continue;
                    matrixSub[di,fi] = MatrixInp[i,j];
                    Console.WriteLine( matrixSub[di,fi]);
                    fi++;
                    
                    if (fi == cr - 1) // end of row
                    {
                        di++;
                        fi = 0;
                    }
                }
            }
            return matrixSub;
        }


        // Current implementation: https://en.wikipedia.org/wiki/Bareiss_algorithm

        public double Determinant() 
        {
            double[,] det = new double[MatrixInp.GetLength(0), MatrixInp.GetLength(1)];
            det = (double[,])MatrixInp.Clone();

            // double det = 0;
            int n = det.GetLength(0) -1;

            // Console.WriteLine($"n = {n}");

            for (int k = 0; k < n; k++)
            {
                for (int i = k+1; i <= n; i++)
                {
                    for (int j = k+1; j <= n; j++)
                    {
                        det[i, j] = ( (det[i, j]* det[k, k]) - (det[i, k] * det[k, j]) )/ (k == 0 ? 1 : det[k - 1, k - 1]);
                    }
                }
            }

            return det[n, n];

        }

        private double SubDeterminant(double[,] sub) // for sub matrixes
        {
            double[,] det = (double[,])sub.Clone();

            int n = det.GetLength(0)-1;

           
            for (int k = 0; k < n; k++)
            {
                for (int i = k + 1; i <= n; i++)
                {
                    for (int j = k + 1; j <= n; j++)
                    {
                        det[i, j] = ((det[i, j] * det[k, k]) - (det[i, k] * det[k, j])) / (k == 0 ? 1 : det[k - 1, k - 1]);
                    }
                }
            }

            return det[n, n+1];
        }



        

        public Matrix RandomMatrix(int row, int col, int maxNum)
        {
            Random rand = new Random();
            double[,] matrixReturn = new double[row, col];


            for (int i = 0; i < matrixReturn.GetLength(0); i++)
            {
                for (int j = 0; j < matrixReturn.GetLength(1); j++)
                {
                    matrixReturn[i, j] = rand.Next(1, maxNum);
                }
            }

            return new Matrix(matrixReturn);
        }
    }

    // Algorithm: https://en.wikipedia.org/wiki/Stern%E2%80%93Brocot_tree
    public class Fraction
    {
        // public double ToConvert { get; set; }
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public Fraction()
        {
            Numerator = 0;
            Denominator = 0;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            if (a.Denominator != b.Denominator)
            {
                // throw new ArgumentException("You can't add fractions of different denominators");

                Fraction[] cF = commmonFractions(a,b);
                a = cF[0];
                b = cF[0];
            }

            a.Numerator = a.Numerator + b.Numerator;
            a.Denominator = a.Denominator + b.Denominator;

            return a;   
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            if (a.Denominator != b.Denominator)
            {
                throw new ArgumentException("You can't subtract fractions of different denominators");
            }

            a.Numerator = a.Numerator - b.Numerator;
            a.Denominator = a.Denominator + b.Denominator;

            return a;
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            a.Numerator = a.Numerator * b.Numerator;
            a.Denominator = a.Denominator * b.Denominator;
            return a;
        }

        public static Fraction operator *(Fraction a, int b)
        {
            a.Numerator = a.Numerator * b;
            a.Denominator = a.Denominator * b;
            return a;
        }


        public static Fraction operator /(Fraction a, Fraction b)
        {
            a.Numerator = a.Numerator * b.Denominator;
            a.Denominator = a.Denominator * b.Numerator;

            return a;
        }

        // comparison

        public static bool operator <(Fraction a, Fraction b)
        {
            return a.toDouble() < b.toDouble();
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return a.toDouble() > b.toDouble();
        }

        public static bool operator <=(Fraction a, Fraction b)
        {
            return a.toDouble() <= b.toDouble();
        }

        public static bool operator >=(Fraction a, Fraction b)
        {
            return a.toDouble() >= b.toDouble();
        }

        public static bool operator == (Fraction a, Fraction b)
        {
            return a.toDouble() == b.toDouble();
        }


        public static bool operator != (Fraction a, Fraction b)
        {
            return a.toDouble() > b.toDouble();
        }

        public override string ToString()
        {
            string returnS = $"{Numerator}/{Denominator}";

            return returnS;
        }


        public double toDouble()
        {
            double n = Convert.ToDouble(Numerator);
            double d = Convert.ToDouble(Denominator);

            return n / d;
        }


        private static Fraction[] commmonFractions(Fraction a, Fraction b)
        {
            int LCM = LowestCommonMultiple(a.Denominator, b.Denominator);

            a = new Fraction(a.Numerator * (LCM / a.Denominator), LCM);
            b = new Fraction(b.Numerator * (LCM / b.Denominator), LCM);

            Fraction[] toReturn = new Fraction[2] { a,b};
            return toReturn;
        }


        // https://en.wikipedia.org/wiki/Least_common_multiple
        private static int LowestCommonMultiple(int a, int b)
        {
           return (int)(Math.Abs(a * b)/GreatestCommonDivisor(a,b));
        }

        // https://en.wikipedia.org/wiki/Greatest_common_divisor
        private static int GreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                int store = b;
                b = a % b;
                a = store;
            }
            return a;
        }


        // Python Implementation: https://stackoverflow.com/questions/5124743/algorithm-for-simplifying-decimal-to-fractions
        // C# Implementation: https://stackoverflow.com/questions/5124743/algorithm-for-simplifying-decimal-to-fractions/32903747#32903747
        // Algorithm: https://en.wikipedia.org/wiki/Stern%E2%80%93Brocot_tree


        public Fraction doubleToFraction(double toConvert, double errorRate)
        {
            int sign = Math.Sign(toConvert);
            
            if (sign == -1)
            {
                toConvert = Math.Abs(toConvert);
            }
            
            double MaxErrorRate = sign == 0 ? errorRate : toConvert * errorRate;

            int n = (int)Math.Floor(toConvert);
            toConvert -= n;

            if (toConvert < MaxErrorRate)
            {
                return new Fraction(sign * n, 1);
            }
            if (1 - MaxErrorRate < toConvert)
            {
                return new Fraction(sign * (n + 1), 1);
            }
            

            Fraction H = new Fraction(1, 1); // UPPER
            Fraction L = new Fraction(0, 1); // LOWER

            
            while (true)
            {
                // Mediant - M
                Fraction M = new Fraction(L.Numerator+H.Numerator, L.Denominator+H.Denominator);

                // Console.WriteLine(M);

                if (M.Denominator * (toConvert + MaxErrorRate) < M.Numerator) // * (toConvert + MaxErrorRate) 
                {
                    H = M;
                }
                else if (M.Numerator < (toConvert - MaxErrorRate) * M.Denominator) // (toConvert - MaxErrorRate) * 
                {
                    L = M;
                }
                else
                {
                    // Fraction toReturn = new Fraction((n * (M.Denominator + M.Numerator)) * sign, M.Denominator);
                    return M;
                }
            }

        }
    }
}

