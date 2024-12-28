using System;
using UnityEngine;

public class RoadPiece:IEquatable<RoadPiece>
{
    public enum RoadType { STRAIGHT,CROSS,CORNER,TJUNC}

    //data that defines a road piece
    public GameObject roadObject;
    public Vector3Int position;
    public RoadType type;
    public int yRotation;


    public bool Equals(RoadPiece otherPiece)
    {
        return (position == otherPiece.position) && (type == otherPiece.type) && (yRotation == otherPiece.yRotation);   
    }
}
