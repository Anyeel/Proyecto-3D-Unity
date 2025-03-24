using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEdges : MonoBehaviour
{
    [SerializeField] float spawnTime = 1f;
    [SerializeField] float despawnTime = 1f;
    [SerializeField] GameObject prefab;
    [SerializeField] float xLength = 4f;
    [SerializeField] float yLength = 4f;
    [SerializeField] float zLength = 4f;
    [SerializeField] float startPosition = 0;
    [SerializeField] float maxXRotationSpeed = 10f;
    [SerializeField] float maxYRotationSpeed = 10f;
    [SerializeField] float maxZRotationSpeed = 10f;
    [SerializeField] float xAnimationDuration = 10f;
    [SerializeField] float yAnimationDuration = 10f;
    [SerializeField] float zAnimationDuration = 10f;
    [SerializeField] AnimationCurve ease;

    private List<GameObject> cubes = new List<GameObject>();
    private float[] rotationSpeeds;

    void Start()
    {
        rotationSpeeds = new float[3];

        StartCoroutine(CreateCube());
        StartCoroutine(InterpolationCube(maxXRotationSpeed, xAnimationDuration, 0));
        StartCoroutine(InterpolationCube(maxYRotationSpeed, yAnimationDuration, 1));
        StartCoroutine(InterpolationCube(maxZRotationSpeed, zAnimationDuration, 2));
    }

    void Update()
    {
        transform.Rotate(rotationSpeeds[0] * Time.deltaTime, rotationSpeeds[1] * Time.deltaTime, rotationSpeeds[2] * Time.deltaTime);
    }

    IEnumerator CreateCube()
    {
        for (float i = startPosition; i < startPosition + xLength; i++)
        {
            for (float j = startPosition; j < startPosition + yLength; j++)
            {
                for (float k = startPosition; k < startPosition + zLength; k++)
                {
                    if ((i == startPosition || i == startPosition + xLength - 1) &&
                        (j == startPosition || j == startPosition + yLength - 1 || k == startPosition || k == startPosition + zLength - 1) ||
                        (j == startPosition || j == startPosition + yLength - 1) &&
                        (k == startPosition || k == startPosition + zLength - 1))
                    {
                        GameObject cube = Instantiate(prefab, transform);
                        cube.transform.localPosition = new Vector3(i, j, k);
                        cubes.Add(cube);
                        yield return new WaitForSeconds(spawnTime);
                    }
                }
            }
        }

        StartCoroutine(DestroyCube());
    }

    IEnumerator DestroyCube()
    {
        foreach (GameObject cube in cubes)
        {
            Destroy(cube); 
            yield return new WaitForSeconds(despawnTime); 
        }
        cubes.Clear();
        StartCoroutine(CreateCube());
        yield return null;
    }

    IEnumerator InterpolationCube(float maxRotationSpeed, float animationDuration, int axis)
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                rotationSpeeds[axis] = Mathf.LerpUnclamped(0, maxRotationSpeed, ease.Evaluate(elapsedTime / animationDuration));
                yield return null;
            }

            elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                rotationSpeeds[axis] = Mathf.LerpUnclamped(0, -maxRotationSpeed, ease.Evaluate(elapsedTime / animationDuration));
                yield return null;
            }

            elapsedTime = 0f;

            yield return null;
        }
    }
}
