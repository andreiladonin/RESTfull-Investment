using ApiFinance.Dtos.Comment;
using ApiFinance.Models;

namespace ApiFinance.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment) {

            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommentFromCreateDTO (this CreateCommentRequest request, int stockId)
        {
            return new Comment
            {
                Title = request.Title,
                Content = request.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdateDTO(this UpdateCommentRequest request)
        {
            return new Comment
            {
                Title = request.Title,
                Content = request.Content,
            };
        }
    }
}
