using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IteratorExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateAndViewCar();
            Console.ReadLine();
        }
        static void CreateAndViewCar()
        {
            var details = new BaseDetail[] {
                new BaseDetail { Name = "Front right vehicle" },
                new BaseDetail { Name = "Front left vehicle" },
                new BaseDetail { Name = "Back right vehicle" },
                new BaseDetail { Name = "Back left vehicle" }
            };
            var car = new Car(details);
            foreach (var detail in car)
            {
                Console.WriteLine(detail.ToString());
            }
        }
    }
    public class Car : IEnumerator<BaseDetail>
    {
        private BaseDetail[] details;
        private int currentIndex;
        private BaseDetail currentDetail;

        public Car(BaseDetail[] list)
        {
            currentDetail = list.FirstOrDefault();
            details = list;
            currentIndex = -1;
        }
        public BaseDetail Current => currentDetail;

        object IEnumerator.Current => currentDetail;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (++currentIndex >= details.Count())
            {
                return false;
            }
            currentDetail = details[currentIndex];
            return true;
        }
        public void Reset()
        {
            currentIndex = -1;
        }
        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
    public class BaseDetail
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return "Detail name: " + Name + "\t\n";
        }
    }
}
