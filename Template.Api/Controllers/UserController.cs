using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Template.Api.Responses;
using Template.Core.CustomEntities;
using Template.Core.DTOs;
using Template.Core.Entities;
using Template.Core.Interfaces;
using Template.Core.QueryFilters;
using Template.Infraestructure.Interfaces;

namespace Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
     

        public UserController(IUserService userService, IMapper mapper, IUriService uriService)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
        }


        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetUsers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsers([FromQuery] UserQueryFilter filters)
        {
            var users = _userService.GetUsers(filters);
            var usersDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            var metadata = new Metadata
            {
                TotalCount = users.TotalCount,
                PageSize = users.PageSize,
                CurrentPage = users.CurrentPage,
                TotalPages = users.TotalPages,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage,
                NextPageUrl = _uriService.GetUserPaginationUri(filters, Url.RouteUrl(nameof(GetUsers))).ToString(),
                PreviousPageUrl = _uriService.GetUserPaginationUri(filters, Url.RouteUrl(nameof(GetUsers))).ToString()
            };

            var response = new ApiResponse<IEnumerable<UserDto>>(usersDtos)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }




        [HttpPost]
        public async Task<ActionResult> Post(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userService.InsertUser(user);

            userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
