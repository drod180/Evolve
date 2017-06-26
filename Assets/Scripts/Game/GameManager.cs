using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int mapSizeX, mapSizeY, mountMin, mountMax, riverMin, riverMax;
	public TileManager map;
	public int players = 8;

	private WorldParams buildParams;
	// Use this for initialization
	void Start () {
		buildGame (players);
	}
		
	private void buildGame(int playerCount) {
		intializeValues ();
		map.buildWorld (buildParams);
		map.addPlayers (playerCount);
	}

	private void intializeValues () {
		buildParams = new WorldParams (mapSizeX, mapSizeY, mountMin, mountMax, riverMin, riverMax);
	}
}
