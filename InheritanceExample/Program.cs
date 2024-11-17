using System;

namespace InheritanceExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TripleACalls();
            UniqueCalls();
            InheritanceCalls();
            Console.Read();
        }
        static void TripleACalls()
        {
            A aa = new A();
            A ab = new B();
            A ac = new C();
            aa.Method();
            ab.Method();
            ac.Method();
            Console.WriteLine("------------------------------");
        }
        static void UniqueCalls()
        {
            A aa = new A();
            B bb = new B();
            C cc = new C();
            aa.Method();
            bb.Method();
            cc.Method();
            Console.WriteLine("------------------------------");
        }
        static void InheritanceCalls()
        {
            A a = new B();
            B b = new B();
            B b2 = new C();
            A c = new C();
            a.Method();
            b.Method();
            b2.Method(); // because of override option
            c.Method();
            Console.WriteLine("------------------------------");
        }
    }
    public class A
    {
        public virtual void Method()
        {
            Console.WriteLine("Class A");
        }
    }
    public class B : A
    {
        public virtual new void Method()
        {
            Console.WriteLine("Class B");
        }
    }
    public class C : B
    {
        public override void Method()
        {
            Console.WriteLine("Class C");
        }
    }
}
