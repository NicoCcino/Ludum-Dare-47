using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPoints : MonoBehaviour
{
    [Header("Geometry")]
    public GameObject origin;
    public List<GameObject> points;
    public List<GameObject> circlePoints;
    public float standardDistance = 20;
    public GameObject onCirclePrefab;
    public GameObject wallPrefab;
    public Transform circleLoopPoints;
    public Transform loopWalls;
    public GameObject point1;
    public int pointsCount = 10;
    public GameObject fakePointPrefab;

    [Header("Game Management")]
    public float shrinkSpeed;
    public float shrinked;
    public float enlargeBonus = 0;
    public float enlargeSpeed = 0.1f;
    public float time = 0f;
    public bool pulse = false;
    public float phaseDuration = 10f;
    public float pulseAmplitude = 2f;

    private float initialShrinkSpeed;


    [Header("Perlin Noise")]

    public float heightScaleX;
    public float freqX;
    public float heightScaleZ;
    public float freqZ;

    public float offsetX = 100;
    public float offsetZ = 100;



    // Start is called before the first frame update
    void Start()
    {
        //SpawnFakePoints(pointsCount);

        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject point = transform.GetChild(i).gameObject;
            if (point != null)
            {
                points.Add(point);
            }
        }
        offsetX = Random.Range(0, 9999f);
        offsetZ = Random.Range(0, 9999f);

        SetPointsPosition();
        BuildWalls();

        initialShrinkSpeed = shrinkSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (point1 != null)
        {
            NoiseUpdate(point1);
        }

        NoiseUpdateAll();
    }

    void SpawnFakePoints(int pointsCount)
    {
        for (int i = 0; i < Mathf.FloorToInt(pointsCount/2); i++)
        {
            Vector3 position = new Vector3(1f,0,-pointsCount/2+i);
            GameObject point = GameObject.Instantiate(fakePointPrefab, position, Quaternion.identity, this.transform);
        }
        for (int i = Mathf.FloorToInt(pointsCount / 2); i < pointsCount; i++)
        {
            Vector3 position = new Vector3(-1f, 0, -pointsCount / 2 + i);
            GameObject point = GameObject.Instantiate(fakePointPrefab, position, Quaternion.identity, this.transform);
        }
    }

    public void SetPointsPosition()
    {
        int count = points.Count;
        for (int i = 0; i < count; i++) 
        {
            GameObject point = points[i];
            float diff = GetDistanceFromOrigin(point) - standardDistance;
            Debug.Log("diff" + diff);
            float xDiff = Mathf.Cos(CalculateAngle(point)) * diff;
            Debug.Log("xDiff" + xDiff);
            float zDiff = Mathf.Sin(CalculateAngle(point)) * diff;
            Debug.Log("zDiff" + zDiff);

            Vector3 newPosition;
            if (point.transform.position.z >= 0)
            {
                newPosition = point.transform.position - new Vector3(xDiff, 0, zDiff);
            }
            else
            {
                newPosition = point.transform.position + new Vector3(-xDiff, 0, zDiff);
            }

            Debug.Log("initial position = " + point.transform.position);
            Debug.Log("position on circle = " + newPosition);
            //points[i].transform.position = 
            GameObject onCircleObject = GameObject.Instantiate(onCirclePrefab, circleLoopPoints);
            onCircleObject.name = point.name;
            onCircleObject.transform.position = newPosition;
            onCircleObject.GetComponent<pointInfo>().positionOnCircle = newPosition;
        }


        for (int i = 0; i < count; i++)
        {
            GameObject circlePoint = circleLoopPoints.GetChild(i).gameObject;
            if (circlePoint != null)
            {
                circlePoints.Add(circlePoint);
            }
        }


    }

    public void NoiseUpdateAll()
    {
        foreach(GameObject circlePoint in circlePoints)
        {
            NoiseUpdate(circlePoint);
        }
    }



    public void NoiseUpdate(GameObject point)
    {

        // Noise

        Vector3 pos;
        offsetX = point.GetComponent<pointInfo>().offsetX;
        offsetZ = point.GetComponent<pointInfo>().offsetZ;

        float distanceOffsetX = heightScaleX * Mathf.PerlinNoise((time + offsetX) * freqX , 0.0f);

        float distanceOffsetZ = heightScaleZ * Mathf.PerlinNoise((time + offsetZ) * freqZ, 0.0f);

        pos = point.GetComponent<pointInfo>().positionOnCircle;
        //pos = point.transform.position;
        pos += new Vector3(distanceOffsetX, 0, distanceOffsetZ);


        // Shrink

        if (pulse)
        {
            shrinkSpeed = Mathf.Sin(time * Mathf.PI / phaseDuration)/time*pulseAmplitude ;


        }

        if (point.GetComponent<pointInfo>().positionOnCircle.x > 0)
        {
            pos -= new Vector3((time) * shrinkSpeed - enlargeBonus * enlargeSpeed, 0, 0);
        }
        if (point.GetComponent<pointInfo>().positionOnCircle.x < 0)
        {
            pos += new Vector3((time) * shrinkSpeed - enlargeBonus * enlargeSpeed, 0, 0);
        }
        if (point.GetComponent<pointInfo>().positionOnCircle.z > 0)
        {
            pos -= new Vector3(0,0,(time) * shrinkSpeed - enlargeBonus * enlargeSpeed);
        }
        if (point.GetComponent<pointInfo>().positionOnCircle.z < 0)
        {
            pos += new Vector3(0, 0, (time) * shrinkSpeed - enlargeBonus * enlargeSpeed);
        }

        shrinked = (time) * shrinkSpeed - enlargeBonus * enlargeSpeed;

        // Move
        MovePoint(point, pos);

    }

    public void MovePoint(GameObject point, Vector3 newPosition)
    {
        // Move point
        point.transform.position = newPosition;

        // Update walls
        UpdateWallsAround(point);

    }

    public GameObject GetPreviousPoint(GameObject point)
    {
        int index = point.transform.GetSiblingIndex();
        int previousIndex = index - 1;
        if (previousIndex < 0)
        {
            previousIndex = point.transform.parent.childCount - 1;
        }
        return (point.transform.parent.GetChild(previousIndex).gameObject);
    }
    public GameObject GetNextPoint(GameObject point)
    {
        int index = point.transform.GetSiblingIndex();
        int nextIndex = index + 1;
        if (nextIndex > (point.transform.parent.childCount - 1))
        {
            nextIndex = 0;
        }
        return (point.transform.parent.GetChild(nextIndex).gameObject);
    }

    public void MovePoint1()
    {
        MovePoint(circlePoints[0], circlePoints[0].transform.position + new Vector3(0, 0, 10f));
    }

    public void BuildWalls()
    {
        GameObject previousPoint = null;
        GameObject currentPoint;

        int count = circlePoints.Count;
        for (int i = 0; i < count + 1; i++) // replace 1 by count
        {
            if (i < count)
            {
                currentPoint = circlePoints[i];
            }
            else // For the last wall
            {
                currentPoint = circlePoints[0];
            }


            if (previousPoint != null)
            {
                BuildWall(previousPoint, currentPoint);
            }

            previousPoint = currentPoint;
        }

    }

    private void BuildWall(GameObject previousPoint, GameObject currentPoint)
    {
        Vector3 wallPosition = (currentPoint.transform.position + previousPoint.transform.position) / 2;
        float wallLength = Vector3.Distance(currentPoint.transform.position, previousPoint.transform.position);
        //Debug.Log("wallLength " + wallLength);
        float wallRotation = Vector3.SignedAngle(currentPoint.transform.position, previousPoint.transform.position, Vector3.right);
        //Debug.Log("wallRotation= " + wallRotation);
        GameObject wall = GameObject.Instantiate(wallPrefab, wallPosition, Quaternion.Euler(0, 0, 0), loopWalls);
        currentPoint.GetComponent<pointInfo>().previousWall = wall;
        previousPoint.GetComponent<pointInfo>().nextWall = wall;
        wall.transform.LookAt(currentPoint.transform);
        wall.transform.Rotate(0, 90f, 0);
        wall.transform.localScale = new Vector3(wallLength, 1, 0.1f);
    }

    private void UpdateWallsAround(GameObject point)
    {
        // previous wall

        Vector3 wallPosition = (point.transform.position + GetPreviousPoint(point).transform.position) / 2;
        float wallLength = Vector3.Distance(point.transform.position, GetPreviousPoint(point).transform.position);
        //Debug.Log("wallLength " + wallLength);
        float wallRotation = Vector3.SignedAngle(point.transform.position, GetPreviousPoint(point).transform.position, Vector3.right);
        //Debug.Log("wallRotation= " + wallRotation);
        GameObject wall = point.GetComponent<pointInfo>().previousWall;
        wall.transform.position = wallPosition;
        wall.transform.LookAt(point.transform);
        wall.transform.Rotate(0, 90f, 0);
        wall.transform.localScale = new Vector3(wallLength, 1, 0.1f);

        // next wall

        wallPosition = (point.transform.position + GetNextPoint(point).transform.position) / 2;
        wallLength = Vector3.Distance(point.transform.position, GetNextPoint(point).transform.position);
        //Debug.Log("wallLength " + wallLength);
        wallRotation = Vector3.SignedAngle(point.transform.position, GetNextPoint(point).transform.position, Vector3.right);
        //Debug.Log("wallRotation= " + wallRotation);
        wall = point.GetComponent<pointInfo>().nextWall;
        wall.transform.position = wallPosition;
        wall.transform.LookAt(point.transform);
        wall.transform.Rotate(0, 90f, 0);
        wall.transform.localScale = new Vector3(wallLength, 1, 0.1f);
    }

    float CalculateAngle(GameObject point)
    {
        float angle = Vector3.SignedAngle(point.transform.position, Vector3.right, Vector3.right);
        //Debug.Log(point);
        //Debug.Log(angle);
        angle = Mathf.Deg2Rad * angle;
        return angle;
    }


    public void CalculateAngle1()
    {
        CalculateAngle(points[0]);
    }

    public float GetDistanceFromOrigin(GameObject point)
    {
        float distance = Vector3.Distance(point.transform.position, origin.transform.position);
        return distance;
    }
}
