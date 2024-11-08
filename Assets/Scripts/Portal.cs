using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    private Vector2 newPosition;

    void Start()
    {
        Debug.Log("Portal Start: Initializing new position.");
        ChangePosition();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        Debug.Log($"Portal Update: Rotating with speed {rotateSpeed}");

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        Debug.Log($"Portal Update: Moving towards new position {newPosition} with speed {speed}");

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            Debug.Log("Portal Update: Reached target position. Generating a new position.");
            ChangePosition();
        }

        bool hasWeapon = PlayerHasWeapon();
        Debug.Log($"Portal Update: Player has weapon? {hasWeapon}");

        if (hasWeapon)
        {
            Debug.Log("Portal Update: Player has a weapon, activating portal.");
            gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            Debug.Log("Portal Update: Player does NOT have a weapon, deactivating portal.");
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Portal OnTriggerEnter2D: Player collided with portal. Loading Main scene.");
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        Debug.Log($"Portal ChangePosition: New target position set to {newPosition}");
    }

    private bool PlayerHasWeapon()
    {
        Player player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Portal PlayerHasWeapon: No player object found.");
            return false;
        }

        bool hasWeapon = player.HasWeapon();
        Debug.Log($"Portal PlayerHasWeapon: Player weapon status: {hasWeapon}");
        return hasWeapon;
    }
}