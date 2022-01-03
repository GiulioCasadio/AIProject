﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class CircleBuffer<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable
{
    private int capacity;
    private int size;
    private int head;
    private int tail;
    private T[] buffer;

    [NonSerialized()]
    private object syncRoot;

    public bool AllowOverflow
    {
        get;
        set;
    }

    public int Capacity
    {
        get { return capacity; }
        set
        {
            if (value == capacity)
                return;

            if (value < size)
                return;

            var dst = new T[value];
            if (size > 0)
                CopyTo(dst);
            buffer = dst;

            capacity = value;
        }
    }

    public int Size
    {
        get { return size; }
    }

    public bool Contains(T item)
    {
        int bufferIndex = head;
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < size; i++, bufferIndex++)
        {
            if (bufferIndex == capacity)
                bufferIndex = 0;

            if (item == null && buffer[bufferIndex] == null)
                return true;
            else if ((buffer[bufferIndex] != null) &&
                comparer.Equals(buffer[bufferIndex], item))
                return true;
        }

        return false;
    }

    public void Clear()
    {
        size = 0;
        head = 0;
        tail = 0;
    }

    public int Put(T[] src)
    {
        return Put(src, 0, src.Length);
    }

    public int Put(T[] src, int offset, int count)
    {
        if (!AllowOverflow && count > capacity - size)
            return 0;

        int srcIndex = offset;
        for (int i = 0; i < count; i++, tail++, srcIndex++)
        {
            if (tail == capacity)
                tail = 0;
            buffer[tail] = src[srcIndex];
        }
        size = Math.Min(size + count, capacity);
        return count;
    }

    public void Put(T item)
    {
        if (!AllowOverflow && size == capacity)
            return;

        buffer[tail] = item;
        if (++tail == capacity)
            tail = 0;
        size++;
    }

    public void Skip(int count)
    {
        head += count;
        if (head >= capacity)
            head -= capacity;
    }

    public T[] Get(int count)
    {
        var dst = new T[count];
        Get(dst);
        return dst;
    }

    public int Get(T[] dst)
    {
        return Get(dst, 0, dst.Length);
    }

    public int Get(T[] dst, int offset, int count)
    {
        int realCount = Math.Min(count, size);
        int dstIndex = offset;
        for (int i = 0; i < realCount; i++, head++, dstIndex++)
        {
            if (head == capacity)
                head = 0;
            dst[dstIndex] = buffer[head];
        }
        size -= realCount;
        return realCount;
    }

    public T Get()
    {
        if (size == 0)
            return default(T);

        var item = buffer[head];
        if (++head == capacity)
            head = 0;
        size--;
        return item;
    }

    public void CopyTo(T[] array)
    {
        CopyTo(array, 0);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        CopyTo(0, array, arrayIndex, size);
    }

    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        if (count > size)
            return;

        int bufferIndex = head;
        for (int i = 0; i < count; i++, bufferIndex++, arrayIndex++)
        {
            if (bufferIndex == capacity)
                bufferIndex = 0;
            array[arrayIndex] = buffer[bufferIndex];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        int bufferIndex = head;
        for (int i = 0; i < size; i++, bufferIndex++)
        {
            if (bufferIndex == capacity)
                bufferIndex = 0;

            yield return buffer[bufferIndex];
        }
    }

    public T[] GetBuffer()
    {
        return buffer;
    }

    public T[] ToArray()
    {
        var dst = new T[size];
        CopyTo(dst);
        return dst;
    }

    // ICollection<T> Members

    int ICollection<T>.Count
    {
        get { return Size; }
    }

    bool ICollection<T>.IsReadOnly
    {
        get { return false; }
    }

    void ICollection<T>.Add(T item)
    {
        Put(item);
    }

    bool ICollection<T>.Remove(T item)
    {
        if (size == 0)
            return false;

        Get();
        return true;
    }

    // IEnumerable<T> Members

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    // ICollection Members

    int ICollection.Count
    {
        get { return Size; }
    }

    bool ICollection.IsSynchronized
    {
        get { return false; }
    }

    object ICollection.SyncRoot
    {
        get
        {
            if (syncRoot == null)
                Interlocked.CompareExchange(ref syncRoot, new object(), null);
            return syncRoot;
        }
    }

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
        CopyTo((T[])array, arrayIndex);
    }

    // IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    // CTOR

    public CircleBuffer(int capacity)
        : this(capacity, false)
    {

    }

    public CircleBuffer(int capacity, bool allowOverflow)
    {
        this.capacity = capacity;
        size = 0;
        head = 0;
        tail = 0;
        buffer = new T[capacity];
        AllowOverflow = allowOverflow;
    }
}