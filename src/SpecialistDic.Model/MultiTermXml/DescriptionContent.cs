using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace SpecialistDic.Model.MultiTermXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [DebuggerDisplay("{"+nameof(Type)+"} = {" + nameof(Text) + "}")]
    public class DescriptionContent
    {
        /// <remarks/>
        [XmlElement("xref")]
        public Reference[] References { get; set; }

        /// <remarks/>
        [XmlText]
        public string[] Text { get; set; }

        /// <remarks/>
        [XmlAttribute("type")]
        public string Type { get; set; }

        public string GetFormatText()
        {
            var result = "";
            if (References != null && Text.Length == References.Length + 1)
            {
                for (var i = 0; i < References.Length; i++)
                {
                    result += Text[i] + $"{{{i}}}";
                }
            }
            result += Text.Last();
            return result;
        }

        public string GetFullPlainText()
        {
            var result = "";
            if (References != null && Text.Length == References.Length + 1)
            {
                for (var i = 0; i < References.Length; i++)
                {
                    result += Text[i] + References[i].Value;
                }
            }
            result += Text.Last();
            return result;
        }
        
    }
}