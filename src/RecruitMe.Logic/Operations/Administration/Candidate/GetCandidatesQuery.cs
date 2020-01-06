using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class GetCandidatesQuery : BaseOperation<PagedResponse<GetCandidatesResultDto>, PagingParameters>
    {
        private readonly BusinessConfiguration _businessConfiguration;

        public GetCandidatesQuery(ILogger logger, BaseDbContext dbContext, BusinessConfiguration businessConfiguration) : base(logger, dbContext)
        {
            this._businessConfiguration = businessConfiguration;
        }

        public override PagedResponse<GetCandidatesResultDto> Execute(PagingParameters request)
        {
            var dataTask = Get(
                _dbContext.Users.Where(u => u.CandidateId != _businessConfiguration.AdminLogin),
                request)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var countTask = _dbContext.Users.CountAsync();
            Task.WhenAll(dataTask, countTask).Wait();
            var count = countTask.Result;
            var data = dataTask.Result;

            return new PagedResponse<GetCandidatesResultDto>()
            {
                Page = request.Page,
                TotalCount = count,
                Data = data.Select(u => new GetCandidatesResultDto()
                {
                    Id = u.Id,
                    CandidateId = u.CandidateId,
                    Email = u.Email,
                    Name = u.Name,
                    Surname = u.Surname
                })
            };
        }

        private IQueryable<User> Get(IQueryable<User> data, PagingParameters request)
        {
            if (string.IsNullOrEmpty(request.SortBy))
                return data;

            switch (request.SortBy)
            {
                case "candidateId":
                    if (request.SortDesc.HasValue && request.SortDesc.Value)
                        return data.OrderByDescending(x => x.CandidateId);
                    else
                        return data.OrderBy(x => x.CandidateId);
                case "name":
                    if (request.SortDesc.HasValue && request.SortDesc.Value)
                        return data.OrderByDescending(x => x.Name);
                    else
                        return data.OrderBy(x => x.Name);
                case "surname":
                    if (request.SortDesc.HasValue && request.SortDesc.Value)
                        return data.OrderByDescending(x => x.Surname);
                    else
                        return data.OrderBy(x => x.Surname);
                case "email":
                    if (request.SortDesc.HasValue && request.SortDesc.Value)
                        return data.OrderByDescending(x => x.Email);
                    else
                        return data.OrderBy(x => x.Email);
                default:
                    return data;
            }
        }
    }
}
