/*
    This file will be deleted eventually
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.Permissions
{
    class UserSkipPermissions
    {
        private static Dictionary<string, Permissions> permissions = new Dictionary<string, Permissions>()
        {
            {"category", new Permissions("location", "sub_category", "amenities") },
            {"sub_category", new Permissions("amenities", "category", "location")},
            {"amenities", new Permissions("category", "sub_category", "location")},
            {"location", new Permissions("category", "sub_category", "amenities") },
            {"distance", new Permissions("category", "sub_category", "amenities", "location") }

        };
        public static Permissions GetPermissions(string CurrentScreen)
        {
            return permissions[CurrentScreen];
            

        }

        internal class Permissions
        {
            public List<string> permissions { get; set; }
            public Permissions(params string[] allowedPermissions)
            {
                this.permissions = new List<string>(allowedPermissions);
            }
        }
    }
}
