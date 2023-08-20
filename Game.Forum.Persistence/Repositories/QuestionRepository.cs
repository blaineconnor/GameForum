using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Forum.Persistence.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly DbSet<Question> _dbSet;

        public QuestionRepository(GameForumContext context) : base(context)
        {
            _dbSet = context.Set<Question>();
        }

        public async Task<PaginationResponse<GetAllQuestions>> GetNewestQuestions(Pagination pagination)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = pagination.PageSize;
            var page = pagination.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData) / Convert.ToDecimal(pageSize));
            var paginationResponse = new PaginationResponse<GetAllQuestions>
            {
                Pagination = new Pagination
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalData = totalData,
                    TotalPage = Convert.ToInt32(totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Id)
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestions
                 {
                     Title = p.Title,
                     View = p.QuestionViews.Count(),
                     Content = p.Content,
                     Category = p.Category,
                     Answer = p.Answers.Count(),
                     Vote = p.Votes.Where(y => y.Voted == true).Count() - p.Votes.Where(z => z.Voted == false).Count(),
                     User = new UserResponse
                     {
                         Name = p.User.Name,
                         Id = p.User.Id,
                         Surname = p.User.Surname,
                         Image = p.User.Image
                     }
                 }).ToListAsync()
            };
            return paginationResponse;
        }
        public async Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingVote(Pagination pagination)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = pagination.PageSize;
            var page = pagination.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData) / Convert.ToDecimal(pageSize));
            var paginationResponse = new PaginationResponse<GetAllQuestions>
            {
                Pagination = new Pagination
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalData = totalData,
                    TotalPage = Convert.ToInt32(totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Votes.Where(y => y.Voted == true).Count() - c.Votes.Where(z => z.Voted == false).Count())
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestions
                 {
                     Title = p.Title,
                     View = p.QuestionViews.Count(),
                     Content = p.Content,
                     Category = p.Category,
                     Answer = p.Answers.Count(),
                     Vote = p.Votes.Where(y => y.Voted == true).Count() - p.Votes.Where(z => z.Voted == false).Count(),
                     User = new UserResponse
                     {
                         Name = p.User.Name,
                         Id = p.User.Id,
                         Surname = p.User.Surname,
                         Image = p.User.Image
                     }
                 }).ToListAsync()
            };
            return paginationResponse;
        }

        public async Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingAnswer(Pagination pagination)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = pagination.PageSize;
            var page = pagination.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData) / Convert.ToDecimal(pageSize));
            var paginationResponse = new PaginationResponse<GetAllQuestions>
            {
                Pagination = new Pagination
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalData = totalData,
                    TotalPage = Convert.ToInt32(totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Answers.Count)
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestions
                 {
                     Title = p.Title,
                     View = p.QuestionViews.Count(),
                     Content = p.Content,
                     Category = p.Category,
                     Answer = p.Answers.Count(),
                     Vote = p.Votes.Where(y => y.Voted == true).Count() - p.Votes.Where(z => z.Voted == false).Count(),
                     User = new UserResponse
                     {
                         Name = p.User.Name,
                         Id = p.User.Id,
                         Surname = p.User.Surname,
                         Image = p.User.Image
                     }
                 }).ToListAsync()
            };
            return paginationResponse;
        }


        public async Task<QuestionDetailResponse> GetQuestionsWithDetail(int id)
        {
            var questionDetail = await _dbSet.Where(x => x.Id == id).Select(p => new QuestionDetailResponse
            {
                Title = p.Title,
                View = p.QuestionViews.Count(),
                Content = p.Content,
                Category = p.Category,
                Answer = p.Answers.Count(),
                User = new UserResponse
                {
                    Name = p.User.Name,
                    Id = p.User.Id,
                    Surname = p.User.Surname,
                    Image = p.User.Image
                },
                AnswerResponse = p.Answers.Select(q => new AnswerResponse
                {
                    User = new UserResponse
                    {
                        Name = q.User.Name,
                        Id = q.User.Id,
                        Surname = q.User.Surname,
                        Image = q.User.Image
                    },
                    Content = q.Content,
                    CreatedDateTime = q.CreatedTime
                }).ToList(),
                Favorite = p.Favorites.Where(x => x.IsDeleted == false).Count(),


            }).FirstOrDefaultAsync();
            return questionDetail;

        }

        
    }
}





