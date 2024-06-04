using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Tag = Studieforeningskalender_Backend.Domain.Tags.Tag;

namespace Studieforeningskalender_Backend.Infrastructure.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Tag> _tag;

		public TagRepository(AppDbContext context)
		{
			_context = context;
			_tag = _context.Tag;
		}

		public Tag? GetTag(string tagName) =>
			_tag.FirstOrDefault(x => x.Name == tagName);

		public Guid? GetTagId(string tagName) =>
			_tag.FirstOrDefault(x => x.Name == tagName)?.Id;

		public IList<Guid> GetTagIds(IList<string> tagName) =>
			_tag.Where(x => tagName.Contains(x.Name)).Select(x => x.Id).ToList();

		public IQueryable<Tag> GetTags() =>
			_tag.OrderByDescending(x => x.EventTags.Count());


		public async Task<int> CreateTag(Tag tag)
		{
			_tag.Add(tag);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteTag(Tag tag)
		{
			_tag.Remove(tag);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteTag(Guid id)
		{
			var tag = _tag.Where(x => x.Id == id).First();
			_tag.Remove(tag);
			return await _context.SaveChangesAsync();
		}
	}
}
