using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.InputSystem.Utilities;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public Vector2 bombOffset;// vector for offset
    public float bombSpawnWaitTime = 3f; // time for the bomb to wait before spawning
    public IEnumerator bombWaitCoroutine; // coroutine for the bomb
    void Start()
    {
        Debug.Log(dotProduct(transform.up,enemyTransform.up));
       
    }
    // Update is called once per frame
    void Update()
    {
        bombOffset = transform.position + transform.up; // offet of the bomb
        // checks if the b key is pressed
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            bombWaitCoroutine = waitToSpawnBomb(bombSpawnWaitTime, bombOffset); // sets the bomb coroutine at the start of the game
            SpawnBombAtOffset(bombOffset); // spawns a bomb

        }
    }
    // function that will be used to spawn bomb at an offset
    public void SpawnBombAtOffset(Vector3 inOffset) { 
 
        StartCoroutine(bombWaitCoroutine); // only starts this isntance of the coroutine
        
    }
    //coroutine that stops
    private IEnumerator waitToSpawnBomb(float waitTimer,Vector3 inOffset)
    {

        yield return new WaitForSeconds(waitTimer); // waits set time
        Instantiate(bombPrefab, inOffset, Quaternion.identity);// spawns bomb
        
    }

    public Vector2 NormalizeCustom(Vector2 inVector)
    {
        float mag = inVector.magnitude;
        Vector2 normalize = new Vector2(inVector.x / Mathf.Abs(mag), inVector.y / Mathf.Abs(mag));
        return normalize;
    }
    public float dotProduct(Vector2 inVector, Vector2 enemyVector)
    {
        float mag = inVector.magnitude;
     
        float enemyMag = enemyVector.magnitude;
        // dot product for finding the angle
        float dotprod = ((inVector.x * enemyVector.x) + (inVector.y * enemyVector.y)) / Mathf.Abs(mag) * Mathf.Abs(enemyMag);
        return Mathf.Cos(dotprod); ;
    }
}
