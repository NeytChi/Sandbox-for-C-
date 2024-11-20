using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomListExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tests = new CustomListTests();
            tests.CreateAndGetByIndex_ReturnNewCustomList();
            tests.ContainsValue_WhenIsTrue_ReturnTrue();
            tests.ContainsValue_WhenIsFalse_ReturnFalse();
            tests.AddNewValue_WhenListIsFull_ReturnList();
            tests.AddNewValue_WhenListIsNotFull_ReturnList();
            tests.CopyTo_WhenIndexZero_ReturnList();
            tests.CopyTo_WhenLastIndex_ReturnList();
            tests.Remove_WhenElementExist_ReturnTrue();
            tests.Remove_WhenElementNotExist_ReturnFalse();
            tests.RemoveAt_ByIndex_Return();
            Console.Read();
            // var firstValue = customList[0];
            // customList.ChangeSizeTo(5);
            // customList.Add(28);
            // customList.Remove(0);
            // customList.ToList();
        }
    }
    public class CustomListTests
    {
        public void CreateAndGetByIndex_ReturnNewCustomList()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            var result = customList[0];
            Console.WriteLine($"Create new custom list and get first value from it -> {result}");
        }
        public void ContainsValue_WhenIsTrue_ReturnTrue()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            var result = customList.Contains(2);
            Console.WriteLine($"Does custom list contain value 2 ->{result}");
        }
        public void ContainsValue_WhenIsFalse_ReturnFalse()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            var result = customList.Contains(4);
            Console.WriteLine($"Does custom list contain value 4 ->{result}");
        }
        public void AddNewValue_WhenListIsFull_ReturnList()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            customList.Add(4);
            Console.WriteLine($"Check if full custom list is adding new value, length ->{customList.Count()}");
        }
        public void AddNewValue_WhenListIsNotFull_ReturnList()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            customList.Add(4);
            customList.Add(5);
            Console.WriteLine($"Check if not full custom list is adding new value, length ->{customList.Count()}");
        }
        public void CopyTo_WhenIndexZero_ReturnList()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            var customCopy = new int[] { 4, 5, 6 };
            Console.WriteLine("Our custom list values: ");
            foreach (var item in customList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("For copy custom list values: ");
            foreach (var item in customCopy)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            customList.CopyTo(customCopy, 0);
            Console.WriteLine("Our custom list after copy: ");
            foreach (var item in customList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
        public void CopyTo_WhenLastIndex_ReturnList()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            var customCopy = new int[] { 4, 5, 6 };
            Console.WriteLine("Our custom list values: ");
            foreach (var item in customList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("For copy custom list values: ");
            foreach (var item in customCopy)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            customList.CopyTo(customCopy, 3);
            Console.WriteLine("Our custom list after copy: ");
            foreach (var item in customList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
        public void Remove_WhenElementExist_ReturnTrue()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            Console.WriteLine($"Custom list length before -> {customList.Count()}");
            var result = customList.Remove(1);
            Console.WriteLine($"Remove element that exists in custom list -> {result}");
            Console.WriteLine($"Custom list length after -> {customList.Count()}");
        }
        public void Remove_WhenElementNotExist_ReturnFalse()
        {
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            Console.WriteLine($"Custom list length before -> {customList.Count()}");
            var result = customList.Remove(4);
            Console.WriteLine($"Remove element that exists in custom list -> {result}");
            Console.WriteLine($"Custom list length after -> {customList.Count()}");
        }
        public void RemoveAt_ByIndex_Return()
        {
            var customList = new CustomList<string>(new string[] { "1", "2", "3" });
            Console.WriteLine($"Custom list length before -> {customList.Count()}");
            customList.RemoveAt(3);
            Console.WriteLine($"Remove element by index in custom list.");
            Console.WriteLine($"Custom list length after -> {customList.Count()}");
        }
    }
    public class CustomList<T> : IEnumerable<T>, ICollection<T>, IList<T>
    {
        private T[] innerArray;
        private bool readOnly = false;
        private int counter = 0;

        public CustomList(IEnumerable<T> values)
        {
            innerArray = new T[values.Count()];
            foreach (T value in values)
            {
                Add(value);
            }
        }
        public T this[int index] 
        { 
            get => innerArray[index]; 
            set => innerArray[index] = value; 
        }

        public int Count => counter;

        public bool IsReadOnly { set => readOnly = value; get => readOnly; }

        public void Add(T item)
        {
            if (readOnly)
            {
                return;
            }
            if (counter == innerArray.Length) 
            { 
                Resize(); 
            }
            innerArray[counter] = item;
            counter++;
        }
        private void Resize() 
        { 
            T[] newArray = new T[innerArray.Length * 2];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = innerArray[i];
            }
            innerArray = newArray;
        }
        private void Resize(int length)
        {
            T[] newArray = new T[length];

            for (int i = 0; i < Count; i++)
            {
                newArray[i] = innerArray[i];
            }
            innerArray = newArray;
        }
        public void Clear()
        {
            var count = innerArray.Length;
            innerArray = new T[count];
        }
        public bool Contains(T item)
        {
            foreach (var i in innerArray)
            {
                if (i.Equals(item)) return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (readOnly)
            {
                return;
            }
            if (arrayIndex > innerArray.Length)
            {  
                throw new IndexOutOfRangeException(); 
            }
            var resizeFor = array.Length + arrayIndex;
            if (innerArray.Length < resizeFor)
            {
                Resize(resizeFor);
            }
            for (int i = 0; i < array.Length; i++)
            {
                innerArray[arrayIndex++] = array[i];
            }
            counter = resizeFor;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < counter; i++) 
            { 
                yield return innerArray[i]; 
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < innerArray.Length; i++)
            {
                if (!innerArray[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > innerArray.Length)
            {
                throw new IndexOutOfRangeException();
            }
            innerArray[index] = item;
        }
        public bool Remove(T item)
        {
            for (int i = 0; i < innerArray.Length; i++)
            {
                if (innerArray[i].Equals(item))
                {
                    var newInnerArray = new T[innerArray.Length - 1];
                    for (int j = 0; j < i - 1; j++)
                    {
                        newInnerArray[j] = innerArray[j];
                    }
                    for (int j = i; j < innerArray.Length - 1; j++)
                    {
                        newInnerArray[j] = innerArray[j + 1];
                    }
                    counter--;
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            var newInnerArray = new T[innerArray.Length - 1];
            for (int i = 0; i < index - 1; i++)
            {
                newInnerArray[i] = innerArray[i];
            }
            for (int i = index; i < index - 1; i++)
            {
                newInnerArray[i] = innerArray[i + 1];
            }
            counter--;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
