using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicBezier : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;
    [SerializeField] Transform pointD;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] AnimationCurve ease;

    void Start()
    {
        StartCoroutine(Animation());
    }

    void Update()
    {
        
    }

    IEnumerator Animation()
    {
        float elapsedTime = 0f;

        while (true)
        {

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float normalizedTime = elapsedTime / animationDuration;

                Vector3 CD = Vector3.Lerp(pointC.position, pointD.position, ease.Evaluate(normalizedTime));
                Vector3 ACD = Vector3.Lerp(pointA.position, CD, ease.Evaluate(normalizedTime));
                Vector3 CDB = Vector3.Lerp(CD, pointB.position, ease.Evaluate(normalizedTime));
                transform.position = Vector3.Lerp(ACD, CDB, ease.Evaluate(normalizedTime));

                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                float normalizedTime = elapsedTime / animationDuration;

                Vector3 DC = Vector3.Lerp(pointD.position, pointC.position, ease.Evaluate(normalizedTime));
                Vector3 BDC = Vector3.Lerp(pointB.position, DC, ease.Evaluate(normalizedTime));
                Vector3 DCA = Vector3.Lerp(DC, pointA.position, ease.Evaluate(normalizedTime));
                transform.position = Vector3.Lerp(BDC, DCA, ease.Evaluate(normalizedTime));

                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0f;

            yield return null;
        }
    }
}
