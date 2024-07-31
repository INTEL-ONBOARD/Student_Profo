﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace stu_profo.Model
{
    class Node
    {
        public dataModel Data { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }

        public Node(dataModel data)
        {
            Data = data;
            Next = null;
            Prev = null;    
        }
    }

    class customLinkedList
    {

        private Node head;
        private Node tail;

        public customLinkedList()
        {
            head = null;
            tail = null;
        }

        // Method to insert a new node at the end
        public void Insert(dataModel data)
        {
            Node newNode = new Node(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }

        // Method to display the doubly linked list in forward direction
        public List<dataModel> DisplayForward()
        {
            List<dataModel> list = new List<dataModel>();
            if (head == null)
            {
                System.Diagnostics.Debug.WriteLine("DataModel is empty"); return null;
            }

            Node temp = head;
            while (temp != null)
            {
                System.Diagnostics.Debug.WriteLine(temp.Data + " ");
                if(temp.Next != null) {list.Add(temp.Next.Data);}
                temp = temp.Next;
            }
            return list;

        }

        // Method to display the doubly linked list in backward direction
        public void DisplayBackward()
        {
            if (tail == null)
            {
                Console.WriteLine("dataModel lsit is empty.");
                return;
            }

            Node temp = tail;
            while (temp != null)
            {
                Console.Write(temp.Data + " ");
                temp = temp.Prev;
            }

            Console.WriteLine();
        }

        // Method to delete a node with a specific value
        public void Delete(dataModel data)
        {
            if (head == null)
                return;

            Node temp = head;

            // If head node itself holds the key to be deleted
            if (head.Data == data)
            {
                head = head.Next;
                if (head != null)
                    head.Prev = null;
                else
                    tail = null;
                return;
            }

            // Search for the key to be deleted
            while (temp != null && temp.Data != data)
            {
                temp = temp.Next;
            }

            // If the key was not found
            if (temp == null)
            {
                Console.WriteLine("Node with dataModel {0} not found.", data);
                return;
            }

            // If node to be deleted is the tail node
            if (temp == tail)
            {
                tail = tail.Prev;
                tail.Next = null;
            }
            else
            {
                temp.Prev.Next = temp.Next;
                temp.Next.Prev = temp.Prev;
            }
        }
    } 
}
