using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {
	public AudioClip screamClip;
	public float maxSpeed;
	public float speedMultiplier;
	public float totalSquashTime;
	public float totalFlipTime;
	public ParticleSystem deathParticlesPrefab;

	protected ParticleSystem deathParticles;
	protected Vector3 originalScale;
	protected float squashStartTime;
	protected float lastSquashTime;
	protected Vector3 velocity = Vector3.zero;
	protected bool isFacingRight = true;
	protected bool hasBeenHit = false;

	// Use this for initialization
	void Awake () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - squashStartTime > 0.5f) && deathParticles != null) {
			if (deathParticles.particleCount == 0) {
				GameObject.Destroy(deathParticles.gameObject);
				deathParticles = null;
			}
		}

		if (hasBeenHit) {
			if (transform.localScale.y != 0) {
				transform.localScale = Vector3.Lerp(
					originalScale,
					new Vector3(originalScale.x, 0, originalScale.z),
					(Time.time - squashStartTime) / totalSquashTime);

				if (transform.localScale.y < 0) transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
			}
		}
		else {
			if ((Input.GetKeyDown(KeyCode.LeftArrow) && isFacingRight) || (Input.GetKeyDown(KeyCode.RightArrow) && !isFacingRight)) {
				Flip();
			}

			velocity = new Vector3(speedMultiplier * Input.GetAxis("Horizontal"), 0, 0);
			transform.position = transform.position + velocity * Time.deltaTime;
		}
	}

	void Flip() {
		isFacingRight = !isFacingRight;

		Go.killAllTweensWithTarget(transform);
		Go.to(transform, totalFlipTime, new GoTweenConfig().vector3XProp("localScale", isFacingRight?1:-1));
	}

	public void Bleed() {
		deathParticles = (ParticleSystem)Instantiate(deathParticlesPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (hasBeenHit) return;

		if (coll.gameObject.tag == "Boulder") {
			squashStartTime = Time.time;
			hasBeenHit = true;
		}
	}
}
