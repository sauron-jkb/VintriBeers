
using System.Collections.Generic;
using System.IO;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using VintriBeers.Domain.Model;

namespace VintriBeers.Domain.Data
{
    public class UserRatingsContext : IUserRatingsContext
    {
        private readonly string _userRatingsContextFilePath = string.Empty; 

        public UserRatingsContext(IConfigurationRoot configuration)
        {
            _userRatingsContextFilePath = configuration["UserRatingsContextFilePath"];

            string jsonFromFile;
            using (var reader = new StreamReader(_userRatingsContextFilePath))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            
            UserRatings = JsonConvert.DeserializeObject<List<UserRating>>(jsonFromFile);
            if (UserRatings == null)
            {
                UserRatings = new List<UserRating>();
            }
        }

        public virtual List<UserRating> UserRatings { get; set; }

        public void AddUserRating(UserRating userRating)
        {
            UserRatings.Add(userRating);

            var serializer = new JsonSerializer();

            using (var streamWriter = new StreamWriter(_userRatingsContextFilePath))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(streamWriter, UserRatings);
            }
        }
    }
}
