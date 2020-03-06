using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwittAPI.Models
{
    public partial class Comment
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public int ProfileId { get; set; }
        [JsonIgnore]
        public int PostId { get; set; }
        [JsonIgnore]
        public virtual Post Post { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
