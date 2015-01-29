using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaps {

	public static Dictionary<int, Map> GetGameMaps()
	{
		int P = (int) Global.MapToken.Path; 			// pathtile
		int T = (int) Global.MapToken.Tower;				// tower tile
		
		Dictionary<int, Map> _maps = new Dictionary<int, Map>();
		
		Map m0 = new Map();
		m0.SetMap(new int[,]
		   {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m1 = new Map();
		m1.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m2 = new Map();
		m2.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m3 = new Map();
		m3.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m4 = new Map();
		m4.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m5 = new Map();
		m5.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m6 = new Map();
		m6.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m7 = new Map();
		m7.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m8 = new Map();
		m8.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m9 = new Map();
		m9.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
		
		Map m10 = new Map();
		m10.SetMap(new int[,]
           {{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//0
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//1
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{0,P,P,P,P,P,P,P,P,P,P,P,P,P,1},//4
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//5
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T}});
			
		
		
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
