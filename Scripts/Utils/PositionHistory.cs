using System;
using UnityEngine;

public class PositionHistory
{
    // Internal Variables
    private Vector2[] buffer;
    
    private int head;
    private int tail;
    private int size;
    private int capacity;

    public PositionHistory(int capacity)
    {
        this.capacity = capacity;
        buffer = new Vector2[capacity];
        head = 0;
        tail = 0;
        size = 0;
    }

    public int Size => size;

    public void Add(Vector2 position)
    {
        buffer[tail] = position;
        tail = (tail + 1) % capacity;
        if (size < capacity)
            size++;
        else
            head = (head + 1) % capacity; // Overwrite the oldest element
    }

    public Vector2 Get(int index)
    {
        int actualIndex = (head + index) % capacity;
        if (actualIndex < 0)
            actualIndex += capacity;
        
        if (index >= size)
            throw new IndexOutOfRangeException("Index out of range");

        return buffer[actualIndex];
    }
    
    public Vector2 GetLatest()
    {
        return Get(-1);
    }
    
    public Vector2 GetOldest()
    {
        return Get(0);
    }
}