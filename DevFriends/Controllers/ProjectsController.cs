using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using DevFriends.Models;
using DevFriends.Models.Projects;

namespace DevFriends.Controllers
{
	public class ProjectsController : Controller
	{
		public ActionResult Index()
		{
			var projectsWithContexts = new List<ProjectWithContext>();
			using (var projectsContext = new ProjectsContext())
			{
				var projectsWithOwnerName = projectsContext.Projects.Join(
					projectsContext.Users,
					p => p.OwnerId,
					u => u.UserId,
					(p, u) => new
					{
						Project = p,
						OwnerName = u.Name
					}).ToList();

				foreach (var projectWithOwnerName in projectsWithOwnerName)
				{
					var tagNames = projectWithOwnerName.Project.TagProjectRelations.Join(
						projectsContext.Tags,
						r => r.TagId,
						t => t.Id,
						(r, t) => t.Name
						).ToList();

					projectsWithContexts.Add(new ProjectWithContext
					{
						Project = projectWithOwnerName.Project,
						OwnerName = projectWithOwnerName.OwnerName,
						TagNames = tagNames
					});
				}
			}

			return View(new IndexViewModel {ProjectsWithContext = projectsWithContexts});
		}

		public ActionResult Detail(int projectId, string ownerId, int tagIdForRelatedProjects)
		{
			var detailInputViewModel = new DetailInputViewModel();

			using (var projectsContext = new ProjectsContext())
			{
				// Project
				detailInputViewModel.Project = projectsContext.Projects.Single(p => p.Id == projectId);

				detailInputViewModel.Owner = projectsContext.Users.Single(u => u.UserId == ownerId);

				detailInputViewModel.Tags =
					projectsContext.Tags.Where(t => t.TagProjectRelations.Any(r => r.ProjectId == projectId)).ToList();

				var relatedProjects = new List<Project>();
				foreach (var project in projectsContext.Projects.ToList())
				{
					if (project.TagProjectRelations.Any(r => r.TagId == tagIdForRelatedProjects))
						relatedProjects.Add(project);
				}

				var relatedProjectsCountToTook = relatedProjects.Count() >= 3 ? 3 : relatedProjects.Count();

				detailInputViewModel.RelatedProjects = relatedProjects.Take(relatedProjectsCountToTook);
			}

			var md5 = new MD5CryptoServiceProvider();
			var hasedEmail =
				BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(detailInputViewModel.Owner.Email.Trim().ToLower())));
			var fromattedEmailForGravatar = hasedEmail.Replace("-", "").ToLower();
			detailInputViewModel.HashedOwnerEmailForProfileImage = fromattedEmailForGravatar;

			return View(detailInputViewModel);
		}
	}
}