using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private Rigidbody m_rigidbody;
  private AudioSource m_audio;
  private Renderer m_renderer;

  private float horizontalMove;
  private float verticalMove;
  private float fire1;
  private float m_speed = 10f;
  private float m_health = 100f;
  private bool isDamaged = false;
  private bool isJumping = false;

  private int m_foodCount = 0;

  public AudioClip successClip;
  public AudioClip failureClip;

  void Start() {
    m_rigidbody = GetComponent<Rigidbody>();
    m_audio = GetComponent<AudioSource>();
    m_renderer = GetComponent<MeshRenderer>();
  }

  void Update() {
    horizontalMove = Input.GetAxis("Horizontal");
    verticalMove = Input.GetAxis("Vertical");
    fire1 = Input.GetAxis("Fire1");

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

    if (Input.GetKeyDown(KeyCode.Space)) {
      StartCoroutine(SpeedUpBoost(20f));
    }

    if (Input.GetKeyDown(KeyCode.H)) {
      StartCoroutine(Invisible());
    }

    if (m_health <= 0f) {
      UnityEditor.EditorApplication.isPlaying = false;
    }
  }

  void FixedUpdate() {
    if (horizontalMove != 0 || verticalMove != 0) {
      m_rigidbody.AddForce(-verticalMove * m_speed, 0f, horizontalMove * m_speed);
    }

    if (fire1 != 0 && !isJumping) {
      isJumping = true;
      m_rigidbody.AddForce(Vector3.up * m_speed, ForceMode.Impulse);
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
    else if (other.gameObject.CompareTag("NonFood") && !isDamaged) {
      m_audio.PlayOneShot(failureClip);
      m_health -= 5f;
      Debug.Log("You tried to eat non-edible item...");
      Debug.Log("Health left: " + m_health);
      StartCoroutine(HealthDecreased());
    }
  }

  IEnumerator HealthDecreased() {
    isDamaged = true;
    yield return new WaitForSeconds(1f);
    isDamaged = false;
  }

  private void OnCollisionEnter(Collision other) {
    if (other.gameObject.CompareTag("Enemy") && !isDamaged) {
      m_audio.PlayOneShot(failureClip);
      m_health -= 15f;
      Debug.Log("Health left: " + m_health);
      StartCoroutine(HealthDecreased());
    }

    if (other.gameObject.CompareTag("Ground")) {
      isJumping = false;
    }
  }

  IEnumerator SpeedUpBoost(float speedUpAmount) {
    m_speed = speedUpAmount;
    yield return new WaitForSeconds(2f);
    m_speed = 10f;
  }

  IEnumerator Invisible() {
    m_renderer.enabled = false;
    yield return new WaitForSeconds(3f);
    m_renderer.enabled = true;
  }
}

