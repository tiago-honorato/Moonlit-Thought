using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{

    [SerializeField] private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ToggleObj(GameObject G_object)
    {
        if (G_object != null)
        {
            G_object.SetActive(!G_object.activeSelf);
        }
    }
}