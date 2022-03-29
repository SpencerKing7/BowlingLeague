using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        // Constructor
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(string team)
        {
            ViewBag.TeamTitle = team;

            var x = _repo.Bowlers
                .Include(x => x.Team)
                .Where(x => x.Team.TeamName == team || team == null)
                .ToList();

            return View(x);
        }

        [HttpGet]
        public IActionResult EditForm(int bowler)
        {
            ViewBag.Teams = _repo.Teams.ToList();

            var x = _repo.Bowlers
                .Include(x => x.Team)
                .Single(x => x.BowlerId == bowler);

            return View("EditForm", x);
        }

        [HttpPost]
        public IActionResult EditForm(Bowler b)
        {
            _repo.SaveBowler(b);
            
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult AddForm()
        {
            ViewBag.Teams = _repo.Teams.ToList();
            ViewBag.IDs = _repo.Bowlers.Max(x => x.BowlerId) + 1;

            return View(new Bowler());
        }

        [HttpPost]
        public IActionResult AddForm(Bowler b)
        {
            _repo.CreateBowler(b);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteBowler(int bowler)
        {
            Bowler b = _repo.Bowlers
                .Include(x => x.Team)
                .Single(x => x.BowlerId == bowler);

            _repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }
    }
}
