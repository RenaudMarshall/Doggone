using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder {
    
    public enum OrderStatus
    {
        Idea,
        Order,
        TransitFood,
        Food,
        Check,
        Reciept,
        Leave
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
        if (this.IsDone())
            this.Advance();
    }

    private bool IsDone()
    {
        return this.progress > this.Size;
    }

    private void Advance()
    {
        this.progress = 0;
        switch (Status)
        {
            case OrderStatus.Idea:
                Status = OrderStatus.Order;
                break;
            case OrderStatus.Order:
                Status = OrderStatus.TransitFood;
                break;
            case OrderStatus.TransitFood:
                Status = OrderStatus.Food;
                break;
            case OrderStatus.Food:
                Status = OrderStatus.Check;
                break;
            case OrderStatus.Check:
                Status = OrderStatus.Reciept;
                break;
            case OrderStatus.Reciept:
                Status = OrderStatus.Leave;
                break;
        }
    }
}
