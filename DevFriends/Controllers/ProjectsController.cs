using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
				// Project
				detailInputViewModel.Project = projectsContext.Projects.Single(p => p.Id == projectId);
				
				detailInputViewModel.Owner = projectsContext.Users.Single(u => u.UserId == ownerId);

				detailInputViewModel.Tags = projectsContext.Tags.Where(t => t.TagProjectRelations.Any(r => r.ProjectId == projectId)).ToList();

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
			var hasedEmail = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(detailInputViewModel.Owner.Email.Trim().ToLower())));
			var fromattedEmailForGravatar = hasedEmail.Replace("-", "").ToLower();
			detailInputViewModel.HashedOwnerEmailForProfileImage = fromattedEmailForGravatar;

			return View(detailInputViewModel);
		}
	}
}