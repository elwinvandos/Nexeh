using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexeh.helpers;

// See https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
public static class AStarAlgorithm
{
    public static Location Compute(Vector2I startVector, Vector2I targetVector, TileMap map)
    {
        Location current = new();

        var start = new Location(startVector);
        var target = new Location(targetVector);
        var openList = new List<Location>();
        var closedList = new List<Location>();
        var g = 0;

        openList.Add(start);

        while (openList.Count > 0)
        {
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            closedList.Add(current);
            openList.Remove(current);

            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) is not null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.Find(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.Find(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X,
                        adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }
        return current;
    }

    private static List<Location> GetWalkableAdjacentSquares(int x, int y, TileMap map)
    {
        var proposedLocations = new List<Location>()
    {
        new() { X = x, Y = y - 1 },
        new() { X = x, Y = y + 1 },
        new() { X = x - 1, Y = y },
        new() { X = x + 1, Y = y },
    };

        // Also check wall and props layers
        return proposedLocations
            .Where(l => 
             map.GetCellSourceId(0, new Vector2I(l.X, l.Y)) == -1 &&
             map.GetCellSourceId(1, new Vector2I(l.X, l.Y)) == -1 &&
             map.GetCellSourceId(2, new Vector2I(l.X, l.Y)) == -1)
            .ToList();
    }

    private static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }
}

public class Location
{
    public Location()
    {
        
    }

    public Location(Vector2I vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public Location Parent;
}
