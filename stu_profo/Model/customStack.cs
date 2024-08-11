using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_profo.Model
{
    class CustomStack<T>
    {
        private List<T> _stack; 


        public CustomStack()
        {
            _stack = new List<T>();
        }

        public void Push(T item)
        {
            _stack.Add(item);
        }

        public T Pop()
        {
            if (_stack.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            T item = _stack[_stack.Count - 1];
            _stack.RemoveAt(_stack.Count - 1);  
            return item;
        }

        public T Peek()
        {
            if (_stack.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            return _stack[_stack.Count - 1];
        }

        public bool IsEmpty()
        {
            return _stack.Count == 0;
        }

        public int Count()
        {
            return _stack.Count;
        }

        public void Clear()
        {
            _stack.Clear();
        }
    }
}