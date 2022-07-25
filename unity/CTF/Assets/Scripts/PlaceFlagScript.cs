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
    [SerializeField] string color;

    Vector3 position;
    Plane field = new Plane(Vector3.up, 0);

    bool buttonClicked = false;

    private void Update()
    {
        if (!buttonClicked)
            return;

        if (!Input.GetMouseButtonDown(0)) //Input.GetMouseButtonDown(0)
            return;

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

            if (color == "blue")
            {
                PerlinNoiseMapGeneration.SetBlueBaseOnTile(GetTileFromPosition(position));
            }
            else // red base clicked
            {
                PerlinNoiseMapGeneration.SetRedBaseOnTile(GetTileFromPosition(position));
            }
        }
        else
        {
            incorrectPlacementText.SetActive(true);
        }

        buttonClicked = false;

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
        var NoLakeDomains = PerlinNoiseMapGeneration.GetNoLakeDomains();
        // what is tile.domain index in GetNoLakeDomains List (xx) and if it exists in the first place
        int indexOfDomain = NoLakeDomains.FindIndex(d => d.domain == tile.domain);
        if (indexOfDomain != -1)
        {
            List<Tile> possibleBaseLocations = PerlinNoiseMapGeneration.GetPossibleBaseLocationsInDomain(NoLakeDomains[indexOfDomain]);
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
