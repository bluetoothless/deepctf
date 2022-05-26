using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class PlaceFlagScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject incorrectPlacementText;

    Vector3 position;
    Plane field = new Plane(Vector3.up, 0);

    bool buttonClicked = false;

    private void Update()
    {
        if (buttonClicked)
        {
            if (Input.GetMouseButtonDown(0)) {
                float distance;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (field.Raycast(ray, out distance))
                {
                    position = ray.GetPoint(distance);
                }
                Debug.Log("Position: " + position);

                if (CheckCorrectPlacement(position))
                {
                    Debug.Log("Place at Position: " + position);
                }
                else
                {
                    incorrectPlacementText.SetActive(true);
                }

                buttonClicked = false;
            }
        }
    }
    public void PlaceFlag()
    {
        incorrectPlacementText.SetActive(false);
        buttonClicked = true;
    }

    public bool CheckCorrectPlacement(Vector3 position)
    {
        // if out of field range return false
        if ((position.z > PerlinNoiseMapGeneration.GetWidth() / 2) || (position.z < -PerlinNoiseMapGeneration.GetWidth() / 2)
            || (position.x > PerlinNoiseMapGeneration.GetHeight() / 2) || (position.x < -PerlinNoiseMapGeneration.GetHeight() / 2))
        {
            Debug.Log("Cant place on edge");
            return false;
        }
        //  pos = -180/0/-200 to 180/0/200
        Tile tile = GetTileFromPosition(position);
        // check if 
        if (Enumerable.Any(PerlinNoiseMapGeneration.GetNoLakeDomains(), d => d.domain == tile.domain))
        {
            List<Tile> possibleBaseLocations = PerlinNoiseMapGeneration.GetPossibleBaseLocationsInDomain(PerlinNoiseMapGeneration.GetNoLakeDomains()[tile.domain]);
            if (Enumerable.Any(possibleBaseLocations, location => location == tile))
            {
                Debug.Log("Can be Placed!");
                return true;
            }
            else
            {
                Debug.Log("Cant place here, needs 3x3 space"); // ADD SOMETHING ABOUT DISTANCE BETWEEN BASES AND SAME DOMAIN
            }
        }
        else
        {
            Debug.Log("Cant place in the water");
        }
        return false;
    }

    Tile GetTileFromPosition(Vector3 position)
    {
        int x, y;
        x = (int)Math.Floor(position.x / 4) + 45;
        y = (int)Math.Floor(position.z / 4) + 50;

        int index = x * 100 + y;
        return PerlinNoiseMapGeneration.GetTilesList()[index];
    }
}
