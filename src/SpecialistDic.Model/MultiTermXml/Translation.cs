using System.Diagnostics;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [DebuggerDisplay("{" + nameof(Language) + "}")]
    public class Translation
    {
        [XmlElement("language")]
        public LanguageDescription Language { get; set; }

        /// <remarks/>
        [XmlElement("termGrp")]
        public TermGroup[] TermGroups { get; set; }
    }
}