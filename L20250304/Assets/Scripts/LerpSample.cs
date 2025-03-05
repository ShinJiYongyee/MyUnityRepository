using UnityEngine;

public class LerpSample : MonoBehaviour
{
    public Transform A;
    public Transform B;

    [Range(0, 1)]
    public float T = 0;

    float elapsedTime = 0;
    float sign = 1;

    public Color AColor;
    public Color BColor;

    public AnimationCurve curve;
    // Update is called once per frame
    void Update()
    {
        elapsedTime += sign * Time.deltaTime;
      
        if (elapsedTime > 1 || elapsedTime < 0)
        {
            sign = sign * -1;
        }
        transform.position = Vector3.Lerp(A.position, B.position, curve.Evaluate(elapsedTime));
        transform.rotation = Quaternion.Slerp(A.rotation, B.rotation, curve.Evaluate(elapsedTime));

        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.Lerp(AColor, BColor, curve.Evaluate(elapsedTime)));
    }
}
