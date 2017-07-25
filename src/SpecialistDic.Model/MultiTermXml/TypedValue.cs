using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <summary>
    /// A value with an arbitrary type.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class TypedValue
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}