using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController(AppDbContext context) : BaseApiController
{
    //We want accesst to DbContext in this class as we want to query the database

    [HttpGet]
    public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
    {
        return await context.Activities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Activity>> GetActivity(string id)
    {
        var activity = await context.Activities.FindAsync(id);

        if (activity == null) return NotFound();

        return activity;
    }
}
