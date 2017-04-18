using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite : MonoBehaviour {
	public string Name;
	public Sprite TileImage;
	public Tiles TileType;

	public TileSprite (){
		Name = "Unset";
		TileImage = new Sprite ();
		TileType = Tiles.Unset;
	}

	public TileSprite(string name, Sprite image, Tiles tile){
		Name = name;
		TileImage = image;
		TileType = tile;
	}
}
