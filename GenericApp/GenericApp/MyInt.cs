namespace GenericApp
{
    [Key]
    public struct MyInt
    {
        public int Value { get;}

        public MyInt(int d)
        {
            Value = d;
        }

        public static implicit operator MyInt(int d)
        {
            return new MyInt(d);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}