using SpecialistDic.Model.MultiTermXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialistDic.Model.Domain
{
    public class Description
    {
        public Reference[] References { get; set; }

        public string FormatText { get; set; }

        public string Type { get; set; }

        public string[] ToStrings()
        {
            var text = FormatText;
            if(References != null)
                text = string.Format(FormatText, References.Select(r => r.Value));
            var result = Split(text);
            return result;
        }

        public string[] ToStrings(string formattedLinkedText)
        {
            if (References == null)
                return ToStrings();

            var formattedReferences = References.Select(r => r.ToString(formattedLinkedText)).ToArray();
            var formattedText = string.Format(FormatText, formattedReferences);
            var result = Split(formattedText);
            return result;
        }

        private static string[] Split(string s)
        {
            return s.Split(new[] { ";", "|", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }
    }


}
