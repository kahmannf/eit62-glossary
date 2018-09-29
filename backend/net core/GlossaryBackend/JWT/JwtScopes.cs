using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GlossaryBackend.JWT
{
    public static class JwtScopes
    {
        public const string Admin = "Admin";


        public static string[] Scopes = GetScopes();

        private static string[] GetScopes() 
            => typeof(JwtScopes)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue()).ToArray();
    }
}