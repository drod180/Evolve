using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class TileManager : MonoBehaviour {

	public Vector2 mapSize;
	public char[,] mapValues;
	public Vector2[] startingPoints;
	public GameObject tileEmpty;
	public GameObject tileGrass;
	public GameObject tileMountain;
	public GameObject tileWater;
	public SpeciesManager speciesManager;
	public JumpPointParam jpParam;

	private int[,] fillValues;
	private class FillDetails
	{
		public int value;
		public int count;
		public bool bigEnough;

		public FillDetails(int val, int total, bool big) {
			value = val;
			count = total;
			bigEnough = big;
		}
	}
	private List<FillDetails> fillList;
	private int startingSizeMin;

	private int debugCountPass = 0;
	private int debugCountTotal = 0;

	public void buildWorld (WorldParams buildParams) {
		setTiles (buildParams);
		populateFillList ();
		AddTilesToWorld ();
		buildPathingGrid ();
	}

	public void addPlayers (int speciesCount) {
		bool addedStarting = false;
		while (!addedStarting) {
			addedStarting = addStartingPoints (speciesCount);
		}
		SpeciesManager newSpeciesManager = (SpeciesManager)Instantiate (speciesManager, Vector2.zero, transform.rotation);
		newSpeciesManager.map = this;
		newSpeciesManager.createAllSpecies (startingPoints);
	}

	private void AddTilesToWorld () {
		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				Vector2 spawnPosition = new Vector2 ((float)i, (float)j);
				switch (mapValues [i, j]) {
				case 'G':
					//GameObject newGrass = (GameObject)Instantiate (tileGrass, spawnPosition, transform.rotation);
					//newGrass.transform.parent = this.transform;
					break;
				case 'M':
					//GameObject newMountain = (GameObject)Instantiate (tileMountain, spawnPosition, transform.rotation);
					//newMountain.transform.parent = this.transform;
					break;
				case 'W':
					//GameObject newWater = (GameObject)Instantiate (tileWater, spawnPosition, transform.rotation);
					//newWater.transform.parent = this.transform;
					break;
				default:
					GameObject newTile = (GameObject)Instantiate (tileEmpty, spawnPosition, transform.rotation);
					newTile.transform.parent = this.transform;
					break;
				}

			}
		}
	}
		
	private void setTiles (WorldParams buildParams) {
		initializeMapVars (buildParams.mapSizeX, buildParams.mapSizeY);
		addGrasslands ();
		addMountainlands (buildParams.mountMin, buildParams.mountMax);
		addWaterlands (buildParams.riverMin, buildParams.riverMax);

	}

	private void addGrasslands() {
		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				mapValues [i, j] = 'G';
				fillValues [i, j] = -1;
			}
		}
	}

	private void addMountainlands(int min, int max) {
		int mountainCount = Random.Range (min, max);
		for (int i = 0; i < mountainCount; i++) {
			buildTerrain (Random.Range (3, 5), 'M');
		}
	}


	private void addWaterlands(int min, int max) {
		int riverCount = Random.Range (min, max);
		for (int i = 0; i < riverCount; i++) {
			buildTerrain (Random.Range (3, 5), 'W');
		}
	}

	private void buildTerrain (int size, char terrain) {
		int maxSize = size * 2;
		int minSize = (int)Mathf.Ceil(size / 2.0f);
		if (minSize == 1) {
			Debug.Log(Mathf.Ceil(1.5f));
		}
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

	private void populateFillList () {
		int floodCount = 0;
		int floodTileCount = 0;
		bool floodBigEnough = true;

		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				if (mapValues [i, j] == 'G' && fillValues [i, j] == -1) {
					floodTileCount = floodFill (new int[2] { i, j }, 'G' , (char)floodCount, 0);
					floodBigEnough = floodTileCount >= startingSizeMin;
					fillList.Add(new FillDetails(floodCount, floodTileCount, floodBigEnough));
					floodCount++;
				}
			}
		}
	}

	private int floodFill (int[] node, char targetType, int markingValue, int tileCount) {
		if (targetType == markingValue || 
			mapValues [node [0], node [1]] != targetType ||
			fillValues [node [0], node [1]] == markingValue) {
			return tileCount;
		}

		fillValues [node [0], node [1]] = markingValue;
		tileCount++;

		if (node [1] - 1 >= 0) {
			tileCount = floodFill (new int[2] { node [0], (node [1] - 1) }, targetType, markingValue, tileCount);
		}
		if (node [1] + 1 < mapSize.y) {
			tileCount = floodFill (new int[2] { node [0], (node [1] + 1) }, targetType, markingValue, tileCount); 
		}
		if (node [0] - 1 >= 0) {
			tileCount = floodFill (new int[2] { (node [0] - 1), node [1] }, targetType, markingValue, tileCount); 
		}
		if (node [0] + 1 < mapSize.x) {
			tileCount = floodFill (new int[2] { (node [0] + 1), node [1] }, targetType, markingValue, tileCount);
		}
			
		return tileCount;
	}

	private void initializeMapVars (int x, int y) {
		mapSize = new Vector2 (x, y);
		mapValues = new char[x, y];
		fillValues = new int[x, y];
		fillList = new List<FillDetails>();
		startingSizeMin = (int)(mapSize.x * mapSize.y / 10);
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

	private int updateSize (int size, int minSize, int maxSize) {
		int changeSize = Random.Range (0, 6);
		if (changeSize == 0 && size < maxSize) {
			size += 1;
		} else if (changeSize == 1 && size > minSize) {
			size -= 1;
		}

		return size;
	}
		
	private bool placeStartPoint (int minX, int maxX, int minY, int maxY, int speciesNumber, int attempts) {
		bool placed = false;

		for (int i = 0; i < attempts && !placed; i++) {
			int[] startingPoint = new int[2] { Random.Range (minX, maxX), Random.Range (minY, maxY) };
			if (mapValues[startingPoint[0], startingPoint[1]] == 'G' && fillList[fillValues[startingPoint[0], startingPoint[1]]].bigEnough) {
				placed = true;
				startingPoints [speciesNumber] = new Vector2(startingPoint[0], startingPoint[1]);
			}
		}

		return placed;
	}

	private bool addStartingPoints (int teamCount) {
		float[] start = new float[] { 0, 0.333f }; 
		float[] mid = new float[] { 0.333f, 0.667f }; 
		float[] end = new float[] { 0.667f, 1f };
		float[][] sections = new float[9][]
		{
			new float[4] { start[0],start[1],start[0],start[1] },
			new float[4] { end[0], end[1], end[0],end[1] },
			new float[4] { start[0],start[1], end[0],end[1] },
			new float[4] { end[0], end[1], start[0],start[1] },
			new float[4] { mid[0], mid[1], mid[0],mid[1] },
			new float[4] { end[0], end[1], mid[0],mid[1] },
			new float[4] { start[0], start[1], mid[0],mid[1] },
			new float[4] { mid[0], mid[1], end[0],end[1]},
			new float[4] { mid[0], mid[1], start[0],start[1]}
		};
		startingPoints = new Vector2[teamCount];
		int team = 0;
		bool placed;
		for(int i = 0; i < sections.Length && teamCount > team; i++) {
			placed = false;
			placed = placeStartPoint ((int)(sections[i][0] * mapSize.x), (int)(sections[i][1] * mapSize.x), (int)(sections[i][2] * mapSize.y), (int)(sections[i][3] * mapSize.y), team, 5);
			if (placed) {
				team++;
			}
		}
		return teamCount == team;
	}


	private void buildPathingGrid () {
		BaseGrid searchGrid = new StaticGrid ((int)mapSize.x, (int)mapSize.y);

		for (int i = 0; i < mapSize.x; i++) {
			for (int j = 0; j < mapSize.y; j++) {
				if (mapValues [i, j] == 'G'){
					searchGrid.SetWalkableAt(i, j, true);
				}
			}
		}

		GridPos startPos=new GridPos(0,0); 
		GridPos endPos = new GridPos(0,0); 
		jpParam = new JumpPointParam(searchGrid, startPos, endPos, true, DiagonalMovement.OnlyWhenNoObstacles,HeuristicMode.EUCLIDEAN);
	}

	//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Debugging~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

	private void printFillArray () {
		string tempLog = "";
		for (int i = 0; i < mapSize.x; i++) {
			tempLog = "";
			for (int j = 0; j < mapSize.y; j++) {
				if (fillValues [i, j] == -1) {
					tempLog += "<color=black>████</color>";
				} else if (fillList [fillValues [i, j]].bigEnough) {
					tempLog += "<color=green>████</color>";
				} else if (!fillList [fillValues [i, j]].bigEnough) {
					tempLog += "<color=red>████</color>";
				} else {
					tempLog += "<color=white>████</color>";
				}
			}
			Debug.Log (tempLog + "\n" + tempLog);
		}

	}
		
	private void printStats () {
		Debug.Log ("Pass: <color=green>" + debugCountPass + "</color>");
		Debug.Log ("Fail: <color=red>" + (debugCountTotal - debugCountPass) + "</color>");
		Debug.Log ("Total: <color=black>" + debugCountTotal + "</color>");
		Debug.Log ("%: <color=blue>" + (((float)debugCountPass / (float)debugCountTotal) * 100) + "</color>");
	}
}

public class WorldParams
{
	public int mapSizeX, mapSizeY, mountMin, mountMax, riverMin, riverMax;

	public WorldParams(int mapSizeX, int mapSizeY, int mountMin, int mountMax, int riverMin, int riverMax) {
		this.mapSizeX = mapSizeX;
		this.mapSizeY = mapSizeY;
		this.mountMin = mountMin;
		this.mountMax = mountMax;
		this.riverMin = riverMin;
		this.riverMax = riverMax;
	}

}