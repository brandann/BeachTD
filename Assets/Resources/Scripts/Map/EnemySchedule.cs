using UnityEngine;
using System.Collections;

public class EnemySchedule {

	public enum Token {WAIT, A0, A1, B0, B1, C0, C1}
	public float time;
	public Token token;
	
	public EnemySchedule(Token tok, float ti) { token = tok; time = ti; }
}
