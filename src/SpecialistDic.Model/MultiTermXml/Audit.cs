using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <summary>
    /// An Audit entry
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class Audit
    {
        [XmlElement("transac")]
        public TypedValue Content { get; set; }

        [XmlAttribute("date")]
        public System.DateTime Date { get; set; }
    }
}