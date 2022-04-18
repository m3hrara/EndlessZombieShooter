using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    private GameManager gameManager;
    StateMachine zombieStateMachine;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        zombieStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        zombieStateMachine.ChangeState((ZombieStateType.isDead));
    }
    private void Update()
    {
        if (CurrentHealth < 1)
        {
            zombieStateMachine.ChangeState((ZombieStateType.isDead));
            gameManager.zombieKills++;
            Destroy(this);
        }
    }
}
