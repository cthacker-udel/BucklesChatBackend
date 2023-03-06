using BucklesChatBackend.API;
using BucklesChatBackend.Encryption;
using BucklesChatBackend.Models.DTO;
using BucklesChatBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BucklesChatBackend.Controllers.User
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("exists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> DoesUsernameExist([FromQuery] string username)
        {
            string id = EncryptionConstants.GenerateId();
            try
            {
                return Ok(new ApiResponse(id, _userRepository.DoesUserWithUsernameExist(username)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(id, false, new ApiErrorInfo(id, ApiErrorCodes.USERNAME_DOES_EXIST, ex)));
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> Create([FromBody] BucklesChatUser convertedUser)
        {
            string id = EncryptionConstants.GenerateId();
            try
            {
                if (convertedUser == null || convertedUser.Password == null) return BadRequest("Cannot send invalid payload");

                if (_userRepository.DoesUserWithUsernameExist(convertedUser.Username ?? "")) return BadRequest("Username already exists");

                Tuple<string, string> encryptedPasswordResults = PasswordEncryption.EncryptPassword(convertedUser.Password);

                convertedUser.Password = encryptedPasswordResults.Item2;
                convertedUser.PasswordSalt = encryptedPasswordResults.Item1;

                bool addResults = _userRepository.AddUser(convertedUser).Result;

                if (addResults)
                {
                    return Ok(new ApiResponse(id, true));
                }

                return BadRequest(new ApiResponse(id, false));
            }
            catch (Exception ex)
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
            string id = EncryptionConstants.GenerateId();
            try
            {
                IEnumerable<BucklesChatUser> allUsers = _userRepository.GetAllLocalUsers();
                return Ok(new ApiResponse(id, allUsers));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(id, ex));
            }
        }


    }
}
