using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject Player;
    public GameObject FireingPoint1;
    public GameObject FireingPoint2;

    public GameObject Missile1;
    public GameObject Missile2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();

        }

    }
    void Fire()
    {
        Missile1.transform.position = FireingPoint1.transform.position;
        Missile1.transform.rotation = Player.transform.rotation;
        Instantiate(Missile1);

        Missile2.transform.position = FireingPoint2.transform.position;
        Missile2.transform.rotation = Player.transform.rotation;
        Instantiate(Missile2);

    }
}
