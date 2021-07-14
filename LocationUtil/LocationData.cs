﻿// Used for syncing only the necessary data
internal class LocationData
{
    public string LocationName { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public LocationData(string locationName, float x, float y)
    {
        this.LocationName = locationName;
        this.X = x;
        this.Y = y;
    }
}
