using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot("mtf", Namespace = "", IsNullable = false)]
    public class MultiTermRoot
    {
        /// <remarks/>
        [XmlElement("conceptGrp")]
        public ConceptGroup[] ConceptGroups { get; set; }
    }
}