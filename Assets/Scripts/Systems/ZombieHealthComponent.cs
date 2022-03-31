using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{ 
    StateMachine zombieStateMachine;
    // Start is called before the first frame update
    private void Awake()
    {
        zombieStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        zombieStateMachine.ChangeState((ZombieStateType.isDead));
    }
}
