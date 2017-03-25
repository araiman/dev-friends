using System.Linq;
using System.Web.Mvc;
using DevFriends.Models;

namespace DevFriends.Controllers
{
	public class DayController : Controller
	{
		/// <summary>
		/// 日付毎のメンバーの感情を表示する
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			var dayViewModel = new DayViewModel();

			using (var context = new NiconicoContext())
			{
				var users = context.User.ToList();

				dayViewModel.Users = users;
			}

			return View(dayViewModel);
		}

		public ActionResult RegisterEmotion()
		{
			return null;
		}

		public ActionResult UpdateEmotion()
		{
			return null;
		}
	}
}