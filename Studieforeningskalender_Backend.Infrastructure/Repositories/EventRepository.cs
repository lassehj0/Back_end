using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class EventRepository : IEventRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Event> _event;

		public EventRepository(AppDbContext context)
		{
			_context = context;
			_event = _context.Event;
		}

		public IQueryable<EventDto> GetEvent()
		{
			return _event
				.Select(e => new EventDto
				{
					Id = e.Id,
					Title = e.Title,
					Description = e.Description,
					StartTime = e.StartTime,
					EndTime = e.EndTime,
					Image = Convert.ToBase64String(e.Image),
					MediumImage = Convert.ToBase64String(e.MediumImage),
					SmallImage = Convert.ToBase64String(e.SmallImage),
					Creation = e.Creation,
					AddressLine = e.AddressLine,
					City = e.City,
					PostalCode = e.PostalCode,
					EventTags = e.EventTags,
					EventUsers = e.EventUsers,
				});
		}

		public Event? GetEventById(Guid id) =>
			_event.Where(e => e.Id == id).FirstOrDefault();

		public IQueryable<EventDto> GetEvents(string sorting, IList<string>? tags, string searchText)
		{
			IQueryable<Event> query;

			if (!searchText.IsNullOrEmpty())
			{
				var sql = @"
					SELECT *, similarity(""Title"" || ' ' || ""Description"", {0}) AS rank
					FROM public.""Event""
					WHERE similarity(""Title"" || ' ' || ""Description"", {0}) > 0.01
					ORDER BY rank DESC";

				query = _event.FromSqlRaw(sql, searchText.ToLower());
			}
			else
			{
				query = _event;
			}

			if (tags != null && tags.Count > 0)
				query = query.Where(x => x.EventTags.Any(et => tags.Contains(et.Tag.Name)));

			if (searchText.IsNullOrEmpty())
			{
				if (sorting.ToLower() == "popular")
					query = query.OrderByDescending(x => x.EventUsers.Count()).ThenBy(x => x.Id);
				else if (sorting.ToLower() == "soon")
					query = query.OrderBy(x => x.StartTime).ThenBy(x => x.Id);
			}

			IQueryable<EventDto> newQuery = query
				.Select(e => new EventDto
				{
					Id = e.Id,
					Title = e.Title,
					Description = e.Description,
					StartTime = e.StartTime,
					EndTime = e.EndTime,
					Image = Convert.ToBase64String(e.Image),
					MediumImage = Convert.ToBase64String(e.MediumImage),
					SmallImage = Convert.ToBase64String(e.SmallImage),
					Creation = e.Creation,
					AddressLine = e.AddressLine,
					City = e.City,
					PostalCode = e.PostalCode,
					EventTags = e.EventTags,
					EventUsers = e.EventUsers,
				});

			return newQuery;
		}

		public async Task<int> CreateEvent(Event newEvent)
		{
			_event.Add(newEvent);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteEvent(Event delEvent)
		{
			_event.Remove(delEvent);
			return await _context.SaveChangesAsync();
		}
	}
}
