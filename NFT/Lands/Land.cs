using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour {

  // Static variable to keep track of attached instances of this script
  private static bool scriptAttached = false;
  public int xPos = 0;
  public int yPos = 0;

  void Awake() {
    // Check if this script is already attached to the GameObject.
    if (scriptAttached) {
      Debug.LogWarning("Script already attached to this GameObject.");
      Destroy(this);
      return;
    }

    // Attach the script to the GameObject if it's not already attached.
    scriptAttached = true;
  }

  void OnDestroy() {
    // Reset the scriptAttached variable when the script is destroyed.
    scriptAttached = false;
  }

  // Start is called before the first frame update.
  void Start() {}

  // Update is called once per frame.
  void Update() {}
}