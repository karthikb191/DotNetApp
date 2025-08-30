using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Applicaiton.Activities.Queries;
using Applicaiton.Activities.Commands;
using Microsoft.CodeAnalysis.Differencing;

namespace API.Controllers;

public class ActivitiesController() : BaseApiController
{
    //We want accesst to DbContext in this class as we want to query the database

    [HttpGet]
    public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
    {
        // Send query to the appropriate mediator
        return await Mediator.Send(new GetActivityList.Query());
        //return await context.Activities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Activity>> GetActivityDetail(string id)
    {
        return await Mediator.Send(new GetActivityDetails.Query { Id = id });
        //var activity = await context.Activities.FindAsync(id);
        //if (activity == null) return NotFound();
        //return activity;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(Domain.Activity activity)
    {
        return await Mediator.Send(new CreateActivity.Command() { Activity = activity });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(Domain.Activity activity)
    {
        await Mediator.Send(new EditActivity.Command() { Activity = activity });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteActivity.Command { Id = id });
        return Ok();
    }
}
