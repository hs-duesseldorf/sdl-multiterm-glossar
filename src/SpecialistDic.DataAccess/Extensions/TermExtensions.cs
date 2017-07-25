using System;
using System.Collections.Generic;
using System.Linq;
using SpecialistDic.Model;

namespace SpecialistDic.DataAccess.Extensions
{
    static class TermGroupExtensions
    {
        public static IOrderedEnumerable<TermResult> OrderByQuery(this IEnumerable<TermResult> enumerable, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return enumerable.OrderBy(tg => tg.SourceTerm.Text);

            return enumerable
                .OrderBy(tg => !tg.SourceTerm.Text.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                .ThenBy(tg => tg.SourceTerm.Text)
                .ThenBy(tg => tg.TargetTerm.Text);
        }
    }
}
