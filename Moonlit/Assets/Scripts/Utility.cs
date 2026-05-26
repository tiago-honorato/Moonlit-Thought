using UnityEngine;

public class Utility : MonoBehaviour
{
    public void SwitchState()
    {

        gameObject.SetActive(!gameObject.activeSelf);

    }
}
