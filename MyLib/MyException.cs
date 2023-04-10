using System;

namespace MyLib
{
    public class MyException : Exception
    {
        public MyException(string Msg, int Code) : base(Msg)
        {
            ErrorCode = Code;
        }
        public int ErrorCode { get; set; }
    }
}
