using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {
	public AudioClip thudClip;

	protected bool hasLanded = false;
	protected Boy boy;

	// Use this for initialization
	void Awake () {
		StartCoroutine(DropBoulder());
		boy = GameObject.Find("Boy").GetComponent<Boy>();
	}

	IEnumerator DropBoulder() {
		yield return new WaitForSeconds(1);

		rigidbody2D.isKinematic = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (hasLanded) return;

		if (coll.gameObject.tag == "InvisibleWall") {
			AudioSource.PlayClipAtPoint(thudClip, transform.position);
			hasLanded = true;
			AudioSource.PlayClipAtPoint(boy.screamClip, transform.position, 0.1f);
			GameObject.Find("Music").GetComponent<AudioSource>().Stop();
			boy.Bleed();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
