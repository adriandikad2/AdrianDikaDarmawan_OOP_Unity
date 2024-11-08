using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake() {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName) {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        yield return SceneManager.LoadSceneAsync(sceneName);

        if (Player.Instance != null) {
            Player.Instance.transform.position = new Vector3(0, -4.5f, 0);
        }

        animator.SetTrigger("End");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}