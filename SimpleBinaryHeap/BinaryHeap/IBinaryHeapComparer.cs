using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapLib
{
    // Задача восстановления свойств кучи лежит на этих компараторах
    // Для корректной работы необходимо соблюдать порядок передачи параметров: первым потомок, вторым родитель
    // Если компараторы в ходе сравнения вернули:
    // (-1) - нарушено свойство кучи, необходимо восстановление её свойств
    // (0)  - нарушений свойств кучи нет
    // (1)  - нарушений свойств кучи нет

    /// <summary>
    /// Интерфейс адаптер для компараторов Бинарной Кучи
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBinaryHeapComparer<T> : IComparer<T> where T : IComparable<T> { }
    /// <summary>
    /// Компаратор для максимальной Бинарной Кучи
    /// </summary>
    public class MaxHeapComparer<T> : IBinaryHeapComparer<T> where T : IComparable<T>
    {   
        public int Compare(T child, T parent)
        {
            return parent.CompareTo(child);
        }
    }
    /// <summary>
    /// компаратор для минимальной Бинарной Кучи
    /// </summary>
    public class MinHeapComparer<T> : IBinaryHeapComparer<T> where T : IComparable<T>
    {
        public int Compare(T child, T parent)
        {
            return child.CompareTo(parent);
        }
    }
}
