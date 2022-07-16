using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSquare
{

    public float minX, minY, maxX, maxY;
    private readonly static float BOUNDARIES = 0.5f;
    private int regionNumber;
    public CoordinateSquare(int minX, int maxX, int maxY, int minY, int regionNumber)
    {
        this.minX = minX + BOUNDARIES;
        this.minY = minY + BOUNDARIES;
        this.maxX = maxX - BOUNDARIES;
        this.maxY = maxY - BOUNDARIES;
        this.regionNumber = regionNumber;
    }

    public Vector2 RandomCoordinate()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public int getRegionNumber()
    {
        return regionNumber;
    }
}