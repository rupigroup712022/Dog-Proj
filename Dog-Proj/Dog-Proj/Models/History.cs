using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class History
    {
        DateTime dateOfService;
        int givingId;
        int gettingId;
        DateTime hourOfService;
        string typePfService;
        int numOfPoints;
        int opinionScore;

        public History(DateTime dateOfService, int givingId, int gettingId, DateTime hourOfService, string typePfService, int numOfPoints, int opinionScore)
        {
            DateOfService = dateOfService;
            GivingId = givingId;
            GettingId = gettingId;
            HourOfService = hourOfService;
            TypePfService = typePfService;
            NumOfPoints = numOfPoints;
            OpinionScore = opinionScore;
        }

        public DateTime DateOfService { get => dateOfService; set => dateOfService = value; }
        public int GivingId { get => givingId; set => givingId = value; }
        public int GettingId { get => gettingId; set => gettingId = value; }
        public DateTime HourOfService { get => hourOfService; set => hourOfService = value; }
        public string TypePfService { get => typePfService; set => typePfService = value; }
        public int NumOfPoints { get => numOfPoints; set => numOfPoints = value; }
        public int OpinionScore { get => opinionScore; set => opinionScore = value; }
    }
}