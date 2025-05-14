namespace Project2
{
    public class MyTuple
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }

        public MyTuple(int v1, int v2)
        {
            Value1 = v1;
            Value2 = v2;
        }

        public override string ToString()
        {
            return $"{Value1}, {Value2}";
        }
    }
}
