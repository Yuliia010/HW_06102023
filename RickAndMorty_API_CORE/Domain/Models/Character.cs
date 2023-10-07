using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty_API_CORE.Domain.Models
{
    public class Character
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Species { get; set; }
        public string? Gender { get; set; }
        public string? Location { get; set; }
        public string? ImageURL { get; set; }
        public Character()
        {
            
        }

        public Character(string id, string? name, string? status, string? species, string? gender, string? location, string? imageURL)
        {
            Id = id;
            Name = name;
            Status = status;
            Species = species;
            Gender = gender;
            Location = location;
            ImageURL = imageURL;
        }
    }
}
