﻿using System.Collections;
using UnityEngine;

public class PonasanjeNeprijatelja : MonoBehaviour {

	public float snaga = 150;
	public GameObject projektil;
	public float PucanjUSekundi = 4f;
	public float brzinaProjektila = 10;
	public int rezultatValue = 150;
	private prikazRezultata prikazrezultata;
	public AudioClip zvukPucnja;
	public AudioClip zvukUnistenja;

	void Start()
	{
		prikazrezultata = GameObject.Find("Rezultat").GetComponent<prikazRezultata>();
	}

	void Die()
	{
		prikazrezultata.Rezultat(rezultatValue);
		Destroy(gameObject);
	}


	// Use this for initialization
	void OnTriggerEnter2D (Collider2D collision) {
		projektil missile = collision.gameObject.GetComponent<projektil>();
		if (missile)
		{
			missile.Hit();
			snaga -= missile.GetDamage();
			if (snaga <= 0)
			{
				/*AudioSource.PlayClipAtPoint(zvukUnistenja, transform.position);*/
				AudioSource.PlayClipAtPoint(zvukUnistenja, transform.position);
				Die();
				
			}
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		float vjerojatnost = PucanjUSekundi * Time.deltaTime;
		if (Random.value < vjerojatnost)
		{
			Fire();
		}
		
	}
	void Fire()
	{
		Vector3 offset = new Vector3(0, -1.0f, 0);
		Vector3 polozajPucanja = transform.position + offset;
		GameObject missile = Instantiate(projektil, polozajPucanja, Quaternion.identity);
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -brzinaProjektila);
		AudioSource.PlayClipAtPoint(zvukPucnja, transform.position);
	}
}
