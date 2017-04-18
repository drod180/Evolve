using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public Vector2 mapSize = new Vector2 (40, 40);
	public char[,] mapValues;
	public char[] tileValues;

	public GameObject tileEmpty;
	public GameObject tileGrass;
	public GameObject tileMountain;
	public GameObject tileWater;

	public void Start () {
		InvokeRepeating ("buildWorld", 1, 5);
	}

	private void Update () {
	}

	private void buildWorld () {
		SetTiles ();
		AddTilesToWorld ();
	}
	private void SetTiles () {
		initializeMapVars ((int)mapSize.x, (int)mapSize.y);
		addGrasslands ();
		addMountainlands ();
		addWaterlands ();

	}

	private void AddTilesToWorld () {
		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				Vector2 spawnPosition = new Vector2 ((float)i, (float)j);
				switch (mapValues [i, j]) {
				case 'G':
					Instantiate (tileGrass, spawnPosition, transform.rotation);
					break;
				case 'M':
					Instantiate (tileMountain, spawnPosition, transform.rotation);
					break;
				case 'W':
					Instantiate (tileWater, spawnPosition, transform.rotation);
					break;
				default:
					Instantiate (tileEmpty, spawnPosition, transform.rotation);
					break;
				}

			}
		}

	}

	private void addWaterlands() {
		int riverCount = Random.Range (1, 6);
		for (int i = 0; i < riverCount; i++) {
			buildTerrain (Random.Range (2, 4), 'W');
		}
	}

	private void addMountainlands() {
		int mountainCount = Random.Range (1, 5);
		for (int i = 0; i < mountainCount; i++) {
			buildTerrain (Random.Range (2, 4), 'M');
		}
	}

	private void addGrasslands() {
		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				mapValues [i, j] = 'G';
			}
		}
	}

	private void initializeMapVars (int x, int y) {
		tileValues = new char[4] {'W', 'G', 'M', '?'};
		mapValues = new char[x, y];
	}

	private void buildTerrain (int size, char terrain) {
		int maxSize = size * 2;
		int minSize = (int)Mathf.Ceil(size / 2);
		int currentSize = size;
		int length = size * Random.Range (5, 20);
		int currentDirVal = Random.Range (0, 8);
		string[] direction = new string[8] {"N", "NE", "E", "SE", "S", "SW", "W", "NW"};
		int[] currentPos = new int[2] {Random.Range (0, (int)mapSize.x), Random.Range (0, (int)mapSize.y)};

		for (int i = 0; i < length; i++) {
			buildTerrainPiece (direction [currentDirVal], currentPos, currentSize, terrain);
			currentDirVal = updateDir (currentDirVal);
			currentSize = updateSize (currentSize, minSize, maxSize);
			currentPos = updatePos (direction [currentDirVal], currentPos);
		}
	}

	private int updateSize (int size, int minSize, int maxSize) {
		int changeSize = Random.Range (0, 6);
		if (changeSize == 0 && size < maxSize) {
			size += 1;
		} else if (changeSize == 1 && size > minSize) {
			size -= 1;
		}

		return size;
	}

	private int[] updatePos (string dir, int[] pos) {
		switch (dir) {
		case "N":
			pos = new int[2] { pos [0], pos [1] + 1 };
			break;
		case "NE":
			pos = new int[2] { pos [0] + 1, pos [1] + 1 };
			break;
		case "E":
			pos = new int[2] { pos [0] + 1, pos [1] };
			break;
		case "SE":
			pos = new int[2] { pos [0] + 1, pos [1] - 1 };
			break;
		case "S":
			pos = new int[2] { pos [0], pos [1] - 1};
			break;
		case "SW":
			pos = new int[2] { pos [0] - 1, pos [1] - 1};
			break;
		case "W":
			pos = new int[2] { pos [0] - 1, pos [1] };
			break;
		case "NW":
			pos = new int[2] { pos [0] - 1, pos [1] + 1};
			break;
		default:
			Debug.LogError ("Invalid river direction: " + dir);
			break;
		}

		return pos;
	}

	private int updateDir (int currentDir) {
		
		int dirChange = Random.Range (0, 6);
		if (dirChange == 0) {
			currentDir += 1;
		} else if (dirChange == 1) {
			if (currentDir == 0) {
				currentDir = 7;
			} else {
				currentDir -= 1;
			}
		}

		return currentDir % 8;
	}

	private void buildTerrainPiece (string dir, int[] coord, int size, char terrain) {
		int[][] piece = new int[size][]; 
		switch (dir) {
		case "N":
			for (int i = 0; i < size; i++) {
				int x = (int)(Mathf.Pow (-1, i) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, 0};
			}
			break;
		case "NE":
			for (int i = 0; i < size; i++) {
				int x = (int)(((i+1) % 2) * Mathf.Ceil ((i + 1) / 2));
				int y = (int)((i % 2) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, y};
			}
			break;
		case "E":
			for (int i = 0; i < size; i++) {
				int y = (int)(Mathf.Pow (-1, i + 1) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {0, y};
			}
			break;
		case "SE":
			for (int i = 0; i < size; i++) {
				int x = (int)((i % 2) * Mathf.Ceil ((i + 1) / 2));
				int y = (int)(-1 *((i + 1) % 2) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, y};
			}
			break;
		case "S":
			for (int i = 0; i < size; i++) {
				int x = (int)(Mathf.Pow (-1, i + 1) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, 0};
			}
			break;
		case "SW":
			for (int i = 0; i < size; i++) {
				int x = (int)(-1*((i+1) % 2) * Mathf.Ceil ((i + 1) / 2));
				int y = (int)((i % 2) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, y};
			}
			break;
		case "W":
			for (int i = 0; i < size; i++) {
				int y = (int)(Mathf.Pow (-1, i) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {0, y};
			}
			break;
		case "NW":
			for (int i = 0; i < size; i++) {
				int x = (int)(-1 *(i % 2) * Mathf.Ceil ((i + 1) / 2));
				int y = (int)(((i + 1) % 2) * Mathf.Ceil ((i + 1) / 2));
				piece [i] = new int[2] {x, y};
			}
			break;
		default:
			Debug.LogError ("Invalid river direction: " + dir);
			break;
		}
		for (int i = 0; i < piece.Length; i++) {
			if ((coord[0] + piece [i][0] >= 0 && coord[0] + piece [i][0] < (int)mapSize.x) 
				&& (coord[1] + piece[i][1] >= 0 && coord[1] + piece[i][1] < (int)mapSize.y)) {
				mapValues[(coord[0] + piece[i][0]), (coord[1] + piece[i][1])] = terrain;
			}

		}
	}

	private void printMapArray () {
		string tempLog = "";
		for (int i = 0; i < mapSize.x; i++) {
			tempLog = "";
			for (int j = 0; j < mapSize.y; j++) {
				switch (mapValues [i, j]) {
				case 'W':
					tempLog += "<color=blue>████</color>";
					break;
				case 'G':
					tempLog += "<color=green>████</color>";
					break;
				case 'M':
					tempLog += "<color=black>████</color>";
					break;
				default:
					tempLog += "<color=red>████</color>";
					break;
				}
			}
			Debug.Log (tempLog + "\n" + tempLog);
		}

	}
}
