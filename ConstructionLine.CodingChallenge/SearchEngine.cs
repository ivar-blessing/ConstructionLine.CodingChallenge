using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    /// <summary>
    ///     Search shirts
    /// </summary>
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly SearchResults _searchResult;

        /// <summary>
        ///   New instance of <see cref="SearchEngine"/>
        /// </summary>
        /// <param name="shirts">List of <see cref="Shirt"/></param>
        public SearchEngine(List<Shirt> shirts)
        {
            this._shirts = shirts;

            // init search results
            this._searchResult = new SearchResults
            {
                Shirts = new List<Shirt>(), // empty list of shirts
                ColorCounts = Color.All.Select(c => new ColorCount() // list of colours
                {
                    Color = c,
                    Count = 0
                }).ToList(),
                SizeCounts = Size.All.Select(s => new SizeCount() // list of sizes
                {
                    Size = s,
                    Count = 0
                }).ToList()
            };

        }

        /// <summary>
        ///     Perform search operation on list of shirts
        /// </summary>
        /// <param name="options">Search options, <see cref="SearchOptions"/></param>
        /// <returns>Search result <see cref="SearchResults"/></returns>
        public SearchResults Search(SearchOptions options)
        {
            foreach (var shirt in this._shirts)
            {
                if ((!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(shirt.Size.Id)) &&
                    (!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(shirt.Color.Id)))
                {
                    // add shirt to list if size and color match or there's no filter for either
                    this._searchResult.Shirts.Add(shirt);
                }

                if ((!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(shirt.Size.Id)))
                {
                    // update color count for shirt size
                    this._searchResult.ColorCounts.SingleOrDefault(s => s.Color.Id == shirt.Color.Id).Count++;
                }

                if ((!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(shirt.Color.Id)))
                {
                    // update size count for shirt size
                   this._searchResult.SizeCounts.SingleOrDefault(s => s.Size.Id == shirt.Size.Id).Count++;
                }                
            }

            return this._searchResult;
        }
    }
}