using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class ConceptGroup
    {
        [XmlElement("concept")]
        public int ConceptNr { get; set; }

        [XmlElement("transacGrp")]
        public Audit[] Audits { get; set; }

        [XmlElement("descripGrp")]
        public DescriptionGroup[] Descriptions { get; set; }

        [XmlElement("languageGrp")]
        public Translation[] Translations { get; set; }
    }
}