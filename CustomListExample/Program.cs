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
            var customList = new CustomList<int>(new int[] { 1, 2, 3 });
            // var firstValue = customList[0];
            // customList.ChangeSizeTo(5);
            // customList.Add(28);
            // customList.Remove(0);
            // customList.ToList();
        }
    }
    public class CustomList<T> : IEnumerable<T>, ICollection<T>, IList<T>
    {
        private T[] innerArray;
        private bool readOnly = false;

        public CustomList(IEnumerable<T> values)
        {
            innerArray = new T[values.Count()];
        }
        public T this[int index] 
        { 
            get => innerArray[index]; 
            set => innerArray[index] = value; 
        }

        public int Count => innerArray.Count();

        public bool IsReadOnly { set => readOnly = value; get => readOnly; }

        public void Add(T item)
        {

            throw new NotImplementedException();
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
            if (arrayIndex > innerArray.Length)
            {  
                throw new IndexOutOfRangeException(); 
            }

            var x = innerArray.Length - arrayIndex;
            var y = array.Length - x;
            var lengthToNewArray = innerArray.Length;
            
            if (y > 0) 
            {
                lengthToNewArray += y;
            }
            var newInnerArray = new T[lengthToNewArray];
            
            for (int i = 0; i < arrayIndex; i++)
            {
                newInnerArray[i] = innerArray[i];
            }    
            for (int i = arrayIndex; i < lengthToNewArray; i++)
            {
                newInnerArray[i] = array[i];
            }
            innerArray = newInnerArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
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
                    var newInnerArray = new T[i];
                    for (int j = 0; j < i - 1; j++)
                    {
                        newInnerArray[j] = innerArray[j];
                    }
                    for (int j = i; j < innerArray.Length - 1; j++)
                    {
                        newInnerArray[j] = innerArray[j + 1];
                    }
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
