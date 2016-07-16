namespace GenericApp
{
    public struct MyString
    {
        public string Value { get; }

        public MyString(string d)
        {
            Value = d;
        }

        public static implicit operator MyString(string d)
        {
            return new MyString(d);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}