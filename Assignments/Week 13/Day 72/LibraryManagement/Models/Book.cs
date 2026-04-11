using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    [JsonIgnore]
    public virtual Author? Author { get; set; }
}
