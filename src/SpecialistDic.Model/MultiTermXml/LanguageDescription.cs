using System.Diagnostics;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [DebuggerDisplay("{" + nameof(TwoLetterLanguageCode) + "}")]
    public class LanguageDescription
    {
        /// <remarks/>
        [XmlAttribute("lang")]
        public string TwoLetterLanguageCode { get; set; }

        /// <remarks/>
        [XmlAttribute("type")]
        public string Name { get; set; }
    }
}