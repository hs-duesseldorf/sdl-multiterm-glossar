using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SpecialistDic.DataAccess.Extensions;
using SpecialistDic.Model;
using SpecialistDic.Model.MultiTermXml;
using SpecialistDic.Model.Domain;

namespace SpecialistDic.DataAccess
{
    public class TermQuery
    {
        public string SearchText { get; set; }

        public string SearchPath { get; set; }

        public int MaxResults { get; set; }
    }


    public class XmlFileQueryHandler : ITermQueryHandler
    {
        public Task<TermQueryResult> ExecuteQueryAsync(TermQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.SearchPath))
                throw new ArgumentNullException(nameof(query.SearchPath));

            var isDirectory = IO.Directory.Exists(query.SearchPath);
            if (!isDirectory && !IO.File.Exists(query.SearchPath))
                throw new ArgumentException("SearchPath does not exist", nameof(query.SearchPath));

            return isDirectory
                ? ExecuteDirectoryQuery(query)
                : ExecuteFileQuery(query, query.SearchPath);
        }


        private async Task<TermQueryResult> ExecuteDirectoryQuery(TermQuery query)
        {
            var xmlFiles = IO.Directory.EnumerateFiles(query.SearchPath, "*.xml", IO.SearchOption.AllDirectories);

            var result = new TermQueryResult();
            foreach (var xmlFile in xmlFiles) //TODO: Try Catch!
            {
                var fileResult = await ExecuteFileQuery(query, xmlFile);
                result.ResultCount += fileResult.ResultCount;
                result.Terms.AddRange(fileResult.Terms);
            }

            result.Terms = result.Terms
                .OrderByQuery(query.SearchText)
                .Take(query.MaxResults)
                .ToList();

            return result;
        }


        private async Task<TermQueryResult> ExecuteFileQuery(TermQuery query, string filePath)
        {
            if (!IO.File.Exists(filePath))
                throw new IO.FileNotFoundException("File could not be found.", filePath);

            var multiTermXml = await Task.Run(() =>
            {
                using (var fileStream = IO.File.OpenRead(filePath))
                {
                    var serializer = new XmlSerializer(typeof(MultiTermRoot));

                    var xmlContent = serializer.Deserialize(fileStream) as MultiTermRoot;
                    return xmlContent;
                }
            });

            if (multiTermXml == null)
                return new TermQueryResult();

            var result = GetResultsPerXml(multiTermXml, query.SearchText, filePath);

            return new TermQueryResult
            {
                Terms = result.OrderByQuery(query.SearchText)
                              .Take(query.MaxResults)
                              .ToList(),
                ResultCount = result.Count
            };
        }


        /// <summary>
        /// Per Xml File
        /// </summary>
        /// <param name="multiTermRoot"></param>
        /// <param name="searchText"></param>
        /// <param name="filePath"></param>
        /// <returns>One List of TermGroup(elements) unsorted, where element.LangDe contains filter, or all elements if filter is null or empty</returns>
        private List<TermResult> GetResultsPerXml(MultiTermRoot multiTermRoot, string searchText, string filePath)
        {
            var result = new List<TermResult>();

            var termConceptGroups = GetTermConceptGroups(multiTermRoot, searchText, "DE");

            if (searchText.Length > 1)  // do not include synonyms in one letter search
            {
                var synonymConeptGroups = GetSynonymTermConceptGroups(multiTermRoot, searchText, "DE");

                foreach (var synonymConeptGroup in synonymConeptGroups)
                {
                    if (!termConceptGroups.ContainsKey(synonymConeptGroup.Key))
                        termConceptGroups.Add(synonymConeptGroup.Key, synonymConeptGroup.Value);
                }
            }


            foreach (var sourceTermGroup in termConceptGroups.Keys)
            {
                var conceptGroup = termConceptGroups[sourceTermGroup];
                var conceptDescriptions = GetDescriptions(conceptGroup.Descriptions);

                var subjects = GetSubjects(conceptDescriptions, IO.Path.GetFileNameWithoutExtension(filePath));

                var sourceDescriptions = GetDescriptions(sourceTermGroup.Descriptions);
                var sourceTerm = new Term(sourceTermGroup.Term, "DE", sourceDescriptions);

                // ===============================================================================================================
                //TODO: Source und Target LAnguage!
                var germanLanguageGroup = conceptGroup.Translations.FirstOrDefault(x => x.Language.TwoLetterLanguageCode == "DE"); //TODO: In Methode umwandeln GetLanguageGroup(string lng)
                var englishLanguageGroup = conceptGroup.Translations.FirstOrDefault(x => x.Language.TwoLetterLanguageCode == "EN");

                var germanTerm = sourceTermGroup.Term;

                var germanTermGroups = germanLanguageGroup.TermGroups; //TODO: Possible NullReferenceException
                //TODO: ToList().IndexOf Statt TakeWhile
                var posInGermanTermGroup = germanTermGroups.TakeWhile(termGroup => !termGroup.Term.Equals(germanTerm)).Count();

                var targetDescriptions = GetDescriptions(englishLanguageGroup.TermGroups[posInGermanTermGroup].Descriptions);
                //TODO: ArrayOutOfBounds beachten!
                var targetTerm = new Term(englishLanguageGroup.TermGroups[posInGermanTermGroup].Term, "EN", targetDescriptions);
                // ===============================================================================================================
                

                result.Add(new TermResult
                {
                    Subjects = subjects,

                    SourceTerm = sourceTerm,
                    TargetTerm = targetTerm
                });
                //}
            }

            return result;
        }


        /// <summary>
        /// Fetches all concept groups with a matching term.
        /// </summary>
        /// <param name="multiTermRoot"></param>
        /// <param name="searchText"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private Dictionary<TermGroup, ConceptGroup> GetTermConceptGroups(MultiTermRoot multiTermRoot, string searchText, string language)
        {
            var result = new Dictionary<TermGroup, ConceptGroup>();
            bool isWordSearch = searchText.Length > 1;

            foreach (var concept in multiTermRoot.ConceptGroups)
            {
                var sourceTermsQuery = concept.Translations
                    .Where(t => t.Language.TwoLetterLanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase))
                    .SelectMany(t => t.TermGroups);
                
                if (isWordSearch)
                    sourceTermsQuery = sourceTermsQuery.Where(tg => tg.Term.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
                else 
                    sourceTermsQuery = sourceTermsQuery.Where(tg => tg.Term.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) == 0);

                foreach (var sourceTerm in sourceTermsQuery.ToList())
                    result.Add(sourceTerm, concept);
            }

            return result;
        }

        /// <summary>
        /// Fetches all concept groups with a matching term in the Synonyms.
        /// </summary>
        /// <param name="multiTermRoot"></param>
        /// <param name="searchText"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private Dictionary<TermGroup, ConceptGroup> GetSynonymTermConceptGroups(MultiTermRoot multiTermRoot, string searchText, string language)
        {
            var result = new Dictionary<TermGroup, ConceptGroup>();

            foreach (var concept in multiTermRoot.ConceptGroups)
            {
                var sourceTerms = concept.Translations
                    .Where(t => t.Language.TwoLetterLanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase))
                    .SelectMany(t => t.TermGroups)
                    .Where(tg => tg.Descriptions?.Any(des =>
                        des.Content.Type.Equals("Synonyme")
                        && des.Content.GetFullPlainText().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        ?? false)
                    .ToList();

                foreach (var sourceTerm in sourceTerms)
                    result.Add(sourceTerm, concept);
            }

            return result;
        }


        /// <summary>
        /// Fetches all terms with a matching language
        /// </summary>
        /// <param name="conceptGroup"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private List<TermGroup> GetTargetTerms(ConceptGroup conceptGroup, string language)
        {
            var result = conceptGroup
                .Translations
                .Where(t => t.Language.TwoLetterLanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase))
                .SelectMany(t => t.TermGroups)
                .ToList();

            return result;
        }


        private List<Description> GetDescriptions(DescriptionGroup[] descriptionGroups)
        {
            var result = new List<Description>(descriptionGroups?.Length ?? 0);
            if (descriptionGroups == null)
                return result;

            foreach (var descriptionGroup in descriptionGroups)
            {
                result.Add(new Description()
                {
                    FormatText = descriptionGroup.Content.GetFormatText(),
                    References = descriptionGroup.Content.References,
                    Type = descriptionGroup.Content.Type
                });

                if (descriptionGroup.Descriptions?.Any() != true)
                    continue;

                result.AddRange(GetDescriptions(descriptionGroup.Descriptions));
            }

            return result;
        }

        private Description[] GetSubjects(List<Description> descriptions, string fileName)
        {
            var result = descriptions.Where(des => des.Type.Equals("Fachgebiet", StringComparison.OrdinalIgnoreCase)).ToArray();
            if (result.Any())
                return result;

            return new[]{new Description()
            {
                FormatText = fileName
            } };
        }
    }
}
