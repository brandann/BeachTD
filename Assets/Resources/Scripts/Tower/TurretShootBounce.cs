﻿using UnityEngine;
using System.Collections;

public class TurretShootBounce : MonoBehaviour {

    private const float MINPOS = -0.06f;
    private const float MAXPOS = 0.02f;
    private const float STARTPOS = 0;
    private const float SPEED = 0.01f;

    public int Multiplier;

    private int MOVE;

    private enum State { DIP, RISE, ENDING, WAITING}
    private State currentState;

    private float timeElapsed = 0;
    private float SpawnTime = .1f;

    public void Shoot()
    {
        if (currentState == State.WAITING)
        {
            currentState = State.DIP;
        }
    }

	// Use this for initialization
	void Start () {
        currentState = State.WAITING;
        MOVE = 0;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case (State.WAITING):
                break;
            case (State.DIP):
                transform.position += transform.up * (SPEED * -1);
                MOVE--;
                if (MOVE <= 6 * Multiplier)
                {
                    currentState = State.RISE;
                }
                break;
            case (State.RISE):
                transform.position += transform.up * (SPEED);
                MOVE++;
                if (MOVE >= 2 * Multiplier)
                {
                    currentState = State.ENDING;
                }
                break;
            case (State.ENDING):
                transform.position += transform.up * (SPEED * -1);
                MOVE--;
                if(MOVE <= 0)
                {
                    currentState = State.WAITING;
                }
                break;
        }
	}
}
