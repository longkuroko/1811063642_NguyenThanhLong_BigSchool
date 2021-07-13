using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class UnfollowController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public UnfollowController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UnFollow(FolowingDto followDto)
        {
            var userId = User.Identity.GetUserId();

            var DelFollowing = _dbContext
                .Followings
                .FirstOrDefault(a => a.FollowerId == userId && a.FolloweeId == followDto.FolloweeId);

            if (DelFollowing == null)
            {
                return BadRequest("The Follow  is not exists!");
            }
            _dbContext.Followings.Remove(DelFollowing);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
