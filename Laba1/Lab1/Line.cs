

namespace Lab1
{
    class Line
    {
        decimal[] arr;
        public Line(decimal[] l)
        {
            arr = l;
        }
        public static Line operator+(Line a,Line b)
        {
            decimal[] newarr = new decimal[a.arr.Length];
            for (int i = 0; i < a.arr.Length; i++)
            {
                newarr[i] = a.arr[i] + b.arr[i];
            }
                return new Line(newarr);
        }
        public static Line operator -(Line a, Line b)
        {
            decimal[] newarr = new decimal[a.arr.Length];
            for (int i = 0; i < a.arr.Length; i++)
            {
                newarr[i] = a.arr[i] - b.arr[i];
            }
            return new Line(newarr);
        }
        public static Line operator *(Line b, decimal a)
        {
            decimal[] newarr = new decimal[b.arr.Length];
            for (int i = 0; i < b.arr.Length; i++)
            {
                newarr[i] = a * b.arr[i];
            }
            return new Line(newarr);
        }
        public static Line operator *(decimal a, Line b)
        {
            decimal[] newarr = new decimal[b.arr.Length];
            for (int i = 0; i < b.arr.Length; i++)
            {
                newarr[i] = a * b.arr[i];
            }
            return new Line(newarr);
        }
        public override string ToString()
        {
            string s="";
            for (int i = 0; i < arr.Length; i++)
            {
                string g = arr[i].ToString("0.###");
                if (i != 0 && i != (arr.Length - 1))
                    s += " " + g;
                else if(i==(arr.Length-1))
                    s += " | " + g +"";
                else
                    s +=""+ g;
            }
            return s;
        }
        public decimal[] GetElements()
        {
            return arr;
        }
        public decimal GetElement(int i)
        {
              return arr[i];
        }
        public bool CheckZeroLine()
        {
            bool isZero = true;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    isZero = false;
                    break;
                }
            }
            return isZero;
        }
    }
}
