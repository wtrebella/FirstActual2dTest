using UnityEngine;
using System.Collections;

public enum FacingDirection {
	Right,
	Left
}

public class Bird : MonoBehaviour {
	public float speed = 1.0f;

	protected Animator birdAnim;
	protected FacingDirection facingDirection = FacingDirection.Right;

	// Use this for initialization
	void Awake () {
		birdAnim = GameObject.Find("Bird Sprite").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime;

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
