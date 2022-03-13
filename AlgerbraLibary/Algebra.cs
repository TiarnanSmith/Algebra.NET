using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace AlgebraLibary
{
    public class Algebra
    {
        private double xpower, coefficient;
        private string unknown;

        public double Coefficient
        {
            get { return coefficient; }
            set { coefficient = value; }
        }

        public string Unknown
        {
            get
            {
                Unknown = unknown;
                unknown.ToLower();
                return unknown;
            }
            set { this.unknown = value; }

        }

        public double XPower
        {
            get { return xpower; }
            set { xpower = value; }

        }


        public Algebra(double coefficient, double xpower, string unknown)
        {
            this.Coefficient = coefficient;
            this.XPower = xpower;
            this.Unknown = unknown;
        }

        public Algebra()
        {
            this.Coefficient = coefficient;
            this.XPower = xpower;
            this.Unknown = unknown;
        }

        public static Algebra operator +(Algebra a, Algebra b)
        {
            return a.Unknown == b.Unknown ? (a.xpower == b.xpower ? new(a.Coefficient + b.Coefficient, a.XPower = a.XPower, a.unknown = a.unknown) : null) : null;
        }
        public static Algebra operator -(Algebra a, Algebra b)
        {
            return a.Unknown == b.Unknown ? (a.xpower == b.xpower ? new(a.Coefficient - b.Coefficient, a.XPower = a.XPower, a.unknown = a.unknown) : null) : null;
        }
        public static Algebra operator /(Algebra a, Algebra b)
        {
            if (a.Unknown == b.Unknown)
            {
                return new(a.coefficient * b.coefficient, a.xpower = a.xpower, a.unknown = b.unknown);
            }
            else if (a.unknown == "") // if there is no x term
            {
                return new(a.Coefficient / b.Coefficient, a.XPower = a.XPower, a.unknown = b.unknown);
            }
            else // (a.Unknown != b.Unknown) If the x and y terms are not the same
            {
                return new(a.Coefficient / b.Coefficient, a.XPower = a.XPower, a.unknown = a.unknown + b.unknown);
            }
        }


        public static Algebra operator *(Algebra a, Algebra b)
        {
            if (a.Unknown == b.Unknown)
            {
                return new(a.coefficient * b.coefficient, a.xpower + b.xpower, a.unknown = b.unknown);
            }
            else if (a.unknown == "") // if there is no x term
            {
                return new(a.Coefficient * b.Coefficient, a.XPower = a.XPower + b.XPower, a.unknown = b.unknown);
            }
            else // (a.Unknown != b.Unknown) If the x and y terms are not the same
            {
                return new(a.Coefficient * b.Coefficient, a.XPower = a.XPower, a.unknown = a.unknown + b.unknown);
            }

        }


        public override string ToString()
        {
            string returnAlgebra;

            if (xpower == 0)
            {
                returnAlgebra = $"{coefficient}";
            }
            else if (xpower == 1)
            {
                returnAlgebra = $"{coefficient}{unknown}";
            }
            else
            {
                returnAlgebra = $"{coefficient}{unknown}^{xpower}";
            }

            return returnAlgebra;
        }
    }

    public class AlgebraExpression
    {
        // Nullable values as there is not always roots
        private List<Algebra>? algebras;
        private double[]? roots;
        public double[]? Roots
        {
            get { return roots; }
        }

        public List<Algebra> Algebras
        {
            get { return algebras; }
            set
            {
                this.algebras = value;
                if (algebras.Count == 3)
                {
                    roots = quadraticEquation(this);
                }
                else
                {
                    roots = null;
                }
            }
        }

        public AlgebraExpression(List<Algebra> algebras)
        {
            this.Algebras = algebras;
            // Roots = quadraticEquation(this);
        }
        public AlgebraExpression()
        {
            this.Algebras = algebras;
            // Roots = quadraticEquation(this);
        }

        public static AlgebraExpression operator *(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.algebras.Count != 1 && b.algebras.Count != 1)
            {
                for (int i = 0; i < a.algebras.Count; i++)
                {
                    for (int j = 0; j < b.algebras.Count; j++)
                    {

                        c.Add(a.algebras[i] * b.algebras[j]);
                    }

                }
            }
            else
            {
                c.Add(a.algebras[0] * b.algebras[0]);
            }

            return new(c);
        }

        public static AlgebraExpression operator /(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.algebras.Count != 1 && b.algebras.Count != 1)
            {
                for (int i = 0; i < a.algebras.Count; i++)
                {
                    for (int j = 0; j < b.algebras.Count; j++)
                    {

                        c.Add(a.algebras[i] / b.algebras[j]);
                    }

                }
            }
            else
            {
                c.Add(a.algebras[0] * b.algebras[0]);
            }

            return new(c);
        }

        public static AlgebraExpression operator +(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.algebras.Count != 1 && a.algebras.Count != 1)
            {
                for (int i = 0; i < a.algebras.Count; i++)
                {
                    c.Add(a.algebras[i] + b.algebras[i]);
                }
            }
            return new(c);
        }

        public static AlgebraExpression operator -(AlgebraExpression a, AlgebraExpression b)
        {
            List<Algebra> c = new List<Algebra>();

            if (a.algebras.Count != 1 && a.algebras.Count != 1)
            {
                for (int i = 0; i < a.algebras.Count; i++)
                {
                    c.Add(a.algebras[i] - b.algebras[i]);
                }
            }

            return new(c);
        }


        public AlgebraExpression collectingLikeTerms() // merges like terms
        {
            List<Algebra> nope = new List<Algebra>() { };
            AlgebraExpression c = new AlgebraExpression(nope) { };
            AlgebraExpression d = new AlgebraExpression(algebras);
            List<double> merge = new List<double>();

            for (var i = d.algebras.Count - 1; i > -1; i--)
            {
                if (!merge.Contains(algebras[i].XPower))
                {
                    // Console.WriteLine($"Bah: {algebras[i].ToString()}");
                    merge.Add(algebras[i].XPower);
                    c.algebras.Add(algebras[i]);
                    d.algebras.RemoveAt(i);
                }
            }

            // Console.WriteLine(d.algebras.Count);

            for (var i = 0; i < c.algebras.Count; i++)
            {
                for (var j = 0; j < d.algebras.Count; j++)
                {
                    if (c.algebras[i].XPower == d.algebras[j].XPower)
                    {
                        // Console.WriteLine($"{c.algebras[i]} + {d.algebras[j]} = {c.algebras[i] + d.algebras[j]}");
                        c.algebras[i] += d.algebras[j];
                    }
                }
                // Console.WriteLine(c[i]);
            }

            // Console.WriteLine($"C= {c.ToString()}");
            return c;
        }

        public AlgebraExpression Sort()
        {
            List<Algebra> sorted = algebras.OrderByDescending(x => x.XPower).ToList<Algebra>();
            AlgebraExpression sortedAl = new AlgebraExpression(sorted);
            return sortedAl;
        }


        private double[] quadraticEquation(AlgebraExpression eq) //ax^2+bx+c
        {
            double a = eq.algebras[0].Coefficient;
            double b = eq.algebras[1].Coefficient;
            double c = eq.algebras[2].Coefficient;

            double discriminant = Math.Pow(b, 2) - 4 * a * c;
            double[] final = new double[2];


            final[0] = -b + Math.Sqrt(discriminant);
            final[1] = -b - Math.Sqrt(discriminant);

            return final;
        }

        public override string ToString()
        {
            string returnAlgebra = $"{algebras[0].ToString()}";

            for (int i = 1; i < algebras.Count; i++)
            {
                string currentAL = algebras[i].ToString();

                if (currentAL.Contains('-')) // negative
                {
                    returnAlgebra = $"{returnAlgebra} - {algebras[i].ToString().Remove(0, 1)}";
                }
                else
                {
                    returnAlgebra = $"{returnAlgebra} + {algebras[i].ToString()}";
                }
            }


            return returnAlgebra;
        }


        
        public double substitute(double sub)
        {
            double result = 0;

            for (int i = 0; i < algebras.Count; i++)
            {
                result += Math.Pow(sub,algebras[i].XPower);
                if (algebras[i].Coefficient != 0)
                {
                    result *= algebras[i].Coefficient;
                }
            }

            return result;
        }

        public AlgebraExpression ToAlgebra(string input) // converts text into written algerbra
        {
            char[] unknowns = new char[] { 'x', 'y' };

            List<string> stringArray = input.Split(' ').ToList<string>();
            List<Algebra> algebras = new List<Algebra>();

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
                algebras.Add(toAdd);
            }

            return new AlgebraExpression(algebras);
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
    }

    public class Matrix
    {
        private double[,] matrixInp; private double[] matrixSize;
        public double[,] MatrixInp { get => matrixInp; set => matrixInp = value; }

        public Matrix(double[,] matrixInp)
        {
            MatrixInp = matrixInp;
        }

        public Matrix()
        {
            MatrixInp = matrixInp;
        }

        public static Matrix operator +(Matrix a, Matrix b) // addition
        {
            if (a.MatrixInp.GetLength(0) != b.MatrixInp.GetLength(0) || a.MatrixInp.GetLength(1) != b.MatrixInp.GetLength(1))
            {
                throw new ArgumentException("You can't add matrixes of different dimentions");
            }

            double[,] c = new double[a.matrixInp.GetLength(0), b.matrixInp.GetLength(1)];

            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.MatrixInp.GetLength(1); j++)
                {
                    c[i, j] = a.matrixInp[i, j] + b.matrixInp[i, j];
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

            double[,] c = new double[a.matrixInp.GetLength(0), b.matrixInp.GetLength(1)];

            for (int i = 0; i < a.MatrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.MatrixInp.GetLength(1); j++)
                {
                    c[i, j] = a.matrixInp[i, j] - b.matrixInp[i, j];
                }
            }

            return new Matrix(c);
        }

        public static Matrix operator *(Matrix a, Matrix b) // mutiplication
        {
            double[,] c = new double[a.matrixInp.GetLength(0), b.matrixInp.GetLength(1)];

            // Console.WriteLine($"A: {a.matrixInp.GetLength(0)} B: {b.matrixInp.GetLength(1)}");

            if (a.matrixInp.Rank != b.matrixInp.GetLength(0))
            {
                throw new ArgumentException("Can't mutiply matrixes with a different length");
            }

            for (int i = 0; i < a.matrixInp.GetLength(0); i++)
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
            for (int i = 0; i < a.matrixInp.GetLength(0); i++)
            {
                for (int j = 0; j < a.matrixInp.GetLength(1); j++)
                {
                    a.matrixInp[i, j] = a.matrixInp[i, j] * b;
                }
            }

            return a;
        }

       
        private static double[] matrixMathRow(Matrix a, int dimension) // gets full row
        {
            double[] returnRow = new double[a.matrixInp.GetLength(1)];

            for (int i = 0; i < returnRow.Length; i++)
            {
                returnRow[i] = a.matrixInp[dimension, i];
            }

            return returnRow;
        }


        // puts the current collum into array
        private static double[] matrixMathCol(Matrix a, int dimension) // gets full collumn
        {
            double[] returnCol = new double[a.matrixInp.GetLength(0)];

            for (int i = 0; i < returnCol.Length; i++)
            {
                returnCol[i] = a.matrixInp[i, dimension];
            }

            return returnCol;
        }

        public override string ToString()
        {
            string returnString = "";

            for (int i = 0; i < matrixInp.GetLength(0); i++)
            {
                string row = "";

                for (int j = 0; j < matrixInp.GetLength(1); j++)
                {
                    row = $"{row} {matrixInp[i, j]}";
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
            double[,] coFactor = matrixInp;
            
            for (int i = 0; i < coFactor.GetLength(0); i++)
            {
                for (int j = 0; j < coFactor.GetLength(1); j++)
                {
                    double[,] sub = subMatrix(i, j);
                    double det = Determinant(sub);

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

            return matrixInp;
        }

        private double[,] subMatrix(int r, int c)
        {
            // d,f indices of result

            int nr = matrixInp.GetLength(0);
            int cr = matrixInp.GetLength(1);

            int di = 0, fi = 0;

            double[,] matrixSub = new double[matrixInp.GetLength(0)- 1, matrixInp.GetLength(1)- 1];
            
            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < cr; j++)
                {
                    if (i == r || j == c) continue;
                    matrixSub[di,fi] = matrixInp[i,j];
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

        public double Determinant() // help?? https://stackoverflow.com/questions/5051528/how-to-calculate-matrix-determinant-nn-or-just-55
        {
            // intresting: https://en.wikipedia.org/wiki/Bareiss_algorithm
            // Simlar implementation: https://github.com/borissevo/csharp-bareiss/blob/master/BareissAlgorithm/BareissAlg.cs

            double[,] det = new double[matrixInp.GetLength(0), matrixInp.GetLength(1)];
            det = (double[,])matrixInp.Clone();

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

        private double Determinant(double[,] sub) // for sub matrixes
        {
            

            int n = sub.GetLength(0) - 1;
            double[,] subMatrix = new double[n, n];
            subMatrix = (double[,])sub.Clone();



            for (int k = 0; k < n; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    double kd = (k == 0 ? 1 : subMatrix[k - 1, k - 1]);

                    for (int j = k + 1; j <= n; j++)
                    {
                        double d1 = (subMatrix[i, j] * subMatrix[k, k]);
                        double d2 = (subMatrix[i, k] * subMatrix[k, j]);
                        subMatrix[i, j] = (d1 - d2) / kd;
                    }
                }
            }

            

            return subMatrix[n, n];

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
}


namespace unsimplifedForm
{
    public class surd
    {
        private double radical;
        private double degree;
        private double? simpifedRad;
        public double Radical
        {
            get { return radical; }
            set
            {
                radical = value;
                simpifedRad = radicalSimplify(value)[0];
            }
        }
        public double Degree
        {
            get { return degree; }
            set
            {
                degree = value;
                degree = radicalSimplify(radical)[1];
            }
        }

        public surd(double radical, double degree)
        {
            Radical = radical;
            Degree = degree;
        }



        /*
        private double[] radicalSimplify(double rad)
        {
            int radicalSize = rad.ToString().Length + 20 / 2;

            double[] surdStore = new double[2];
            surdStore[0] = rad;


            for (int i = radicalSize; i > 2; i--)
            {
                double sqrNum = Math.Pow(i, 2);

                if (rad % i == 0 && rad != sqrNum)
                {
                    surdStore[0] = surdStore[0] / sqrNum;
                    surdStore[1] = i;
                    Console.WriteLine(surdStore[0]);
                }
            }
            return surdStore;
        }
        */

        private double[] radicalSimplify(double rad)
        {

            double[] surdStore = new double[2];
            double squareNum = 1; double squardSimp = 1;
            var i = 2;
            var iteration = 0;

            // while loop method needs refining to allow for square numbers > 9
            // unsure how to find a route but not count off until infinity

            while (iteration < 2)
            {
                squareNum = Math.Pow(i, 2);
                squardSimp = surdStore[0] % squareNum;

                if (squardSimp == 0)
                {
                    surdStore[0] = rad / squareNum;
                    surdStore[1] = surdStore[1] + (squareNum / i);
                }
                if (i == 9)
                {
                    break;
                }

                i++;
            }

            return surdStore;
        }


        public override string ToString()
        {
            string returnString = $"{degree}/{radical}";

            return returnString;
        }
    }

    public class surdFraction
    {
        private dynamic fraction, surd;
        public dynamic Fraction
        {
            get { return fraction; }
            set { fraction = value; }
        }
        public dynamic Surd { get; set; }

        public surdFraction(dynamic fraction, dynamic surd)
        {
            this.Fraction = fraction;
            this.Surd = surd;
        }

        private surdFraction surdCreate()
        {
            return new surdFraction(this.Fraction, this.Surd);
        }
    }
}

