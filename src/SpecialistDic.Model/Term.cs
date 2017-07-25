using System.Collections.Generic;
using System.Linq;
using SpecialistDic.Model.MultiTermXml;
using SpecialistDic.Model.Domain;

namespace SpecialistDic.Model
{
    public class Term
    {
        public Term(string text, string language, List<Description> descriptions)
        {
            Text = text;
            Language = language;

            Descriptions = descriptions
                .ToLookup(d => d.Type, d => d);
        }
        
        public string Text { get; set; }
        public string Language { get; set; }
        
        public ILookup<string, Description> Descriptions { get; set; }
    }
}
