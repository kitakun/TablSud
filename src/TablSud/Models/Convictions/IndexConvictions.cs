using System;
using System.Collections.Generic;
using System.Linq;
using TablSud.Core.Data.Mongo;
using TablSud.Core.Domain.Court;

namespace TablSud.Web.Models.Convictions
{
    /// <summary>
    /// Data-model for index page
    /// </summary>
    public class IndexConvictions
    {
        public IndexConvictions(IEnumerable<ConvictionsItem> input, int size)
        {
            Collection = input.ToList() ?? throw new ArgumentNullException(nameof(input));
            Count = size;
        }

        public int Count { get; set; }

        public int TotalPages
        {
            get
            {
                int coeff = Count % MongoRepository<ConvictionsItem>.ElementsOnPage > 0 ? 1 : 0;
                int rsl = Count / MongoRepository<ConvictionsItem>.ElementsOnPage + coeff;
                return rsl;
            }
        }


        public IEnumerable<ConvictionsItem> Collection { get; }
    }
}
