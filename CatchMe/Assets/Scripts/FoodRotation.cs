using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRotation : MonoBehaviour {
  private GameObject[] foods;

  void Start() {
    foods = GameObject.FindGameObjectsWithTag("Food");
  }

  void Update() {
    for (int i = 0; i < foods.Length; i++) {
      if (foods[i] != null) {
        foods[i].transform.Rotate(new Vector3(0, 1, 0) * 180 * Time.deltaTime);
      }
    }
  }
}
