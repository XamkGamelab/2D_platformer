using UnityEngine;

public class Follow_player : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
        // Jos halutaan kameran seuraavan pelaajaa t�ysin
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        // Jos halutaan kameran katsovan v�h�n eteen
        //transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
