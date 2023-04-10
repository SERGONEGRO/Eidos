namespace MyLib
{
    public class Client<T> : Person
    {
        public T Id { get; set; }

        public bool IsVIP { get; set; }
    }
}
