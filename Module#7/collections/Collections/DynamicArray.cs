using System;
using System.Collections;
using System.Collections.Generic;
using Collections.Interfaces;

namespace Collections
{
	public class DynamicArray<T> : IDynamicArray<T>
	{
		T[] array;
		int Len = 0;
		int Cap = 0;

		public DynamicArray()
		{
			array = new T[8];
			Cap = 8;
		}

		public DynamicArray(int capacity)
		{
			array = new T[capacity];
			Cap = capacity;
		}

		public DynamicArray(IEnumerable<T> items)
		{
            
            int i = 0;
            foreach (T el in items)
            {
                i++;
            }
            array = new T[i];

			Cap = i;
            i = 0;

            foreach (T el in items)
            {
                array[i] = el;
                i++;
				Len++;
            }
        }

        public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < array.Length; i++)
			{
				yield return array[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public T this[int index]
		{
			get
			{
				if (Len <= index)
					throw new IndexOutOfRangeException("Array out of range");

				return array[index];
			}
			
		}

		public int Length
		{ 
			get
			{
				return Len;
			} 
		}
		public int Capacity 
		{
            get
            {
				return Cap;
			}
		}

		public void Add(T item)
		{
			if (Len == Cap)
            {
                if (Cap == 0)
                {
                    Cap = 1;
                }

                CreateNewMass(2);
                array[Cap - 1] = item;
                Len++;
                Cap *= 2;
            }
            else
            {
				array[Cap - 1] = item;
				Len++;
			}

		}

        private void CreateNewMass(int factor)
        {
            T[] newArray = new T[Cap * factor];
            array.CopyTo(newArray, 0);
            array = newArray;
        }

        public void AddRange(IEnumerable<T> items)
		{
			
				int j = 0;
				foreach (T el in items)
				{
					j++;
				}

				while (Cap < (Len + j))
				{
					Cap *= 2;
				}

				CreateNewMass(1);
				foreach (T el in items)
				{
					array[Len] = el;
					Len++;
				}

		}

		public void Insert(T item, int index)
		{
			if (index > Cap || index < 0)
				throw new IndexOutOfRangeException("Array out of range");

			if(Len == Cap)
				Cap *= 2;

			T[] newArray = new T[Cap];

			int k = 0;
            while (k != index)
            {
				newArray[k] = array[k];
				k++;
            }

			newArray[k] = item;
			k++;

			while(k != Len + 1)
            {
				newArray[k] = array[k-1];
				k++;
			}

			array = newArray;
		}

		public bool Remove(T item)
		{
			int i;
			for (i = 0; i < Len; i++)
            {
                if (array[i].Equals(item))
                {
					break;
                }
            }

			if (i == Len)
				return false;

			T[] newArray = new T[Cap];
			int j = 0;
			int arr = 0;
			while(j != Len )
            {
				if (j != i)
				{
					newArray[arr] = array[j];
					arr++;
				}
				j++;
			}
			Len -= 1;
			array = newArray;

			return true;
		}
	}
}