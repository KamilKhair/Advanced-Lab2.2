namespace GenericApp
{
    public struct MyString
    {
        private readonly string _value;

        public MyString(string value)
        {
            _value = value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}