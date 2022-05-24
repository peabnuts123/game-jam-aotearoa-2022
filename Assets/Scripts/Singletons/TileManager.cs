using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class TileManager : MonoBehaviour
{
    // Public enums
    public enum TimeOfDay
    {
        Day,
        Night,
    }

    // Public references
    [NotNull]
    [SerializeField]
    private Grid nightTilePalette;
    [NotNull]
    [SerializeField]
    private Grid dayTilePalette;
    [NotNull]
    [SerializeField]
    private Tilemap[] tilemaps;

    // Private references
    [Inject]
    private TimeController timeController;

    // Private state
    private TimeOfDay currentTimeOfDay = TimeOfDay.Day;


    void Start()
    {
        timeController.OnDayTime -= OnDayTime;
        timeController.OnDayTime += OnDayTime;
        timeController.OnNightTime -= OnNightTime;
        timeController.OnNightTime += OnNightTime;
    }

    private void SetPalette(TimeOfDay timeOfDay)
    {
        Tilemap currentTilePalette, newTilePalette;

        switch (timeOfDay)
        {
            case TimeOfDay.Day:
                currentTilePalette = nightTilePalette.GetComponentInChildren<Tilemap>();
                newTilePalette = dayTilePalette.GetComponentInChildren<Tilemap>();
                break;
            case TimeOfDay.Night:
                currentTilePalette = dayTilePalette.GetComponentInChildren<Tilemap>();
                newTilePalette = nightTilePalette.GetComponentInChildren<Tilemap>();
                break;
            default:
                throw new NotImplementedException($"Unhandled time of day: {timeOfDay}");
        }

        foreach (var paletteCoordinate in currentTilePalette.cellBounds.allPositionsWithin)
        {
            var currentTile = currentTilePalette.GetTile(paletteCoordinate);
            var newTile = newTilePalette.GetTile(paletteCoordinate);
            if (currentTile == null && newTile != null)
            {
                // Current tile is null
                Debug.LogError($"Cannot swap tiles. Tile at coordinate {paletteCoordinate} is null in current palette '{currentTilePalette.name}' but not null in new palette '{newTilePalette.name}'");
            }
            else if (currentTile != null && newTile == null)
            {
                // New tile is null
                Debug.LogError($"Cannot swap tiles. Tile at coordinate {paletteCoordinate} is null in new palette '{newTilePalette.name}' but not null in current palette '{currentTilePalette.name}'");
            }
            else if (currentTile != null && newTile != null)
            {
                // Neither tile is null - ready to swap in each tilemap
                foreach (var tilemap in tilemaps)
                {
                    // var tile = paletteTilemap.GetTile(paletteCoordinate);
                    tilemap.SwapTile(currentTile, newTile);

                }
            } // else both tiles are null - this is fine
        }

        currentTimeOfDay = timeOfDay;
    }

    private void OnDayTime()
    {
        SetPalette(TimeOfDay.Day);
    }
    private void OnNightTime()
    {
        SetPalette(TimeOfDay.Night);
    }
}