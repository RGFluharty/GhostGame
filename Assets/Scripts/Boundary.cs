using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This script is assigned to the Boundary tilemap to prevent the player from flying off the level as a ghost
// It will kill the player and restart the level when the player collides with an object with this component

public class Boundary : MonoBehaviour
{
    [SerializeField] float iceCubes = 2f;

}
