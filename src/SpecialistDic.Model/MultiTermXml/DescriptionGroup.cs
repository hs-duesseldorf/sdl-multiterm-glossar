using System.Diagnostics;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
        /// <remarks/>
        [XmlType(AnonymousType = true)]
        [DebuggerDisplay("{" + nameof(Content) + "}")]
        public class DescriptionGroup
        {
            [XmlElement("descrip")]
            public DescriptionContent Content { get; set; }
            
            [XmlElement("descripGrp")]
            public DescriptionGroup[] Descriptions { get; set; }
        }
}
