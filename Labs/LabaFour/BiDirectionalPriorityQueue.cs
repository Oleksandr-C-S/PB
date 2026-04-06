using System;
using System.Collections.Generic;
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
}
}