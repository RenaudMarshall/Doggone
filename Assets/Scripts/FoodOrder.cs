using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder {
    
    public enum OrderStatus
    {
        Idea,
        Order,
        Food,
        Check,
        Reciept
    }


    public float progress;

    public OrderStatus Status;

    public float Size;

    public Table forTable;

    public FoodOrder(float size, Table t)
    {
        this.Size = size;
        this.forTable = t;
    }

    public void DoWork(float work)
    {
        this.progress += work;
    }

    public bool IsDone()
    {
        return this.progress > this.Size;
    }
}
