﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dal.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("User")]
        public int? AuthorId { get; set; }

        public string AuthorName { get; set; }

        public User User { get; set; }

        public IList<Comment> Comments { get; set; }

    }
}
