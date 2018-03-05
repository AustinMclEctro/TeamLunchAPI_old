﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// Singleton class which holds all submitted TeamMembers and Restaurants.
/// </summary>
public sealed class Data
{
    /// <summary>
    /// Dictionary of team members.
    /// Key: int, employee id.
    /// Value: string, dietary restrictions.<para />
    /// FINISH DESCRIPTION
    /// </summary>
    public Dictionary<string, string> TeamMembers = new Dictionary<string, string>();

    /// <summary>
    /// List of restaurants.
    /// </summary>
    public List<Restaurant> Restaurants = new List<Restaurant>();

    /// <summary>
    /// Returns instance of restaurant with given name.
    /// Returns null if none is found.
    /// </summary>
    public Restaurant GetRestaurant(string name)
    {
        foreach (Restaurant r in Restaurants)
        {
            if (r.name == name)
                return r;
        }
        return null;
    }

    private static Data instance = new Data();

    static Data()
    {
    }

    private Data()
    {
    }

    public static Data Instance { get { return instance; } }
}
