using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeapLib
{
    public class BinaryHeap<T> where T : IComparable<T>
    {
        #region Fields

        /// <summary>
        /// Список находящихся в Куче узлов
        /// </summary>
        public List<T> _nodes;
        /// <summary>
        /// Компаратор Кучи
        /// </summary>
        private IBinaryHeapComparer<T> _comparer;

        #endregion

        #region Properties

        /// <summary>
        /// Количество находящихся в Куче узлов
        /// </summary>
        public int Count
        {
            get
            {
                return _nodes.Count;
            }
        }

        #endregion

        #region Constructors

        public BinaryHeap() : this(new MaxHeapComparer<T>()) { }

        public BinaryHeap(IBinaryHeapComparer<T> comparer)
        {
            _nodes = new List<T>();
            _comparer = comparer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Добавление элемента в кучу
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Add(T item)
        {
            if (item == null)
            {
                return;
            }
            
            // добавляем элемент в самый конец
            _nodes.Add(item);
            var itemIndex = _nodes.Count - 1;
            var parentIndex = (itemIndex - 1) / 2;

            // с использованием текущего компаратора определяем, необходимо ли поднимать элемент вверх по дереву
            // если компаратор возвращает -1, то необходимо поднимать элемент
            while (itemIndex > 0 && _comparer.Compare(_nodes[itemIndex], _nodes[parentIndex]) < 0)
            {
                var temp = _nodes[itemIndex];
                _nodes[itemIndex] = _nodes[parentIndex];
                _nodes[parentIndex] = temp;

                itemIndex = parentIndex;
                parentIndex = (itemIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Получение корневого элемента кучи с удалением
        /// </summary>
        /// <returns>Элемент в корне кучи (максимальный или минимальный в зависимости от компаратора)</returns>
        public T Remove()
        {
            // заменяем элемент в корне на последний добавленный
            var item = _nodes[0];
            _nodes[0] = _nodes[Count - 1];
            _nodes.RemoveAt(Count - 1);
            // вызываем процедуру упорядочивания для корня
            // элемент с конца кучи найдёт новое место и сохранятся свойства кучи
            Heapify(0);
            return item;
        }

        /// <summary>
        /// Получение корневого элемента кучи без его удаления
        /// </summary>
        /// <returns>Элемент в корне кучи (максимальный или минимальный в зависимости от компаратора)</returns>
        public T Peek()
        {
            return _nodes[0];
        }

        /// <summary>
        /// Очистка кучи
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }

        /// <summary>
        /// Установка нового компаратора для Кучи
        /// </summary>
        /// <param name="comparer">Компаратор</param>
        public void SetComparator(IBinaryHeapComparer<T> comparer)
        {
            if (comparer == null)
            {
                return;
            }

            // если компараторы совпали, ничего не делаем
            if (comparer.GetType().Equals(_comparer.GetType()))
            {
                return;
            }
            // обновляем компаратор
            _comparer = comparer;
            // упорядочиваем кучу начиная с корня согласно новому компаратору
            
            // вызываем процедуру упорядочивания для всех элементов, не являющихся листьямим
            for (int indx = Count / 2; indx >= 0; indx--)
            {
                Heapify(indx);
            }
        }

        /// <summary>
        /// Упорядочивание Кучи согласно текущему компаратору
        /// </summary>
        /// <param name="index">Индекс, с которого начинаем упорядочивание</param>
        private void Heapify(int index)
        {
            if (index < 0 && index >= Count)
            {
                return;
            }

            int leftChildIndex;
            int rightChildIndex;
            int replacingChildIndex = index;

            while (true)
            {
                leftChildIndex = 2 * index + 1;
                rightChildIndex = 2 * index + 2;

                // с использованием текущего компаратора определяем, необходимо ли опускать элемент вниз по дереву
                // если компаратор возвращает -1, то необходимо опускать элемент
                // из 2 детей выбираем наибольшего
                if (leftChildIndex < Count && _comparer.Compare(_nodes[leftChildIndex], _nodes[replacingChildIndex]) < 0)
                {
                    replacingChildIndex = leftChildIndex;
                }
                if (rightChildIndex < Count && _comparer.Compare(_nodes[rightChildIndex], _nodes[replacingChildIndex]) < 0)
                {
                    replacingChildIndex = rightChildIndex;
                }

                // если не нашли заменяемый элемент, то свойства кучи восстановлены
                if (replacingChildIndex == index)
                {
                    break;
                }

                // если нашли заменяемый элемент, меняем местами с исходным
                var temp = _nodes[index];
                _nodes[index] = _nodes[replacingChildIndex];
                _nodes[replacingChildIndex] = temp;
                index = replacingChildIndex;
            }
        }

        /// <summary>
        /// Получение левого потомка узла
        /// </summary>
        /// <param name="index">Индекс родителя</param>
        /// <returns>default(T) в случае, если левого потомка нет, иначе левый потомок</returns>
        private T GetLeftChild(int index)
        {
            return _nodes.ElementAtOrDefault(2 * index + 1);
        }

        /// <summary>
        /// Получение правого потомка узла
        /// </summary>
        /// <param name="index">Индекс родителя</param>
        /// <returns>default(T) в случае, если правого потомка нет, иначе правый потомок</returns>
        private T GetRightChild(int index)
        {
            return _nodes.ElementAtOrDefault(2 * index + 2);
        }

        #endregion
    }
}
