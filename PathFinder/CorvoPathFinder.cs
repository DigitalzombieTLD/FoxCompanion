using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using MelonLoader;
using System;
using UnhollowerRuntimeLib;
using UnhollowerBaseLib.Attributes;
using System.IO;
using System.Reflection;
using UnhollowerBaseLib;

namespace FoxCompanion
{
	public class CorvoPathFinder : MonoBehaviour
{
	public CorvoPathFinder(IntPtr obj1) : base(obj1)
	{
	}

	//public CorvoPathFinder() : base(ClassInjector.DerivedConstructorPointer<CorvoPathFinder>()) => ClassInjector.DerivedConstructorBody(this);

	public float nodeWidth = 1.5f;
	//[Range(0.02f, 9000)]
	public float unitRay = 0.2f;
	//[Range(0,90)]
	public float climbAngle = 25f;
	public float unitStepHeight = 0.5f;
	//[Range(3f, 1000)]
	public int gridSizeX = 80;
	//[Range(3f, 1000)]
	public int gridSizeY = 80;
	//[Range(1f, 50)]
	public int gridLevels = 2;
	public float levelHeight = 2f;

    //public LayerMask walkMask = ~0;//LayerMask.GetMask("Default","onlyAmbient","Water"); // ORG
       
    public int walkMask = 0;
    public int prohibiteMask = 0;

        

       // public LayerMask prohibiteMask;//LayerMask.GetMask("Default","onlyAmbient","Water"); 
       //layerMask |= 1 << 26;
       //public LayerMask ignoreMask;//LayerMask.GetMask("Default","onlyAmbient","Water"); 
       //[Range(1f, 100)]
        public int expansionCoefficient = 30;
	public diagonalPath diagonals = diagonalPath.everywhere;
	public Transform pathFindingPos;

	//PERFORMANCE:
	public ProcessingSpeed processingSpeed = ProcessingSpeed.high;//if you have many AI or other stupid stuff make it low for better performnce

	GridNode[,,] grid = new GridNode[0, 0, 0];

	[HideFromIl2Cpp]
	public Vector3 getDestination()
	{
		return foundPath.getPosition();
	}

	[HideFromIl2Cpp]
	public Vector3[] getPath()
	{
		if (havePath())
		{               
            List<Vector3> _list = new List<Vector3>();
			GridNode _node = foundPath;
			while (_node != null)
			{
				_list.Add(_node.getPosition());
				_node = _node.nextPathNode;
			}
			return _list.ToArray();
		}
		return new Vector3[0];
	}

	[HideFromIl2Cpp]
	public bool havePath()
	{            
		return _havePath;
	}

	[HideFromIl2Cpp]
	public void nextNode()
	{
		foundPath = foundPath.nextPathNode;
		if (foundPath == null)
			clearPath();
	}

	[HideFromIl2Cpp]
	public bool findPath(Vector3 _destination, Vector3 _startPos)
	{
			int groundMask = 1 << 8;
			int buildingsMask = 1 << 11;
			int nonavMask = 1 << 28;
			int terrainObjectMask = 1 << 9;
			walkMask = groundMask | buildingsMask | nonavMask | terrainObjectMask;

			int waterMask = 1 << 4;
			//int triggerMask = 1 << 21;
			prohibiteMask = waterMask;

			if (!calculating)
			{
			calculating = true;
			MelonCoroutines.Start(calculatePath(_destination, _startPos));
			return true;
			}
		return false;

	}

	[HideFromIl2Cpp]
	public bool findPath(Vector3 _destination, Transform _updatePos = null)
	{
			int groundMask = 1 << 8;
			int buildingsMask = 1 << 11;
			int nonavMask = 1 << 28;
			int terrainObjectMask = 1 << 9;
			walkMask = groundMask | buildingsMask | nonavMask | terrainObjectMask;

			int waterMask = 1 << 4;
			//int triggerMask = 1 << 21;
			prohibiteMask = waterMask;

			if (!calculating)
		{
			if (!_updatePos)
				_updatePos = transform;
			calculating = true;
			MelonCoroutines.Start(calculatePath(_destination, transform.position));
			return true;
		}
		return false;
	}

	[HideFromIl2Cpp]
	public void clearGrid()
	{
		haveGrid = false;
		generating = false;
		grid = new GridNode[0, 0, 0];
	}

	[HideFromIl2Cpp]
	public void clearPath()
	{
		foundPath = null;
		_havePath = false;
		_nearestATALL = _nearestToDest = null;
		_discovered.Clear();
	}

