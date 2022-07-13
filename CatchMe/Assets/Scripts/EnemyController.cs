using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public Transform target;
  private Vector3 targetPosition;
  public MeshRenderer targetRenderer;
  private float speed = 1.0f;

  private GameObject[] enemies;

  void Start() {
    enemies = GameObject.FindGameObjectsWithTag("Enemy");
  }

  void Update() {
    for (int i = 0; i < enemies.Length; i++) {
      if (targetRenderer.enabled == true) {
        targetPosition = new Vector3(target.transform.position.x, enemies[i].transform.position.y, target.transform.position.z);
        enemies[i].transform.LookAt(targetPosition);;
   
        Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
        enemies[i].transform.Translate(movement);
      }
    }
  }
}
