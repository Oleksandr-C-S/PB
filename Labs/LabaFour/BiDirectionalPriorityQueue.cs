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
}
}