	[HideFromIl2Cpp]
	public void forceStop()
	{
		calculating = generating = false;
		clearGrid();
		clearPath();
	}

	//INTERNAL PROCESSING/YOU DONT NEED TO EDIT HERE
	int sX = 0, sY = 0, lvs = 0;

	[HideFromIl2Cpp]
	public void generateGrid(bool _force = false)
	{
		if (!generating || _force)
		{
			clearGrid();
			sX = gridSizeX + 1;
			sY = gridSizeY + 1;
			lvs = gridLevels;
			if (sX < 3 || sY < 3)
			{
                MelonLogger.Msg("Grid can't be less the 3x3! ", gameObject);
				return;
			}
			grid = new GridNode[sX, sY, lvs];

			MelonCoroutines.Start(generatingGrid());
		}
	}

	bool haveGrid = false;
	bool generating = false;
	
	[HideFromIl2Cpp]
	IEnumerator generatingGrid()
	{
        haveGrid = false;
		generating = true;

		RaycastHit _hit;
		//create point grid 

		int passibase = 0, maxPassi = setMaxPassi();
		//MY VERSION 
		double _startTime = Time.time;

		bool _diagonal = false;
		for (int lev = 0; lev < gridLevels; lev++)
		{
			Vector3 _up = Vector3.down * gridLevels / 2 * levelHeight + Vector3.up * (lev + 1) * levelHeight;
			for (int i = 0; i < sX; i++)//ROW
			{
				for (int j = 0; j < sY; j++)//COLUMN 
				{
					passibase++;
					grid[i, j, lev] = null;
					Vector3 _startPos = transform.position + Vector3.right * (i - (sX / 2)) * iMultiplicator(i) * nodeWidth + Vector3.forward * (j - (sY / 2)) * jMultiplicator(j) * nodeWidth;
					if (Physics.Raycast(_startPos + _up, -Vector3.up, out _hit, levelHeight * 1.3f, walkMask))
					{
						if (prohibiteMask == (prohibiteMask | (1 << _hit.collider.gameObject.layer)))
							continue;
						if (_hit.collider.transform.root == transform.root)
							continue;

						switch (diagonals)
						{
							case diagonalPath.everywhere:
								_diagonal = true;
								break;
							case diagonalPath.nowhere:
								_diagonal = false;
								break;
						}


						if (lev == 0)
							grid[i, j, lev] = new GridNode(_hit.point);
						else
						{
							if (grid[i, j, lev - 1] != null)
							{
								if (_hit.point.y - grid[i, j, lev - 1].getPosition().y > nodeWidth / 3)
									grid[i, j, lev] = new GridNode(_hit.point);
								else
									continue;
							}
							else
								grid[i, j, lev] = new GridNode(_hit.point);
						}

						if (j > 0)//does not go over to the left 
						{
							if (_diagonal)
							{
								if (i > 0)//does not go high 
								{
									//TOP LEFT
									conectGridPoints(grid[i, j, lev], grid[i - 1, j - 1, lev]);
									if (lev > 0)
									{
										conectGridPoints(grid[i, j, lev], grid[i - 1, j - 1, lev - 1]);
										if (i < sX - 1 && j < sY - 1)
											conectGridPoints(grid[i, j, lev], grid[i + 1, j + 1, lev - 1]);
									}
								}
							}

							//TOP
							conectGridPoints(grid[i, j, lev], grid[i, j - 1, lev]);
							if (lev > 0)
							{
								conectGridPoints(grid[i, j, lev], grid[i, j - 1, lev - 1]);
								if (j < sY - 1)
									conectGridPoints(grid[i, j, lev], grid[i, j + 1, lev - 1]);
							}


						}
						if (i > 0)//does not exceed above 
						{
							if (_diagonal)
							{
								if (j < sY - 1)//does not go over to the right 
								{
									//LOWER LEFT 
									conectGridPoints(grid[i, j, lev], grid[i - 1, j + 1, lev]);
									if (lev > 0)
									{
										conectGridPoints(grid[i, j, lev], grid[i - 1, j + 1, lev - 1]);
										if (i < sX - 1 && j > 0)
											conectGridPoints(grid[i, j, lev], grid[i + 1, j - 1, lev - 1]);
									}
								}
							}
							//LEFT
							conectGridPoints(grid[i, j, lev], grid[i - 1, j, lev]);
							if (lev > 0)
							{
								conectGridPoints(grid[i, j, lev], grid[i - 1, j, lev - 1]);
								if (i < sX - 1)
									conectGridPoints(grid[i, j, lev], grid[i + 1, j, lev - 1]);
							}
						}
						//STOPPA PER AZIONI // STOCK FOR SHARES
						passibase += 4;
						//CHECK IF CLOSE ENOUGH 
					}

					if (passibase > maxPassi)
					{
						passibase = 0;
						if (Application.isPlaying)
							yield return null;

						if (!generating)//forced stop
						{
							yield break;
						}
					}
				}
			}
		}
		//if (!Application.isPlaying)
			MelonLogger.Msg("Grid generated. Time needed: " + (Time.time - _startTime));
		generating = false;
		haveGrid = true;
	}


