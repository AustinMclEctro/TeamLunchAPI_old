using System;
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
    /// The dietary restrictions string is a character sequence describing restrictions.
    /// Largest possible string: "vgnf" - vegitarian, gluten-free, nut-free, fish-free.
    /// </summary>
    public Dictionary<int, string> TeamMembers = new Dictionary<int, string>();

    /// <summary>
    /// List of restaurants.
    /// </summary>
    public List<Restaurant> Restaurants = new List<Restaurant>();

    private static Data instance = new Data();

    static Data()
    {
    }

    private Data()
    {
    }

    public static Data Instance { get { return instance; } }
}
