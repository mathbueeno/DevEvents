using DevEvents.API.Data;
using DevEvents.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevEvents.API.Controllers
{
	// Coloca este nome na rota

	[Route("api/dev-events")]
	[ApiController]
	public class DevEventsController : ControllerBase
	{
		// Injeção de Dependência através do Singleton
		// Método Construtor

		private readonly DevEventsContext _context;
		public DevEventsController(DevEventsContext context)
		{
			_context = context;
		}


		//Pontos de Acesso


		
		/// <summary>
		/// Obter todos os eventos
		/// </summary>
		/// <returns>Coleção de eventos</returns>
		/// <response code= "200">Sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult GetAll()
		{
			// Efetua a busca daqueles eventos que NÃO estão cancelados
			// Claúsula 
			var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
			return Ok(devEvents);
		}

		// Buscar pelo ID
		// Recebe o parâmetro ID
		/// <summary>
		/// Obter um evento
		/// </summary>
		/// <param name="id">Identificador do evento</param>
		/// <returns>Dados sobre os eventos</returns>
		/// <response code = "200">Sucesso</response>
		/// /// <response code = "404">Não encontrado</response>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult GetById(Guid id)
		{
			//Busca onde o ID for igual a ID
			var devEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

			// SE  devEvents for igual a nulo, retorne NotFound
			if (devEvents == null)
			{
				return NotFound();
			}

			return Ok(devEvents);
		}

		/// <summary>
		/// Cadastro do Evento
		/// </summary>
		/// <remarks>
		/// {objeto JSON}
		/// </remarks>
		/// <param name="devEvent"></param>
		/// <returns>Objeto recem criado</returns>
		/// <response code = "201">Sucesso</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public ActionResult Post(DevEvent devEvent)
		{
			_context.DevEvents.Add(devEvent);

			//está colocando um nome para o objeto que está sendo cadastrado e também um id
			return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
		}

		/// <summary>
		/// Atualizar o evento
		/// </summary>
		/// <remarks>
		/// {obj json}
		/// </remarks>
		/// <param name="id">identificador dos eventos</param>
		/// <param name="input">Dados do evento</param>
		/// <returns>Nada</returns>
		/// <response code="204">Sucesso</response>
		/// /// <response code="404">Não encontrado</response>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult Update(Guid id, DevEvent input)
		{
			// Repete o ByID pois primeiro faremos uma verificação, para depois atualizar.
			var devEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

			if (devEvents == null)
			{
				return NotFound();
			}

			devEvents.Update(input.Title, input.Description, input.StartDate, input.EndDate);
			return NoContent();
		}

		// Delete
		// Recebe por parâmetro id 

		[HttpDelete("{id}")]
		public ActionResult Delete(Guid id)
		{
			// Repete o ByID pois primeiro faremos uma verificação, para depois deletar.
			var devEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

			if (devEvents == null)
			{
				return NotFound();
			}

			devEvents.Delete();

			return NoContent();

		}

		[HttpPost("{id}/speakers")]
		public IActionResult PostSpeaker(Guid id, DevEventsSpeakers speakers)
		{
			var devEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

			if (devEvents == null)
			{
				return NotFound();
			}

			devEvents.Speakers.Add(speakers);

			return NoContent();
		}

	}
}
