using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake() {
            weapon = weaponHolder;
    }
    
    // Start is called before the first frame update
    void Start() {
        Debug.Log("WeaponPickup script is active on " + gameObject.name);
        if (weapon != null) {
            TurnVisual(false);
        } else {
            Debug.LogError("Weapon is not initialized for " + gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D called on " + gameObject.name);
        if (other.CompareTag("Player")) {
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
            
            if (currentWeapon != null) {
                Debug.Log("Player already has a weapon. Replacing the current weapon.");
                Destroy(currentWeapon.gameObject);
            }

            Debug.Log("Player collided with weapon pickup!");

            weapon = Instantiate(weaponHolder, other.transform.position, Quaternion.identity);
            weapon.transform.SetParent(other.transform);
            weapon.parentTransform = other.transform;
            TurnVisual(true, weapon);

        } else {
            Debug.LogError("Weapon is null in OnTriggerEnter2D for " + gameObject.name);
        }
    }

    void TurnVisual(bool on) {
        if (weapon != null) {
            foreach (var component in weapon.GetComponentsInChildren<Renderer>()) {
                component.enabled = on;
            }
        }
    }

    void TurnVisual(bool on, Weapon weapon) {
        if (weapon != null) {
            foreach (var component in weapon.GetComponentsInChildren<Renderer>()) {
                component.enabled = on;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}