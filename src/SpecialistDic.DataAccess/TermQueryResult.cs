using System.Collections.Generic;
using SpecialistDic.Model;

namespace SpecialistDic.DataAccess
{
    public class TermQueryResult
    {
        public TermQueryResult()
        {
            Terms = new List<TermResult>();
        }
        
        public List<TermResult> Terms { get; set; }

        public int ResultCount { get; set; }
    }
}