using BucklesChatBackend.Database;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BucklesChatBackend.Models.Entities;
using BucklesChatBackend.Repositories;
using BucklesChatBackend.API;
using BucklesChatBackend.Encryption;
using MongoDB.Driver;
using BucklesChatBackend.Models.DTO;

namespace BucklesChatBackend.Controllers.User
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("does_username_exist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> DoesUsernameExist([FromQuery] string username)
        {
            var id = EncryptionConstants.GenerateId();
            try
            {
                return Ok(new ApiResponse(id, _userRepository.DoesUserWithUsernameExist(username)));
            } catch (Exception ex)
            {
                return BadRequest(new ApiResponse(id, false, new ApiErrorInfo(id, ApiErrorCodes.USERNAME_DOES_EXIST, ex)));
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> Create([FromBody] JObject Request)
        {
            var id = EncryptionConstants.GenerateId();
            try
            {
                var convertedUser = JsonConvert.DeserializeObject<BucklesChatUser>(Request.ToString());

                if (convertedUser == null) return BadRequest("Cannot send empty body");

                if (_userRepository.DoesUserWithUsernameExist(convertedUser.Username ?? "")) return BadRequest("Username already exists");

                var addResults = _userRepository.AddUser(convertedUser);

                if (addResults == null)
                {
                    return Ok(new ApiResponse(id, true));
                }

                return BadRequest(new ApiResponse(id, false));
            } catch (Exception ex)
            {
                return BadRequest(new ApiResponse(id, ex));
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> TestConnection()
        {
            return Ok(new ApiResponse("Systems online"));
        }

        [HttpGet("allUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> GetAllUsers()
        {
            var id = EncryptionConstants.GenerateId();
            try
            {
                var allUsers = _userRepository.GetAllLocalUsers();
                return Ok(new ApiResponse(id, allUsers));
            } catch (Exception ex)
            {
                return BadRequest(new ApiResponse(id, ex));
            }
        }


    }
}
