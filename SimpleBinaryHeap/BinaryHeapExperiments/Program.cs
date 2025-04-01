using BinaryHeapLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapExperiments
{
    internal class Program
    {
        static BinaryHeap<int> maxHeap = new BinaryHeap<int>(new MaxHeapComparer<int>()); // куча наименьших элементов
        static BinaryHeap<int> minHeap = new BinaryHeap<int>(new MinHeapComparer<int>()); // куча наибольших элементов
        static void Main(string[] args)
        {
            // задача найти медиану из потока данный по правилу:
            // если количество элементов нечётное - максимальный элемент из кучи наименьших элементов
            // если количество элементов чётное - среднее значение двух максимальных элементов

            var path = "../../inputStream.txt";
            var separators = new char[] { ' ', '\t', '\r', '\n' };
            var inputStream = File.ReadAllText(path).Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(item => int.Parse(item)).ToList();
            
            if (inputStream.Count == 0)
            {
                Console.WriteLine("Входной поток пуст");
                Console.ReadKey();
                Environment.Exit(0);
            }
            
            foreach (var item in inputStream)
            {
                // первый элемент добавляем в maxHeap
                if (maxHeap.Count == 0 && minHeap.Count == 0)
                {
                    maxHeap.Add(item);
                    continue;
                }
                // если элемент <= максимального элемента в maxHeap, то добавляем его в maxHeap
                if (item <= maxHeap.Peek())
                {
                    maxHeap.Add(item);
                }
                // иначе добавляем в minHeap
                else
                {
                    minHeap.Add(item);
                }
                Balance(); // балансировка куч
            }

            // ищем медиану
            double median;
            if (inputStream.Count % 2 == 0)
            {
                median = (maxHeap.Remove() + maxHeap.Remove()) / 2.0;
            }
            else
            {
                median = maxHeap.Remove();
            }

            // печатаем медиану
            Console.WriteLine($"Median is {median}");
        }
        static void Balance()
        {
            // Балансируем по правилу:
            // Если maxHeap содержит больше чем на 1 элемент, чем minHeap, переместить корень из maxHeap в minHeap
            // Если minHeap содержит больше элементов, чем maxHeap, переместить корень из minHeap в maxHeap
            if (maxHeap.Count > minHeap.Count + 1)
            {
                minHeap.Add(maxHeap.Remove());
            }
            if (minHeap.Count > maxHeap.Count)
            {
                maxHeap.Add(minHeap.Remove());
            }
        }
    }
}
