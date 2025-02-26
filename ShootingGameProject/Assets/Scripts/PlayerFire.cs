using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;    //ÃÑ¾Ë ÇÁ¸®ÆÕ
    public GameObject firePosition;     //ÃÑ ¹ß»ç À§Ä¡

    void Update()
    {
        if (Input.GetButtonDown("Jump"))  
        {
            //ÃÑ¾Ë »ý¼º
            GameObject bulletObject = Instantiate(bullet);
            //ÃÑ¾Ë À§Ä¡ º¯°æ
            bulletObject.transform.position = firePosition.transform.position;
        }
    }
}
