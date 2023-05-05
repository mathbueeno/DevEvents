namespace DevEvents.API.Entities
{
	public class DevEvent
	{
		// Método Construtor
		public DevEvent()
		{
			// Verificar
			Speakers = new List<DevEventsSpeakers>();
			//Aqui está dando início como falso
			IsDeleted= false;
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public List<DevEventsSpeakers> Speakers { get; set; }

		public bool IsDeleted { get; set; }



		// Criação dos métodos
		// Método de atualização, está recebendo como parâmetro o que pode ser atualizado
		public void Update(string title, string description, DateTime startDate, DateTime endDate)
		{
			Title= title;
			Description= description;
			StartDate= startDate;
			EndDate= endDate;
		}


		// Criação dos métodos
		// Método de deleção
		public void Delete()
		{
			IsDeleted = true;
		}


	}

	
}