	[HideFromIl2Cpp]
	public void conectGridPoints(GridNode a, GridNode b)
	{
		if (a == null || b == null)
			return;

		Vector3 _dir = (b.getPosition() - a.getPosition()).normalized;
		Vector3 _dirNM = new Vector3(_dir.x, 0, _dir.z);
      

            if (Vector3.Angle(_dir, _dirNM) <= climbAngle)// or if the grid is close to a wall
		{
			//clear direction
			Collider[] _obstacles = Physics.OverlapCapsule(a.getPosition() + Vector3.up * unitRay + Vector3.up * unitStepHeight, b.getPosition() + Vector3.up * unitRay + Vector3.up * unitStepHeight, unitRay, walkMask);

			if (_obstacles.Length > 0) return;//FOR PURCHASABLE VERSION
			a.addNearNode(b);//they add to each other
			b.addNearNode(a);//check that the lower one is always reachable up to a certain height by falling
		}
	}


	//[HideInInspector]
	public GridNode foundPath = null;

	List<GridNode> _discovered = new List<GridNode>();//nodes already checked
	GridNode _nearestATALL = null;
	GridNode _nearestToDest = null;
	bool calculating = false;
	bool _havePath = false;

	[HideFromIl2Cpp]
	IEnumerator calculatePath(Vector3 _destination, Vector3 _pos, Transform _updatePos = null)
	{
		generateGrid();
		while (!haveGrid)
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.07f));

		float DeltaDistX = Mathf.Abs(sX / 2 * iMultiplicator(0) * nodeWidth);
		float DeltaDistY = Mathf.Abs(sY / 2 * jMultiplicator(0) * nodeWidth);

		if (_destination.x > transform.position.x + DeltaDistX)
			_destination = new Vector3(transform.position.x + DeltaDistX, _destination.y, _destination.z);
		else if (_destination.x < transform.position.x - DeltaDistX)
			_destination = new Vector3(transform.position.x - DeltaDistX, _destination.y, _destination.z);

		if (_destination.z > transform.position.z + DeltaDistY)
			_destination = new Vector3(_destination.x, _destination.y, transform.position.z + DeltaDistY);
		else if (_destination.z < transform.position.z - DeltaDistY)
			_destination = new Vector3(_destination.x, _destination.y, transform.position.z - DeltaDistY);


		_discovered.Clear();
		if (_updatePos == null)
		{
			if (pathFindingPos)
				_pos = pathFindingPos.position;
			else
				_pos = transform.position;
		}
		GridNode _firstNode = nearestNode(_pos);

