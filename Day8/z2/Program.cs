using System;
using System.Collections;
using System.Collections.Generic;
public class MyQueue<T> : IEnumerable<T>
{
    private T[] _items;
    private int _head;
    private int _tail;
    private int _count;
    private const int DefaultCapacity = 4;
    public MyQueue()
    {
        _items = new T[DefaultCapacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }
    public MyQueue(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative");
        _items = new T[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }
    public int Count => _count;
    public bool IsEmpty => _count == 0;
    public void Enqueue(T item)
    {
        if (_count == _items.Length)
        {
            Resize(_items.Length * 2);
        }
        _items[_tail] = item;
        _tail = (_tail + 1) % _items.Length;
        _count++;
    }
    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty");
        T item = _items[_head];
        _items[_head] = default(T);
        _head = (_head + 1) % _items.Length;
        _count--;

        if (_count > 0 && _count == _items.Length / 4)
        {
            Resize(_items.Length / 2);
        }
        return item;
    }
    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty");
        return _items[_head];
    }
    public void Clear()
    {
        if (_head < _tail)
        {
            Array.Clear(_items, _head, _count);
        }
        else
        {
            Array.Clear(_items, _head, _items.Length - _head);
            Array.Clear(_items, 0, _tail);
        }
        _head = 0;
        _tail = 0;
        _count = 0;
    }
    public bool Contains(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < _count; i++)
        {
            int index = (_head + i) % _items.Length;
            if (comparer.Equals(_items[index], item))
                return true;
        }
        return false;
    }
    public T[] ToArray()
    {
        T[] array = new T[_count];
        for (int i = 0; i < _count; i++)
        {
            array[i] = _items[(_head + i) % _items.Length];
        }
        return array;
    }
    private void Resize(int newCapacity)
    {
        T[] newArray = new T[newCapacity];
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _items[(_head + i) % _items.Length];
        }
        _items = newArray;
        _head = 0;
        _tail = _count;
    }
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[(_head + i) % _items.Length];
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class QueueProcessor<T>
{
    private MyQueue<T> _queue;
    public QueueProcessor()
    {
        _queue = new MyQueue<T>();
    }
    public void AddTask(T task)
    {
        _queue.Enqueue(task);
        Console.WriteLine($"[ДОБАВЛЕНО] Задача: {task}");
    }
    public T ProcessNextTask()
    {
        if (_queue.IsEmpty)
        {
            Console.WriteLine("[ОШИБКА] Нет задач для обработки");
            return default(T);
        }
        T task = _queue.Dequeue();
        Console.WriteLine($"[ОБРАБОТАНО] Задача: {task}");
        return task;
    }
    public T PeekNextTask()
    {
        if (_queue.IsEmpty)
        {
            Console.WriteLine("[ОШИБКА] Нет задач в очереди");
            return default(T);
        }
        T task = _queue.Peek();
        Console.WriteLine($"[ПРОСМОТР] Следующая задача: {task}");
        return task;
    }
    public int GetPendingTaskCount()
    {
        return _queue.Count;
    }
    public void ProcessAllTasks()
    {
        Console.WriteLine($"\n[ОБРАБОТКА ВСЕХ ЗАДАЧ] Всего задач: {_queue.Count}");
        while (!_queue.IsEmpty)
        {
            ProcessNextTask();
        }
        Console.WriteLine("[ЗАВЕРШЕНО] Все задачи обработаны\n");
    }
    public bool ContainsTask(T task)
    {
        bool exists = _queue.Contains(task);
        Console.WriteLine($"[ПОИСК] Задача '{task}' {(exists ? "найдена" : "не найдена")}");
        return exists;
    }
    public T[] GetAllTasks()
    {
        T[] tasks = _queue.ToArray();
        Console.WriteLine($"[СПИСОК] Получено {tasks.Length} задач");
        return tasks;
    }
    public void ClearAllTasks()
    {
        _queue.Clear();
        Console.WriteLine("[ОЧИСТКА] Все задачи удалены из очереди");
    }
    public void ProcessBatch(int batchSize)
    {
        Console.WriteLine($"\n[ПАКЕТНАЯ ОБРАБОТКА] Размер пакета: {batchSize}");
        int processed = 0;
        while (!_queue.IsEmpty && processed < batchSize)
        {
            ProcessNextTask();
            processed++;
        }
        Console.WriteLine($"[ИТОГ] Обработано {processed} задач. Осталось: {_queue.Count}\n");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ MyQueue<T> и QueueProcessor<T> ===\n");
        QueueProcessor<string> processor = new QueueProcessor<string>();
        processor.AddTask("Задача 1: Отправить email");
        processor.AddTask("Задача 2: Сгенерировать отчет");
        processor.AddTask("Задача 3: Обновить кэш");
        processor.AddTask("Задача 4: Выполнить бэкап");
        processor.AddTask("Задача 5: Отправить уведомление");
        Console.WriteLine($"\nСтатус: {processor.GetPendingTaskCount()} задач в очереди");
        processor.PeekNextTask();
        processor.ProcessNextTask();
        processor.ProcessNextTask();
        Console.WriteLine($"\nОсталось задач: {processor.GetPendingTaskCount()}");
        processor.ContainsTask("Задача 3: Обновить кэш");
        processor.ContainsTask("Несуществующая задача");
        var allTasks = processor.GetAllTasks();
        Console.WriteLine("Текущие задачи в очереди:");
        foreach (var task in allTasks)
        {
            Console.WriteLine($"  - {task}");
        }
        processor.ProcessBatch(2);
        processor.ProcessAllTasks();
        processor.AddTask("Новая задача 1");
        processor.AddTask("Новая задача 2");
        processor.ClearAllTasks();
        Console.WriteLine($"\nПосле очистки: {processor.GetPendingTaskCount()} задач");
        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ С ЧИСЛАМИ ===\n");
        QueueProcessor<int> numberProcessor = new QueueProcessor<int>();
        numberProcessor.AddTask(100);
        numberProcessor.AddTask(200);
        numberProcessor.AddTask(300);
        numberProcessor.ProcessAllTasks();
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}