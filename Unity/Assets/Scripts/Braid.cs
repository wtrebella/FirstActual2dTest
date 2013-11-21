using UnityEngine;
using System.Collections;

public class Braid : MonoBehaviour {
	protected Animator animator;
	protected int walkHash;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
		walkHash = Animator.StringToHash("Base Layer.Braid Walking");
	}
	
	// Update is called once per frame
	void Update () {
		float curSpeed = animator.GetFloat("Walk Speed");
		animator.SetFloat("Walk Speed", curSpeed + Time.deltaTime);
	}
}
