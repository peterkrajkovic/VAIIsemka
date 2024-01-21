﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string Username { get; set; }
        public string? Caption { get; set; }
        public string? Location { get; set; }
        [Required]
        public int LikeCount { get; set; }
        [Required]
        public int CommentCount { get; set; }
        [Required]
        public byte[]? Image1 { get; set; }
        public byte[]? Image2 { get; set; }
        public byte[]? Image3 { get; set; }
        public DateTime Date { get; set; }
    }
}
