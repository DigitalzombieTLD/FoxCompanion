using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnhollowerRuntimeLib;
using UnhollowerBaseLib.Attributes;
using System.IO;
using System.Reflection;

namespace FoxCompanion
{
	public class UnitPathfinder : MonoBehaviour
	{
		public UnitPathfinder(IntPtr obj0) : base(obj0)
		{
		}
		//AI PARAMETERS
		public float speed = 5;
		public rotationType updateRotation = rotationType.rotateLeftRight;

		//other
		Vector3 destination;

		bool mustMove = true;
		float pathRefreshTime = 0;
		void FixedUpdate()
		{
			CorvoPathFinder pf = GetComponent<CorvoPathFinder>();
			if (destinationActive)
			{
				if (pf)
				{
					if (pf.havePath())
						checkReachedNode();
					if (Time.time > pathRefreshTime)
					{
						updatePath();
					}

					if (mustMove)
					{
						if (pf.havePath())
						{
							Vector3 _dir = (pf.getDestination() - transform.position).normalized;
							if (updateRotation != rotationType.dontRotate)
							{
								Vector3 _dir2D;
								if (updateRotation == rotationType.rotateAll)//rotate all
									_dir2D = (pf.getDestination() - transform.position).normalized;
								else//don't update z axis
									_dir2D = ((pf.getDestination() - Vector3.up * pf.getDestination().y) - (transform.position - Vector3.up * transform.position.y)).normalized;
								transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_dir2D), Time.deltaTime * speed * 60);
							}
							transform.position = Vector3.MoveTowards(transform.position, pf.getDestination(), Time.deltaTime * speed);
						}
					}
				}
				else
				{
					MelonLoader.MelonLogger.Msg("No PathFinder Assigned! please assign component CorvopathFinder to this object.", gameObject);
				}
			}
		}

		bool destinationActive = false;
		
		[HideFromIl2Cpp]
		public void goTo(Vector3 _dest)//Start moving to position following pest path
		{
			destinationActive = true;
			destination = _dest;
			updatePath();
		}

		public void stop()//stop unit if moving
		{
			GetComponent<CorvoPathFinder>().forceStop();
			destinationActive = false;
		}

		public void updatePath()//reload the path to see if world has changed
		{
			if (GetComponent<CorvoPathFinder>().findPath(destination))
				pathRefreshTime = Time.time + UnityEngine.Random.Range(9f, 12f);//update world path after X seconds (maybe world has changed?)
			else
				pathRefreshTime = Time.time + UnityEngine.Random.Range(0.01f, 0.1f);//wait until can find new path
		}

		public void checkReachedNode()//Check if reached next Pathnode
		{
			if (Vector3.Distance(GetComponent<CorvoPathFinder>().getDestination(), transform.position) < 1.3f)
				GetComponent<CorvoPathFinder>().nextNode();
			//was last node?
			if (GetComponent<CorvoPathFinder>().foundPath == null)
			{
				if (Vector3.Distance(transform.position, destination) > 5)
				{//not arrived yet
					updatePath();
				}
				else
					stop();//arrived
			}
		}
	}

	public enum rotationType
	{
		dontRotate,
		rotateAll,
		rotateLeftRight
	}
}