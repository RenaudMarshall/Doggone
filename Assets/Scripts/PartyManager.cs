using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {
    private Table[] tables;
    private float _spawnCoolDown;
    public float SpawnCoolDown;
    public Party PartyPrefab;

	// Use this for initialization
	void Start () {
        tables = GameObject.FindObjectsOfType<Table>();
    }
	
	// Update is called once per frame
	void Update () {
		if(_spawnCoolDown > 0)
        {
            _spawnCoolDown -= Time.deltaTime;
        }
        else
        {
            Table openTable = GetOpenTable();
            if (openTable && !openTable.isTaken)
            {
                _spawnCoolDown = SpawnCoolDown;
                
                PartyPrefab.PartyMemberSize = Mathf.RoundToInt((openTable.Chairs.Length - 1) * Random.value) + 1;
                Party p = Instantiate(PartyPrefab, this.transform);
                p.CreateMembers();
                p.TakeTable(openTable);
                
            }

        }
	}

    private Table GetOpenTable()
    {
        foreach(Table t in tables)
        {
            if (!t.isTaken)
            {
                return t;
            }
        }
        return null;
    }
}
