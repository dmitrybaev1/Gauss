using System;

namespace Lab1
{
    class Gauss
    {
        Line[] lines;
        Line[] initlines;
        public Gauss(Line[] l)
        {
            lines = l;
            initlines = lines;
        }
 
        bool CheckConditions(int i)
        {
            if (lines[i].GetElement(i) == 0)
               return false;
            else
               return true;
        }
       /* bool CheckConditions()
        {
            bool isZeroLine;
            bool isZeroRow=true;
            decimal d;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    isZeroRow = true;
                    d = lines[j].GetElement(i);
                    if (d != 0)
                    {
                        isZeroRow = false;
                        break;
                    }
                }
                isZeroLine = lines[i].CheckZeroLine();
                if (isZeroLine||isZeroRow)
                    return false;
            }
            return true;
        }*/
        public Line[] GetTriangularMatrix()
        {
            int k = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (CheckConditions(i))
                {
                    k++;
                    decimal[] elements = lines[i].GetElements();
                    for (int j = k; j < lines.Length; j++)
                    {
                        decimal[] currentElements = lines[j].GetElements();
                        lines[j] -= lines[i] * (currentElements[i] / elements[i]);
                    }
                }
                else
                {
                    throw new ArgumentException("Невозможно решить систему данным методом");
                }
            }
            return lines;        
        }
       /* void TrySwap(int index)
        {
            Line buf;
            for (int j = index+1; j < lines.Length; j++)
            {
                if (lines[j].GetElement(index) != 0)
                {
                    buf = lines[index];
                    lines[index] = lines[j];
                    lines[j] = buf;
                    break;
                }
            }
        }
        */
        public decimal[] GetRoots()
        {
            decimal[] x = new decimal[lines.Length];
            for(int i = lines.Length-1; i >= 0; i--)
            {
                decimal current = lines[i].GetElement(i);
                for(int j = lines.Length; j > i; j--)
                {
                    if (j == lines.Length)
                        x[i] = lines[i].GetElement(j) / current;
                    else
                    {
                        x[i] -= lines[i].GetElement(j)*x[j] / current;
                    }
                }
            }
            return x;
        }
        public decimal[] GetInconsistencies()
        {
            decimal[] d = new decimal[initlines.Length];
            decimal[] x = GetRoots();
            for(int i = 0; i < initlines.Length; i++)
            {
                d[i] = 0;
                for(int j = 0; j <= initlines.Length; j++)
                {
                    if (j == initlines.Length)
                        d[i] -= initlines[i].GetElement(initlines.Length);
                    else
                        d[i] += initlines[i].GetElement(j)*x[j];
                }
            }
            return d;
        }
        public decimal GetDeterminant()
        {
            decimal[] array = new decimal[lines.Length + 1];
            decimal determinant=1;
            for(int i = 0; i < lines.Length; i++)
            {
                decimal[] arr = lines[i].GetElements();
                array[i] = arr[i];
            }
            for (int i = 0; i < lines.Length; i++)
                determinant *= array[i];
            return determinant;
        }
        
    }
}
