using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMap {

	protected int P = (int) Global.MapToken.Path;
	protected int T = (int) Global.MapToken.Tower;
	
	public virtual Map GetMap()
	{
		return null;
	}

	public Dictionary<int, int[,]> GetGameMaps()
	{
		
		Dictionary<int, int[,]> _maps = new Dictionary<int, int[,]>();
		
		int[,] m0 = new int[,]
		   {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m1 = new int[,]
           {{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//0
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//1
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//2
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//5
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//6
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T}};
		
		int[,] m2 = new int[,]
           {{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},//0
			{0,P,P,P,P,P,P,1,T,T,T,T,T,T,T},//1
			{T,T,T,T,T,T,T,P,T,T,T,T,T,T,T},//2
			{T,T,T,T,T,T,T,P,T,T,T,T,T,T,T},//3
			{T,T,T,T,T,T,T,P,T,T,T,T,T,T,T},//4
			{T,T,T,T,T,T,T,P,T,T,T,T,T,T,T},//5
			{T,T,T,T,T,T,T,2,P,P,P,P,P,P,3},//6
			{T,T,T,T,T,T,T,T,T,T,T,T,T,T,T}};
		
		int[,] m3 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m4 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m5 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m6 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m7 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m8 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m9 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
		
		int[,] m10 = new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}};
			
		
		
		_maps.Add(0, m0);
		_maps.Add(1, m1);
		_maps.Add(2, m2);
		_maps.Add(3, m3);
		_maps.Add(4, m4);
		_maps.Add(5, m5);
		_maps.Add(6, m6);
		_maps.Add(7, m7);
		_maps.Add(8, m8);
		_maps.Add(9, m9);
		_maps.Add(10,m10);
		
		return _maps;
	}
}