		if (_firstNode != null)
		{
			_nearestToDest = _nearestATALL = _firstNode;
			_discovered.Add(_firstNode);

			bool found = false;
			//int passibase = 0, maxPassi = setMaxPassi();
			while (_discovered.Count > 0 && haveGrid && !found)
			{
				//passibase++;
				if (_nearestToDest != null)//the one examined is each cycle closest
				{
					_nearestToDest.isChecked = true;
					_discovered.Remove(_nearestToDest);
					float _nearestDistance = Vector3.Distance(_nearestToDest.getPosition(), _destination);
					GridNode _nuovoNearest = _nearestToDest;
					foreach (GridNode _adiacente in _nearestToDest.nearNodes)
					{
						if (!_adiacente.isChecked)//not examined yet
						{
							if (_adiacente.previousPathNode == null)
							{
								_discovered.Add(_adiacente);
								_adiacente.previousPathNode = _nearestToDest;
							}
							if (Vector3.Distance(_adiacente.getPosition(), _destination) < _nearestDistance)
							{
								_nearestDistance = Vector3.Distance(_adiacente.getPosition(), _destination);
								_nuovoNearest = _adiacente;
							}
						}
						//CHECK IF CLOSE ENOUGH
					}
					if (_nearestToDest == _nuovoNearest)//if none of them were closer
					{
						if (_discovered.Count > 0)
						{
							_nearestToDest = _discovered[UnityEngine.Random.Range(0, _discovered.Count)];//one by case
																										 // _nearestToDest = _discovered[_discovered.Count-1];//l'ultimo trovato
						}

					}
					else
						_nearestToDest = _nuovoNearest;
					_nearestDistance = Vector3.Distance(_nearestToDest.getPosition(), _destination);

					if (_nearestDistance < nodeWidth)//is adjacent
					{
						found = true;
					}
					if (_nearestDistance < Vector3.Distance(_nearestATALL.getPosition(), _destination))
						_nearestATALL = _nearestToDest;


					/*
										if (passibase > maxPassi*5)
										{
											//print("Generate grid stop; FPS: " + Corvotools.getMainCamera().getFPS());
											passibase = 0;
											if (Application.isPlaying)
												yield return null;
											//yield return new WaitForSeconds(Time.fixedDeltaTime);
										}*/
				}
			}
			//ends
			GridNode _previous = _nearestATALL;
			while (_previous != null)
			{
				if (_previous.previousPathNode != null)
				{
					_previous.previousPathNode.nextPathNode = _previous;
					foundPath = _previous.previousPathNode;
				}
				_previous = _previous.previousPathNode;
			}
			_discovered.Clear();
			if (foundPath != null)
				_havePath = true;
		}
		else
		{
			_havePath = false;
			//if (!Application.isPlaying)
				MelonLogger.Msg("No path found!");
		}
		_nearestATALL = _nearestToDest = null;
		calculating = false;
		clearGrid();
	}

	[HideFromIl2Cpp]
	public bool isCalculating()
	{
		return calculating;
	}

	[HideFromIl2Cpp]
	float iMultiplicator(int i)
	{
		//return 1;
		return (1 + (Mathf.Abs(i - (float)sX / 2) / (101 - expansionCoefficient)));
	}

	[HideFromIl2Cpp]
	float jMultiplicator(int j)
	{
		//return 1;
		return (1 + (Mathf.Abs(j - (float)(sY / 2)) / (101 - expansionCoefficient)));
	}

	[HideFromIl2Cpp]
	void OnDrawGizmosSelected()
	{
		Color _gizcolor = Color.green;

		if (foundPath == null && grid.Length <= 9 || generating)
		{
			_gizcolor = Color.green;
			Gizmos.color = _gizcolor;
			Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX * nodeWidth, levelHeight * gridLevels, gridSizeY * nodeWidth));


			for (int lev = 0; lev <= gridLevels; lev++)
			{
				Vector3 _ypos = Vector3.down * gridLevels / 2 * levelHeight + Vector3.up * (lev) * levelHeight;

				if (lev == (int)((gridLevels + 1) / 2))
				{
					_gizcolor = Color.cyan;
					_gizcolor.a = 0.25f;
					Gizmos.color = _gizcolor;
					for (int i = -sX / 2; i <= sX / 2; i++)//draw lines
						Gizmos.DrawLine(transform.position + Vector3.right * i * nodeWidth - Vector3.forward * gridSizeY / 2 * nodeWidth, transform.position + Vector3.right * i * nodeWidth + Vector3.forward * gridSizeY / 2 * nodeWidth);
					for (int i = -sY / 2; i <= sY / 2; i++)//draw lines
						Gizmos.DrawLine(transform.position + Vector3.forward * i * nodeWidth - Vector3.right * gridSizeX / 2 * nodeWidth, transform.position + Vector3.forward * i * nodeWidth + Vector3.right * gridSizeX / 2 * nodeWidth);
				}
				_gizcolor = Color.yellow;
				Gizmos.color = _gizcolor;
				//Gizmos.DrawLine(transform.position - Vector3.right * gridSizeX / 2 * nodeWidth + _ypos,transform.position + Vector3.right * gridSizeX / 2 * nodeWidth + _ypos);
				Gizmos.DrawLine(transform.position - Vector3.forward * gridSizeY / 2 * nodeWidth + _ypos, transform.position + Vector3.forward * gridSizeY / 2 * nodeWidth + _ypos);
			}
		}


		if (foundPath == null || generating)
		{
			_gizcolor = Color.yellow;
			Gizmos.color = _gizcolor;
			if (grid.Length > 9 && _nearestATALL == null)
			{
				foreach (GridNode _node in grid)
				{
					if (_node == null)
						continue;
					foreach (GridNode _subnode in _node.nearNodes)
					{
						Gizmos.DrawLine(_node.getLookablePosition(), _subnode.getLookablePosition());
					}
				}
			}
			if (!Application.isPlaying)
			{
				if (_discovered.Count > 0)
				{
					_gizcolor = Color.green;
					Gizmos.color = _gizcolor;
					foreach (GridNode _node in _discovered)
					{
						Gizmos.DrawSphere(_node.getPosition(), nodeWidth / 4);
					}
				}
				_gizcolor = Color.white;
				Gizmos.color = _gizcolor;
				foreach (GridNode _node in grid)
				{
					if (_node != null)
					{
						if (_node.previousPathNode != null && _node.isChecked)
							Gizmos.DrawLine(_node.getLookablePosition(), _node.previousPathNode.getLookablePosition());
					}
					//Gizmos.DrawSphere(_node.getPosition(), nodeWidth / 4);
				}
			}
		}
		if (_nearestATALL != null)
		{
			_gizcolor = Color.blue;
			Gizmos.color = _gizcolor;
			Gizmos.DrawSphere(_nearestATALL.getPosition(), nodeWidth / 1.5f);
		}

		/*//START POSITION ARRAY
        _gizcolor = Color.red;
        Gizmos.color = _gizcolor;
        Gizmos.DrawSphere(transform.position + Vector3.right * -sX/2 * nodeWidth - Vector3.forward * gridSizeY / 2 * nodeWidth, nodeWidth/6);
        _gizcolor = Color.cyan;
        Gizmos.color = _gizcolor;
        Gizmos.DrawSphere(transform.position + Vector3.right * sX / 2 * nodeWidth + Vector3.forward * gridSizeY / 2 * nodeWidth, nodeWidth / 6);*/

		if (foundPath != null)
		{
			_gizcolor = Color.cyan;
			Gizmos.color = _gizcolor;
			Gizmos.DrawSphere(foundPath.getPosition(), nodeWidth / 2);

			GridNode _next = foundPath.nextPathNode;
			while (_next != null)
			{
				Gizmos.DrawSphere(_next.getPosition(), nodeWidth / 4);
				if (_next.nextPathNode != null)
					Gizmos.DrawLine(_next.getLookablePosition(), _next.nextPathNode.getLookablePosition());
				//Gizmos.DrawSphere(_previous.getPosition(), nodeWidth / 3);
				_next = _next.nextPathNode;
			}
		}

	}

	[HideFromIl2Cpp]
	public int setMaxPassi()
	{
		switch (processingSpeed)
		{
			case ProcessingSpeed.veryHigh:
				return 20000;
			case ProcessingSpeed.high:
				return 5000;
			case ProcessingSpeed.medium:
				return 1000;
			case ProcessingSpeed.low:
				return 700;
			case ProcessingSpeed.veryLow:
				return 200;
		}
		return 2000;
	}

	[HideFromIl2Cpp]
	public GridNode nearestNode(Vector3 _pos)
	{
		GridNode _nearest = null;
		float _distance = 50000f; //Mathf.Infinity;
		foreach (GridNode _node in grid)
		{
			/*
            if (_node == grid[sX / 2, sY / 2,gridLevels/2])
                continue;*/
			if (_node != null)
			{
				if (_node.nearNodes.Length > 0)
				{
					float _dist = Vector3.Distance(_pos, _node.getPosition());
					if (_dist < _distance)
					{
						_distance = _dist;
						_nearest = _node;
					}
				}
			}
		}
		return _nearest;
	}

}


