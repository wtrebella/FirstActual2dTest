using UnityEngine;
using System.Collections;

public enum FacingDirection {
	Right,
	Left
}

public class WalkHorizontally : MonoBehaviour {
	public float speed = 1.0f;

	protected Animator birdAnim;
	protected FacingDirection facingDirection = FacingDirection.Right;
	protected Transform birdSpriteTransform;

	// Use this for initialization
	void Awake () {
		birdAnim = GameObject.Find("Bird Sprite").GetComponent<Animator>();
		birdSpriteTransform = GameObject.Find("Bird Sprite").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);

		if (facingDirection == FacingDirection.Right && Input.GetAxis("Horizontal") < 0) {
			facingDirection = FacingDirection.Left;
			Go.killAllTweensWithTarget(transform);
			Go.to(transform, 0.1f, new GoTweenConfig().vector3XProp("localScale", 1));
		}
		else if (facingDirection == FacingDirection.Left && Input.GetAxis("Horizontal") > 0) {
			facingDirection = FacingDirection.Right;
			Go.killAllTweensWithTarget(transform);
			Go.to(transform, 0.1f, new GoTweenConfig().vector3XProp("localScale", -1));
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			birdAnim.SetTrigger("Pump");
		}
		
		if (Input.GetKeyDown(KeyCode.Return)) {
			birdAnim.SetTrigger("Fly");
		}
	}
}
