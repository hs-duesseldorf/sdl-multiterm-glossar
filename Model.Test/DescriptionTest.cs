using System;
using SpecialistDic.Model.Domain;
using SpecialistDic.Model.MultiTermXml;
using Xunit;

namespace Model.Test
{
    public class DescriptionTest
    {
        [Fact]
        public void Descrition_ToString_Test()
        {
            // -- Arrange
            var sut = new Description()
            {
                References = new Reference[3]
                {
                    new Reference(){Tlink = "Deutsch:auch",Value = "auch"},
                    new Reference(){Tlink = "Deutsch:auch1", Value = "Test"},
                    new Reference(){Tlink = "Deutsch:auch2", Value ="Groﬂes Bla"},

                },
                FormatText = "Dies ist {0} ein {1} und {2} bla bla",
                Type = "Synonyme"
            };

            // -- Act
            var result = sut.ToStrings("<a href=/?q={0}>{1}</a>");

            // -- Assert
            Assert.True(result.Length == 1);

        }
    }
}
