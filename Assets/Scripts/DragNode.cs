using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DragNode : MonoBehaviour {

	GameObject node = null;



	Dictionary<int,GameObject> touchNodes = new Dictionary<int, GameObject>();

	public bool useMouse = false;
	RaycastHit hit;

	void Update () {
	
		var mainCamera = Camera.main;
		Touch touch = new Touch();

		if (useMouse == true)
		{
			 if (Input.GetMouseButton(0)|| Input.GetMouseButtonUp(0) || Input.touchCount> 0)
			{
				Ray ray;

				if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
				{
					ray = mainCamera.ScreenPointToRay (Input.mousePosition);
				}
				else 
				{
					touch = Input.GetTouch(0);

					ray = mainCamera.ScreenPointToRay (Input.GetTouch(0).position);
				}


				if (Input.GetMouseButtonDown(0) || (touch.phase == TouchPhase.Began && !Input.GetMouseButton(0)))
				{
					if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
					{
						if (!Physics.Raycast(ray,  out hit, 100))
						{
							return;
						}
					}
					else
					{

						if (!Physics.Raycast(ray,  out hit, 100))
						{
							int edgeX = Screen.width/10;
							int edgeY = Screen.height/20;

							bool rayhit = false;
							ray = mainCamera.ScreenPointToRay (Input.GetTouch(0).position+Vector2.right*edgeX);
							if (Physics.Raycast(ray,  out hit, 100))
								rayhit = true;
							if (!rayhit)
							{
								ray = mainCamera.ScreenPointToRay (Input.GetTouch(0).position+Vector2.right*(-edgeX));
								if (Physics.Raycast(ray,  out hit, 100))
									rayhit = true;
							}
							if (!rayhit)
							{
								ray = mainCamera.ScreenPointToRay (Input.GetTouch(0).position+Vector2.up*(edgeY));
								if (Physics.Raycast(ray,  out hit, 100))
									rayhit = true;
							}
							if (!rayhit)
							{
								ray = mainCamera.ScreenPointToRay (Input.GetTouch(0).position+Vector2.up*(-edgeY));
								if (Physics.Raycast(ray,  out hit, 100))
									rayhit = true;
							}
							if (!rayhit)
								return;
						}
					}

						
				
					// We need to actually hit an object




					// We need to hit a rigidbody that is not kinematic
					if (hit.collider.GetComponent<Node>() &&
					    (hit.collider.GetComponent<Node>().imSpecial == null ||
					 	(hit.collider.GetComponent<Node>().imSpecial.GetType() != typeof(DestinationNode) &&
						hit.collider.GetComponent<Node>().imSpecial.GetType() != typeof(DataSpawner))))
					{
						node = hit.collider.gameObject;
					}

				}
				if (Input.GetMouseButton(0) || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{

					if (node == null)
					{
						if (!(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
						{
							mainCamera.transform.Translate(
								new Vector3(-Input.GetAxis ("Mouse X"),
							            -Input.GetAxis ("Mouse Y"),
							            0));

						}
						else
						{
							mainCamera.transform.Translate(
								new Vector3(-touch.deltaPosition.x/40,
						       	     -touch.deltaPosition.y/40,
						       	     0));
						}					           
					}
					else
					{
						node.transform.position = new Vector3(
							ray.GetPoint(hit.distance).x,
							ray.GetPoint(hit.distance).y,
							0);
						node.rigidbody.velocity = Vector3.zero;
					}
				}
				if (Input.GetMouseButtonUp(0) || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					node = null;
				}


		    
			}
		}
		else
		{
			//if (Input.touches.Length>1)
			//	mainCamera.backgroundColor = Color.green;
			if (Input.touches.Length >= 8)
			{
				mainCamera.transform.position =
					new Vector3(0,
				            1,
				            -10);
				return;
			}

			foreach (Touch t in Input.touches)
			{
				touch = t;
		//		touch = Input.GetTouch(1);
				Ray rayTouch = mainCamera.ScreenPointToRay (touch.position);

			
				if (touch.phase == TouchPhase.Began)
				{
					if (!Physics.Raycast(rayTouch,  out hit, 100))
					{
						int edgeX = Screen.width/10;
						int edgeY = Screen.height/20;

						bool rayhit = false;
						rayTouch = mainCamera.ScreenPointToRay (touch.position+Vector2.right*edgeX);
						if (Physics.Raycast(rayTouch,  out hit, 100))
							rayhit = true;
						if (!rayhit)
						{
							rayTouch = mainCamera.ScreenPointToRay (touch.position+Vector2.right*(-edgeX));
							if (Physics.Raycast(rayTouch,  out hit, 100))
								rayhit = true;
						}
						if (!rayhit)
						{
							rayTouch = mainCamera.ScreenPointToRay (touch.position+Vector2.up*(edgeY));
							if (Physics.Raycast(rayTouch,  out hit, 100))
								rayhit = true;
						}
						if (!rayhit)
						{
							rayTouch = mainCamera.ScreenPointToRay (touch.position+Vector2.up*(-edgeY));
							if (Physics.Raycast(rayTouch,  out hit, 100))
								rayhit = true;
						}
						if (!rayhit)
						{
							touchNodes.Add (touch.fingerId,null);
							return;
						}

					}
					// We need to hit a rigidbody that is not kinematic
					if (hit.collider.GetComponent<Node>() &&
					    (hit.collider.GetComponent<Node>().imSpecial == null ||
					 	(hit.collider.GetComponent<Node>().imSpecial.GetType() != typeof(DestinationNode) &&
					 	hit.collider.GetComponent<Node>().imSpecial.GetType() != typeof(DataSpawner))))
					{
						node = hit.collider.gameObject;
						touchNodes.Add (touch.fingerId,node);
					}
					else {
						touchNodes.Add (touch.fingerId,null);
					}
				}



			
				if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					if (touchNodes[touch.fingerId] == null)
					{
						mainCamera.transform.Translate(
							new Vector3(-touch.deltaPosition.x/40,
					       	     -touch.deltaPosition.y/40,
					       	     0));
					}
					else
					{
						touchNodes[touch.fingerId].transform.position = new Vector3(
							rayTouch.GetPoint(hit.distance).x,
							rayTouch.GetPoint(hit.distance).y,
							0);
						touchNodes[touch.fingerId].rigidbody.velocity = Vector3.zero;
					}
				}
				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					touchNodes.Remove(touch.fingerId);
				}
			}
		}
	}
}
