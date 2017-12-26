using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Animator anim;
	public float speed = 4f;
	public float jumpPower = 8000f;

	public bool useDino;
	public bool interagir;
	public bool joue;
	public bool invincible;
	public bool embrasser;
	public bool trebuche;

	private Rigidbody2D rb2d;

	public bool grounded;
	public bool onRoad;
	public bool onStreet;
	public float groundCheckRadius;
	public Transform groundCheck;
	public LayerMask whatIsRoad;
	public LayerMask whatIsStreet;

	//public AudioClip walkSound;
	
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		//détection du sol
		onRoad = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsRoad);
		onStreet = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsStreet);
		if ((onRoad == true) || (onStreet == true)) {
			grounded = true;
		} else {
			grounded = false;
		}
	}
	
	// Update is called once per frame 
	void Update () {
		invincible = false;
		//float h = Input.GetAxis ("Vertical");
		float x = Input.GetAxis ("Horizontal");

		anim.SetBool ("grounded", grounded);

		anim.SetBool ("embrasser", embrasser);

		//dino
		if (Input.GetKeyDown(KeyCode.R)) {
			useDino = true;
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			useDino = false;
		}

		//interagir
		if (Input.GetKeyDown (KeyCode.E)) {
			interagir = true;
		}
		if (Input.GetKeyUp(KeyCode.E)) {
			interagir = false;
		}

		if ((useDino == true) || (joue == true)) {
			invincible = true;
		}

		anim.SetBool ("useDino", useDino);
		//empeche le mouvement sur utilisation du dino
		if (useDino == false) { 
			anim.SetFloat ("speed", Mathf.Abs (x));

			//saut
			if (Input.GetKeyDown(KeyCode.Space) && grounded == true) {
				rb2d.AddForce(Vector2.up * jumpPower);	
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				print("space");
			}
			
			//anim.SetBool ("trebuche", trebuche); //à corriger plus tard
			//navigation gauche/droite
			if(trebuche == true){
				//on fait trebucher lucile, elle recule en tombant
				if (x > 0) {
					transform.Translate (-1, 0, 0);
				}
				if (x < 0) {
					transform.Translate (1, 0, 0);
				}
				trebuche = false;
			}else if(trebuche == false){
				if (x > 0) {
					transform.Translate (x  * speed * Time.deltaTime, 0, 0);
					transform.eulerAngles = new Vector2(0, 0);
				}
				if (x < 0) {
					transform.Translate (-x  * speed * Time.deltaTime, 0, 0);
					transform.eulerAngles = new Vector2(0, 180);
				}
			}
		}

		//on ignore les collions ballon/voiture pour que la balle puisse aller sur la route directement
		Physics2D.IgnoreLayerCollision (10, 9, true);

		//on ignore les collisions si le joueur est invincible: avec la voiture
		Physics2D.IgnoreLayerCollision (8, 9, invincible);
		//on ignore les collisions avec le ballon si on n'interagit pas
		if (interagir == false){
			Physics2D.IgnoreLayerCollision (8, 10, true); //ballon
			Physics2D.IgnoreLayerCollision (8, 14, true); //couple
		} else if (interagir == true){
			Physics2D.IgnoreLayerCollision (8, 10, false); //ballon
			Physics2D.IgnoreLayerCollision (8, 14, false); //couple
		}
		
	}


}
