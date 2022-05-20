using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    public abstract void EnterState();

    public abstract void ExitState();
    
    public abstract void UpdateState();
}
