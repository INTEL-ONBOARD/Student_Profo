using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace stu_profo.Model
{
    public class CustomQueue<T>
    {
        private T[] _queue;  // Array to store elements
        private int _head;   // Index of the first element
        private int _tail;   // Index of the next insertion point
        private int _size;   // Number of elements in the queue
        private int _capacity; // Capacity of the queue

        public CustomQueue(int capacity )
        {
            _queue = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            _capacity = capacity;
        }

        public void Enqueue(T item)
        {
            if (_size == _capacity)
            {
                throw new InvalidOperationException("The queue is full.");
            }

            _queue[_tail] = item;
            _tail = (_tail + 1) % _capacity;
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new InvalidOperationException("The queue is empty.");

            T value = _queue[_head];
            _queue[_head] = default(T);  
            _head = (_head + 1) % _capacity;
            _size--;
            return value;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException("The queue is empty.");

            return _queue[_head];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public int Count()
        {
            return _size;
        }

        public void Clear()
        {
            _queue = new T[_capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
        }
    }
}

