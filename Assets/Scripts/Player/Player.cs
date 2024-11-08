using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static Player Instance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;
    private Weapon currentWeapon;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        Transform engineEffectTransform = transform.Find("Engine/EngineEffect");

        if (engineEffectTransform != null) {
            animator = engineEffectTransform.GetComponent<Animator>();
        } else {
            Debug.LogError("Animator not found! Ensure Engines/BaseEngineAnimator path is correct.");
        }
    }

    public bool HasWeapon() {
        return currentWeapon != null;
    }

    public void EquipWeapon(Weapon weapon) {
        currentWeapon = weapon;
    }

    void FixedUpdate() {
            playerMovement.Move();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
