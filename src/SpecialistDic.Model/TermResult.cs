using SpecialistDic.Model.Domain;

namespace SpecialistDic.Model
{
    public class TermResult
    {
        public Description[] Subjects { get; set; }
        
        public Term SourceTerm { get; set; }
        public Term TargetTerm { get; set; }
    }
}