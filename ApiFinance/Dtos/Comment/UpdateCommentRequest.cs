﻿using System.ComponentModel.DataAnnotations;

namespace ApiFinance.Dtos.Comment
{
    public class UpdateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(288, ErrorMessage = "Title cannot be over 288 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(288, ErrorMessage = "Content cannot be over 288 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
