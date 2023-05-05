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


		// Buscar todos
		[HttpGet]
		public ActionResult GetAll()
		{
			// Efetua a busca daqueles eventos que NÃO estão cancelados
			// Claúsula 
			var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
			return Ok(devEvents);
		}

		// Buscar pelo ID
		// Recebe o parâmetro ID
		[HttpGet("{id}")]
		public ActionResult GetById(Guid id)
		{
			//Busca onde o ID for igual a ID
			var devEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

			// SE  devEvents for igual a nulo, retorne NotFound
			if(devEvents == null)
			{
				return NotFound();
			}

			return Ok(devEvents);
		}

		[HttpPost]
		// Post - 
		// Recebe por parâmetro o DevEvent
		public ActionResult Post(DevEvent devEvent)
		{
			_context.DevEvents.Add(devEvent);

			//está colocando um nome para o objeto que está sendo cadastrado e também um id
			return CreatedAtAction(nameof(GetById), new {id = devEvent.Id}, devEvent);
		}

		// Update - Atualização 
		// Recebe por parâmetro id e devEvent
		// api/dev-events/id
		[HttpPut("{id}")]
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



	}
}
