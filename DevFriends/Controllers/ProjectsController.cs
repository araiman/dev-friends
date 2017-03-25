using System.Linq;
using System.Web.Mvc;
using DevFriends.Models;
using DevFriends.Models.Projects;

namespace DevFriends.Controllers
{
	public class ProjectsController : Controller
	{
		public ActionResult Index()
		{
			var indexViewmodel = new IndexViewModel();

			using (var projectsContext = new ProjectsContext())
			{
				var projectsWithUser = projectsContext.Projects.Join(
					projectsContext.Users,
					p => p.OwnerId,
					u => u.UserId,
					(p, u) => new
					{
						p.Id,
						p.ProjectName,
						p.Description,
						OwnerName = u.Name,
					});

				var tagWithProjectRelation = projectsContext.Tags.Join(
					projectsContext.TagProjectRelations,
					t => t.Id,
					r => r.TagId,
					(t, r) => new
					{
						r.ProjectId,
						t.Name
					});

				indexViewmodel.ProjectsWithContext = projectsWithUser.GroupJoin(
					tagWithProjectRelation,
					p => p.Id,
					t => t.ProjectId,
					(p, t) => new ProjectWithContext
					{
						ProjectName = p.ProjectName,
						OwnerName = p.OwnerName,
						ProjectDescription = p.Description,
						TagNames = t.Select(tn => tn.Name)
					}).ToList();
			}

			return View(indexViewmodel);
		}
	}
}