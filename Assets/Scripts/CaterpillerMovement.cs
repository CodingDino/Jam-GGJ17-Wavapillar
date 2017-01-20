using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillerMovement : MonoBehaviour {

	public string XAxis = "Horizontal";
	public string YAxis = "Vertical";
	public string GripAxis = "Grip";
	public float forceMult = 100.0f;
	public float forceMultAboveBody = 50.0f;
	public float forceMultNoGrip = 10.0f;
	public List<string> GripColliders = new List<string>();
	public CaterpillerMovement otherHead;

	private bool gripping = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Rigidbody2D body = GetComponent<Rigidbody2D>();

		if (Input.GetAxis(GripAxis) > 0)
		{
			if (gripping || CanGrip())
			{
				gripping = true;
				body.isKinematic = true;
				body.velocity = Vector2.zero;
				body.angularVelocity = 0.0f;
			}
		}
		else
		{
			gripping = false;
		}

		if (!gripping)
		{
			body.isKinematic = false;

			float joyX = Input.GetAxis(XAxis);
			float joyY = Input.GetAxis(YAxis);

			body.AddForce(new Vector2(joyX, -joyY) * GetForceMult());
		}


	}

	private bool CanGrip()
	{
		ColliderList colliders = GetComponent<ColliderList>();

		for (int i = 0; i < GripColliders.Count; ++i)
		{
			if (colliders.IsCollidingWithTag(GripColliders[i]))
				return true;
		}
		return false;
	}

	private float GetForceMult()
	{
		if (otherHead.gripping)
		{
			if(transform.position.y < otherHead.transform.position.y + 1)
				return forceMult;
			else
				return forceMultAboveBody;
		}
		else
		{
			//if(bodytouchingenvironment)
			return forceMultNoGrip;
			//else
			//return 0;
		}
	}
}
