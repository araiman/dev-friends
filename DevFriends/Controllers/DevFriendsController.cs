﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI;
using DevFriends.Models;
using WebGrease.Css.Extensions;

namespace DevFriends.Controllers
{
	public class DevFriendsController : Controller
	{
		// GET: DevFriends
		public ActionResult Index()
		{
			var indexViewmodel = new IndexViewModel();

			using (var devFriendsContext = new DevFriendsContext())
			{
				// userのidを、Projectテーブル・Userテーブル間で異なる型を使用しているため落ちている。
				// 型を変更し、落ちないようにすべし
				var projectsWithUser = devFriendsContext.Projects.Join(
					devFriendsContext.Users,
					p => p.OwnerId,
					u => u.UserId,
					(p, u) => new
					{
						p.Id,
						p.ProjectName,
						p.Description,
						OwnerName = u.Name,
					});

				var tagWithProjectRelation = devFriendsContext.Tags.Join(
					devFriendsContext.TagProjectRelations,
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
					(p, t) => new ProjectWithRelatedInfo
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