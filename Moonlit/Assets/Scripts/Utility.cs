using UnityEngine;

public class Utility : MonoBehaviour
{

    public void ToggleObj(GameObject G_object)
    {
        if (G_object != null)
        {
            G_object.SetActive(!G_object.activeSelf);
        }
    }
}