using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using DevFriends.Models;
using DevFriends.Models.Projects;
using WebGrease.Css.Extensions;

namespace DevFriends.Controllers
{
	public class ProjectsController : Controller
	{
		public ActionResult Index()
		{
			var indexViewmodel = new IndexViewModel();

			using (var projectsContext = new ProjectsContext())
			{
				var projectsWithOwnerName = projectsContext.Projects.Join(
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

				var tagsWithProjectId = projectsContext.Tags.Join(
					projectsContext.TagProjectRelations,
					t => t.Id,
					r => r.TagId,
					(t, r) => new
					{
						r.ProjectId,
						t.Name
					});

			return View(new IndexViewModel { ProjectsWithContext = projectsWithContexts });
		}

		// todo そもそも直接呼び出すことを制御するか何かしらのバリデーションが必要
		public ActionResult Detail(int projectId, string ownerId, int tagIdForRelatedProjects)
		{
			var detailInputViewModel = new DetailInputViewModel();

			using (var projectsContext = new ProjectsContext())
			{
				//var tagWithProjectRelations = projectsContext.Tags.Join(
				//	projectsContext.TagProjectRelations,
				//	t => t.Id,
				//	r => r.TagId,
				//	(t, r) => new
				//	{
				//		r.ProjectId,
				//		Tag = t
				//	}).ToList();

				// Project
				detailInputViewModel.Project = projectsContext.Projects.Single(p => p.Id == projectId);
				
				detailInputViewModel.Owner = projectsContext.Users.Single(u => u.UserId == ownerId);

				detailInputViewModel.Tags = projectsContext.Tags.Where(t => t.TagProjectRelations.Any(r => r.ProjectId == projectId));
				//detailInputViewModel.Tags = tagWithProjectRelations.Where(t => t.ProjectId == inputViewModel.Project.Id)
				//	.Select(tp => tp.Tag);

				//if (tagIdForRelatedProjects == null)
				//	tagIdForRelatedProjects = projectsContext.Tags.Single(t => t.TagProjectRelations.Any(r => r.ProjectId == inputViewModel.RenderedProjectId)).Id;
					//inputViewModel.TagIdForRelatedProjects = tagWithProjectRelations.Single(t => t.ProjectId == inputViewModel.Project.Id).Tag.Id;

				var relatedProjects = new List<Project>();
				foreach (var project in projectsContext.Projects.ToList())
				{
					if (project.TagProjectRelations.Any(r => r.TagId == tagIdForRelatedProjects))
						relatedProjects.Add(project);	
				}
				//var relatedProjects = projectsContext.Projects.Join(
				//	projectsContext.TagProjectRelations,
				//	p => p.Id,
				//	r => r.ProjectId,
				//	(p, r) => new
				//	{
				//		p,
				//		r.TagId
				//	}).Distinct().Where(pt => pt.TagId == inputViewModel.TagIdForRelatedProjects.GetValueOrDefault()).Select(pr => pr.p);

				var relatedProjectsCountToTook = relatedProjects.Count() >= 3 ? 3 : relatedProjects.Count();

				detailInputViewModel.RelatedProjects = relatedProjects.Take(relatedProjectsCountToTook);
			}

			return View(detailInputViewModel);
		}
	}
}