public class GridNode
{
	Vector3 position;
	public GridNode[] nearNodes = new GridNode[0];
	public GridNode previousPathNode = null;
	public GridNode nextPathNode = null;
	public bool isChecked = false;

	[HideFromIl2Cpp]
	public GridNode(Vector3 _pos)
	{
		position = _pos;
	}

	[HideFromIl2Cpp]
	public Vector3 getPosition()
	{
		return position;
	}

	[HideFromIl2Cpp]
	public Vector3 getLookablePosition()
	{
		return position + Vector3.up * 0.3f;
	}

	[HideFromIl2Cpp]
	public void addNearNode(GridNode _node)
	{
		GridNode[] _temp = new GridNode[nearNodes.Length + 1];
		for (int _x = 0; _x < nearNodes.Length; _x++)
			_temp[_x] = nearNodes[_x];
		_temp[_temp.Length - 1] = _node;//node added
		nearNodes = _temp;
	}
}

/*
public class PathNode
{
    Vector3 position;
    public PathNode next = null;
    public PathNode previous = null;

    public PathNode(Vector3 _pos,PathNode _previous)
    {
        position = _pos;
        previous = _previous;
    }

    public Vector3 getPosition()
    {
        return position;
    }
}*/

public enum diagonalPath
{
	everywhere,
	nowhere
}
public enum ProcessingSpeed
{
	veryHigh,
	high,
	medium,
	low,
	veryLow
}
}