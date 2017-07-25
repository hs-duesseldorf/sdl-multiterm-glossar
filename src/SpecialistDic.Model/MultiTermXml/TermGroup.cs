using System.Diagnostics;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [DebuggerDisplay("{" + nameof(Term) + "}")]
    public class TermGroup
    {
        /// <remarks/>
        [XmlElement("term")]
        public string Term { get; set; }

        /// <remarks/>
        [XmlElement("transacGrp")]
        public Audit[] Audits { get; set; }

        /// <remarks/>
        [XmlElement("descripGrp")]
        public DescriptionGroup[] Descriptions { get; set; }
    }
}