using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {

    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask mask;

    public int range;
    public int damage;

    public KeyCode shootKey = KeyCode.Mouse0;

    void Start() {
        if (cam == null) this.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(shootKey)) { }
        
    }
}
