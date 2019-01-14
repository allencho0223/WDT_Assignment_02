using CineplexCinemas.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace System
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, List<cartItem> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetBookingSession(this ISession session, string key, List<int> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static List<T> GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(List<T>) : JsonConvert.DeserializeObject<List<T>>(value);
        }
    }
}