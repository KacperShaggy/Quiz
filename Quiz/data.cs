using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal class Game
    {
        public string UserName1 { get; set; }
        public string UserName2 { get; set; }
        public int PointsUser1 { get; set; }
        public int PointsUser2 { get; set; }
        public int RoundNumber { get; set; }

        public Game(string userName1, string userName2, int pointsUser1, int pointsUser2, int roundNumber)
        {
            UserName1 = userName1;
            UserName2 = userName2;
            PointsUser1 = pointsUser1;
            PointsUser2 = pointsUser2;
            RoundNumber = roundNumber;
        }
       

    }
}
