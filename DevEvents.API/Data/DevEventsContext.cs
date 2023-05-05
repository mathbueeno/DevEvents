using DevEvents.API.Entities;

namespace DevEvents.API.Data
{
	public class DevEventsContext
	{
		public List<DevEvent> DevEvents { get; set; }

		public DevEventsContext()
		{
			DevEvents= new List<DevEvent>();
		}
	

	}
}
