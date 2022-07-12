using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody m_rigidbody;
  private AudioSource m_audio;

  private float horizontalMove;
  private float verticalMove;
  private float m_speed = 10f;
  private float m_health = 100f;

  private int m_foodCount = 0;

  public AudioClip successClip;
  public AudioClip failureClip;

  void Start() {
    m_rigidbody = GetComponent<Rigidbody>();
    m_audio = GetComponent<AudioSource>();
  }

  void Update() {
    horizontalMove = Input.GetAxis("Horizontal");
    verticalMove = Input.GetAxis("Vertical");

    if (Input.GetKeyDown(KeyCode.P)) {
      switch (Time.timeScale) {
        case 0:
          ResumeGame();
          break;
        case 1:
          PauseGame();
          break;
      }
    }

    if (m_health <= 0f) {
      UnityEditor.EditorApplication.isPlaying = false;
    }
  }

  void FixedUpdate() {
    if (horizontalMove != 0 || verticalMove != 0) {
      m_rigidbody.AddForce(-verticalMove * m_speed, 0f, horizontalMove * m_speed);
    }
  }

  void PauseGame() {
    Time.timeScale = 0;
  }

  void ResumeGame() {
    Time.timeScale = 1;
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Food")) {
      m_audio.PlayOneShot(successClip);
      Destroy(other.gameObject);
      m_foodCount++;
      Debug.Log("You have collected " + m_foodCount + " foods.");
    }
    else if (other.gameObject.CompareTag("NonFood")) {
      m_audio.PlayOneShot(failureClip);
      m_health -= 5f;
      Debug.Log("You tried to eat non-edible item...");
      Debug.Log("Health left: " + m_health);
      StartCoroutine(HealthDecreased());
    }
  }

  IEnumerator HealthDecreased() {
    yield return new WaitForSeconds(1f);
  }

  private void OnCollisionEnter(Collision other) {
    if (other.gameObject.CompareTag("Enemy")) {
      m_audio.PlayOneShot(failureClip);
      m_health -= 15f;
      Debug.Log("Health left: " + m_health);
      StartCoroutine(HealthDecreased());
    }
  }
}

