using System;
class Program
{
    public static void Main()
    {
        Sample s = new Sample();
        s.Print();

        ISample i = s;
        i.Print();
    }

    public interface ISample
    {
        void Print(string val = "Interface Executed");
    }

    public class Sample : ISample
    {
        public void Print(string val = "Class Executed")
        {
            Console.WriteLine(val);
        }
    }
}