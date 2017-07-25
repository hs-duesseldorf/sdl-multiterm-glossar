using System.Linq;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class Reference
    {
        /// <remarks/>
        [XmlAttribute]
        public string Tlink { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Clink { get; set; }

        /// <remarks/>
        [XmlText]
        public string Value { get; set; }

        public string ToString(string formatString)
        {
            if (!string.IsNullOrWhiteSpace(Tlink))
            {
                var tLink = Tlink.Split(':');
                if(tLink.First().Equals("Deutsch"))
                    return string.Format(formatString, tLink.Last(), Value);
            }
            return Value;
        }
    }
}