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
	public List<ColliderList> BodyColliders = new List<ColliderList>();

	private bool gripping = false;
	private GameObject gripped = null;
	private Collision2D gripCollision = null;
	private Vector3 gripOffset = Vector2.zero;

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
				if (!gripping)
				{
					gripping = true;
					gripCollision = GetGripCollision();
					gripped = gripCollision.collider.gameObject;
					gripOffset = transform.position - gripped.transform.position;
				}

				body.isKinematic = true;
				body.velocity = Vector2.zero;
				body.angularVelocity = 0.0f;
				transform.position = gripped.transform.position + gripOffset;
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

	private GameObject GetGrippedObject()
	{
		ColliderList colliders = GetComponent<ColliderList>();

		for (int i = 0; i < GripColliders.Count; ++i)
		{
			GameObject collided = colliders.GetColliderWithTag(GripColliders[i]);
			if (collided != null)
				return collided;
		}
		return null;
	}

	private Collision2D GetGripCollision()
	{
		ColliderList colliders = GetComponent<ColliderList>();

		for (int i = 0; i < GripColliders.Count; ++i)
		{
			Collision2D collision = colliders.GetCollisionWithTag(GripColliders[i]);
			if (collision != null)
				return collision;
		}
		return null;
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
			for (int i = 0; i < BodyColliders.Count; ++i)
			{
				if (BodyColliders[i].IsCollidingWithTag("Environment"))
				{
					return forceMultNoGrip;
				}
			}
			return 0;
		}
	}
}
