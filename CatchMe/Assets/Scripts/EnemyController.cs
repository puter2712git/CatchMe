using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public Transform target;
  private float speed = 1.0f;

  private GameObject[] enemies;

  void Start() {
    enemies = GameObject.FindGameObjectsWithTag("Enemy");
  }

  void Update() {
    for (int i = 0; i < enemies.Length; i++) {
      enemies[i].transform.LookAt(target);

      Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
      enemies[i].transform.Translate(movement);
    }
  }
}
