using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    public GameObject keycard;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keycard.activeSelf)
        {
            SceneManager.LoadScene("FinalBoss");
        }
    }
}
