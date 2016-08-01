namespace GenericApp
{
    [Key]
    public struct MyInt
    {
        private readonly int _value;

        public MyInt(int value)
        {
            _value = value;
        }

        public static implicit operator MyInt(int value)
        {
            return new MyInt(value);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}