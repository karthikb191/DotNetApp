using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Applicaiton.Activities.Queries;

public class GetActivityList
{
    public class Query : IRequest<List<Domain.Activity>>
    {

    }

    /*The actual logic of communicating with database happens here*/
    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Domain.Activity>>
    {
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Activities.ToListAsync(cancellationToken);
        }
    }
}