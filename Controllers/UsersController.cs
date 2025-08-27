using BackEndGatoMia.Models;
using Microsoft.AspNetCore.Mvc;
namespace BackEndGatoMia.UserContollers
{
    [ApiController]
    [Route("api/users")]
    public class UserContollers : ControllerBase
    {
        private readonly UserService _userService;

        public UserContollers(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userService.AddUser(user);

            return Ok(new { message = "Usuário criado com sucesso" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new { message = "O ID na rota não corresponde ao ID do usuário no corpo da requisição" });
            }
            try
            {
                await _userService.UpdateUser(user);
                return Ok(new { message = "Usuário atualizado com sucesso!" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "O ID do usuário é obrigatório para a deleção" });
            }
            try
            {
                await _userService.DeleteUser(id, "ID_USUARIO_LOGADO");

                return Ok(new { message = "Usuário deletado com sucesso!" });
            }
            catch (InvalidDataException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno", error = ex.Message });
            }
        }
    }
}