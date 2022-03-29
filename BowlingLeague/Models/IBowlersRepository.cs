using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        //Edit
        public void SaveBowler(Bowler b);
        //Create
        public void CreateBowler(Bowler b);
        //Delete
        public void DeleteBowler(Bowler b);
    }
}
