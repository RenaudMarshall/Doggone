using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggy : MonoBehaviour {

    public Table TableUnder;
	
    public bool IsUnderTable()
    {
        return this.TableUnder != null;
    }
}
