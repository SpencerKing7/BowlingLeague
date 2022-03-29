using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlersRepository _repo { get; set; }

        public TeamsViewComponent (IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["team"];

            var teams = _repo.Bowlers
                .Include(x => x.Team)
                .Select(x => x.Team.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
    }
}
