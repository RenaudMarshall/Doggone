using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {

    public int PartyMemberSize;
    public Guest GuestPrefab;
    Guest[] PartyMembers;
    public Table takenTable;
    private bool areLeaving = false;

	// Use this for initialization
	void Start () {
        CreateMembers();
    }

    public void CreateMembers()
    {
        if(this.PartyMembers == null)
        {
            PartyMembers = new Guest[PartyMemberSize];
            for (int i = 0; i < PartyMemberSize; i++)
            {
                PartyMembers[i] = Instantiate(GuestPrefab, this.transform);
                PartyMembers[i].transform.position = Human.RandomVector(1, this.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.takenTable == null)
        {
            if (!areLeaving)
                FindTable();
            else
            if (AllGuestsAreAtExit)
            {
                Destroy(this.gameObject);
            }

        }
        else
        {
            if (this.takenTable.Order == null && !areLeaving)
            {
                if (this.AllGuestsAreSeated)
                    this.takenTable.Order = new FoodOrder(PartyMemberSize * 3, this.takenTable);
            }
            else
            {
                if (this.takenTable.Order.Status == FoodOrder.OrderStatus.Leave)
                {
                    this.takenTable.Order = null;
                    this.takenTable.isTaken = false;
                    this.takenTable = null;
                    GetGuestsOut();
                    areLeaving = true;
                }
            }
        }

    }

    private bool AllGuestsAreSeated
    {
        get
        {
            foreach (Guest g in PartyMembers)
                if (!g.IsAtChair)
                    return false;
            return true;
        }
    }

    private bool AllGuestsAreAtExit
    {
        get
        {
            foreach (Guest g in PartyMembers)
                if (!g.IsAtExit)
                    return false;
            return true;
        }
    }

    private void AssignChairs(Table t)
    {
        Chair[] chairs = t.RandomChairs();
        for (int i = 0; i < PartyMembers.Length; i++)
        {
            PartyMembers[i].OwnChair = chairs[i];
        }
    }
    private void GetGuestsOut()
    {
        for (int i = 0; i < PartyMembers.Length; i++)
        {
            PartyMembers[i].Exit = this.gameObject;
            PartyMembers[i].IsLeaving = true;
        }
    }

    public void TakeTable(Table t)
    {
        this.takenTable = t;
        t.isTaken = true;
        AssignChairs(t);
    }

    private void FindTable()
    {
        Table[] tables = GameObject.FindObjectsOfType<Table>();
        foreach(Table t in tables)
        {
            if (!t.isTaken && t.Chairs.Length >= this.PartyMemberSize)
            {
                TakeTable(t);
                break;
            }
        }
    }
}
