using System;
using System.Collections.Generic;
using PB.Labs.BiDirectionalPriorityQueue;
namespace PB.Labs.BiDirectionalPriorityQueue{
public class BiDirectionalPriorityQueue<T>
{
    
    //Мінімальний пріорітет
    private PriorityQueue<(int id, T item), int> minHeap;

    //Максимальний пріорітет
    private PriorityQueue<(int id, T item), int> maxHeap;

    //Двойний список
    private LinkedList<(int id, T item, int priority)> list;

//Створюю словник для відстеження актуальності
private Dictionary<int, bool> valid;

//Лічильник спеціальних айді
private int counter;
public BiDirectionalPriorityQueue()
        {
            minHeap = new PriorityQueue<(int, T), int>();
            maxHeap = new PriorityQueue<(int, T), int>();
            list = new LinkedList<(int, T, int)>();
            valid = new Dictionary<int, bool>();
            counter = 0;
        }
        //Додавання елементу
        public void Enqueue(T item, int priority)
        {
            int id = counter++;
            minHeap.Enqueue((id, item),  priority);
            maxHeap.Enqueue((id,item), -priority);
            list.AddLast((id, item, priority));
            valid[id] = true;
        }
        //Очищення від неактуальних елементів

        private void CleanMinHeap()
        {
            while (minHeap.Count > 0 && !valid[minHeap.Peek().id])
            minHeap.Dequeue();
        }
        private void CleanMaxHeap()
        {
            while (maxHeap.Count > 0 && !valid[maxHeap.Peek().id])
            maxHeap.Dequeue();
        }
        private void CleanFront()
        {
            while (list.Count > 0 && !valid[list.First.Value.id])
            list.RemoveFirst();
        }
        private void CleanBack()
        {
            while (list.Count >0 && !valid[list.Last.Value.id])
            list.RemoveLast();
        }
public T Dequeue(string mode)
        {
            
        
int id;
T item;
switch (mode.ToLower())
            {
                case "highest":
                CleanMaxHeap();
                if(maxHeap.Count == 0)return default;
                var max = maxHeap.Dequeue();
                id = max.id;
                item = max.item;
                break;

                case "lowest":
                CleanMinHeap();
                if(minHeap.Count == 0)return default;
                var min = minHeap.Dequeue();
                id = min.id;
                item = min.item;
                break;
                
                case "oldest":
                CleanFront();
                if(list.Count == 0)return default;
                var first = list.First.Value();
                list.RemoveFirst();
                id = first.id;
                item = first.item;
                break;
                
                case "newest":
                CleanBack();
                if (list.Count == 0) return default;
                var last = list.Last.Value;
                list.RemoveLast();
                id = last.id;
                item = last.item;
                break;
                default:
                throw new ArgumentException("Неправильний режим");
            
            }
            valid[id] = false;
            return item;
        }

        public T Peek(string mode)
        {
            switch(mode.ToLower()){
            case "highest":
            CleanMaxHeap();
            return maxHeap.Count > 0? maxHeap.Peak().item:default;

             case "lowest":
            CleanMinHeap();
            return minHeap.Count > 0? minHeap.Peak().item:default;


            case "oldest":
            CleanFront();
            return list.Count > 0? list.First.Value().item:default;

            
            case "newest":
            CleanBack();
            return list.Count > 0? list.Peak().item:default;
            default:
            throw new ArgumentException("Неправильний режим");
        }
}
}
}
class Program
{
    static void Main()
    {
        var pq = new BiDirectionalPriorityQueue<string>();
        pq.Enqueue("A", 5);
        pq.Enqueue("B", 1);
        pq.Enqueue("C", 10);

        Console.WriteLine("Високий пріорітет: "+ pq.Peek("highest"));
        Console.WriteLine("Низький пріорітет: "+ pq.Peek("lowest"));
        Console.WriteLine("Старий пріорітет: "+ pq.Peek("oldest"));
        Console.WriteLine("Новий пріорітет: "+ pq.Peek("newest"));
        Console.WriteLine("Видалення (highest): "+ pq.Dequeue("highest"));
        Console.WriteLine("Видалення (oldest): "+ pq.Dequeue("oldest"));
    }
}