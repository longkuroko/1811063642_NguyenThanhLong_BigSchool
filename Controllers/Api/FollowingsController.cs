using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{

    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        
        [HttpPost]
        public IHttpActionResult Follow(FolowingDto followingDto)
        {
            //var userId = User.Identity.GetUserId();
            //if (_dbContext.Followings.Any(s => s.FollowerId == userId && s.FolloweeId == followingDto.FolloweeId))
            //    return BadRequest("Following already exists!");

            //var following = new Following
            //{
            //    FollowerId = userId,
            //    FolloweeId = followingDto.FolloweeId
            //};

            //_dbContext.Followings.Add(following);
            //_dbContext.SaveChanges();

            //return Ok();
            var userId = User.Identity.GetUserId();

            if (userId == followingDto.FolloweeId)
            {
                return BadRequest("Bạn không thể thực hiện thao tác với chính bản thân!");
            }

            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                _dbContext.Followings.Remove(_dbContext.Followings.Where(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId).FirstOrDefault());
                _dbContext.SaveChanges();
                return Ok("Đã hủy Follow!");
            }



            var follwing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId,
            };

            _dbContext.Followings.Add(follwing);
            _dbContext.SaveChanges();
            return Ok("Đã Follow thành công!");
        }


    }


}
